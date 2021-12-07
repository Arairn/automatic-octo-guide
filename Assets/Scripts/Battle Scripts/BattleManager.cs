using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Battle Settings")]
    private bool battleIsActive;
    public int currentTurn;
    public bool turnWaiting;
public GameObject battleScene;

    bool playerTurnJustStarted;
    [Header("Battle Visual")]

       //Это все для включения боя

    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    float timeForEnemyToMove = 1f;
    public DamageText damageText;
    public MarkerJumper highLight;

    [Header("Battle Math")]

    public GameObject playerPrefab;
    public BattleChars[] playerPrefabs;
    public BattleChars[] enemyPrefabs;

    public List<BattleChars> activeBattlers = new List<BattleChars>();
    public List<int> activeBattlersFriendly = new List<int>();
    public List<int> activeBattlersEnemy = new List<int>();

    public BattleChars player;
    [Header("Battle UI")]

    //public GameObject uiButtons;
    public GameObject playerInfoParent, enemyInfoParent;
    public GameObject battlerInfo;
    public PlayerBattleMenu playerBattleMenu;

    public static event Action BattlersHaveChanged, BattleHasEnded;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y); //Немного геморойно, но т.к. камера не жестко привязана к персонажу, она может открывать бой с небольшим сдвигом. Так - работает.
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Eyeball", "Spider" });

        }
#endif

        if (battleIsActive)
        {
            if (turnWaiting)
            {
                if (activeBattlers[currentTurn].isPlayer)
                {
                    if (playerTurnJustStarted)
                    {
                        playerTurnJustStarted = false;
                        PlayerTurn();
                    }
                }
                else
                {
                    StartCoroutine(EnemyMove());
                }
            }
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        int[] randomPlaces = new int[enemiesToSpawn.Length];
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            randomPlaces[i] = UnityEngine.Random.Range(0, enemyPositions.Length);
        }
        BattleStart(enemiesToSpawn, randomPlaces);
    }
    public void BattleStart(string[] enemiesToSpawn, int[] enemyPlacesToSpawn) //Старт боя. К рефакторингу - Нужно будет разделить подготовку, и спаун сущностей, т.к. они будут появляться и в бою
    {
        if (!battleIsActive)
        {
            battleIsActive = true;
            GameManager.instance.battleIsActive = true;
            battleScene.SetActive(true);

            activeBattlers.Clear();

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);

            // Временная затычка для проверки геймплея. Затем, если можно будет обзавестись спутниками, нужно будет переделать как для врагов.
            GameObject newBattlePlayer = Instantiate(playerPrefab, playerPositions[0].position, playerPositions[0].rotation);
            newBattlePlayer.transform.parent = playerPositions[0];
            newBattlePlayer.GetComponent<PlayerBattleInfoPrefabUpdater>().Renew(true);
            player = newBattlePlayer.GetComponent<BattleChars>();


            activeBattlers.Add(player);
            //activeBattlersFriendly.Add(0);
            Instantiate(battlerInfo).GetComponent<BattlerInfoUpdater>().SetParams(0,playerInfoParent);
            highLight.JumpTo(playerPositions[0]);
            //highLight.transform.position = playerPositions[0].position;


            //Нужно добавить проверку на повтор места, чтобы 2 врага не могли попасть на уже занятую клетку.

            //Спаун врагов на сцене боя
            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {


                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].name == enemiesToSpawn[i])
                    {
                        Debug.Log("Добавляем " + enemyPrefabs[j].name);

                        BattleChars newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[enemyPlacesToSpawn[i]].position, enemyPositions[enemyPlacesToSpawn[i]].rotation);
                        
                        newEnemy.transform.parent = enemyPositions[enemyPlacesToSpawn[i]];

                        activeBattlers.Add(newEnemy);
                        Instantiate(battlerInfo).GetComponent<BattlerInfoUpdater>().SetParams(activeBattlers.Count-1, enemyInfoParent);
                        //activeBattlersEnemy.Add(i);
                    }

                }

            }

            currentTurn = 0;
            turnWaiting = true;
            BattlersHaveChanged?.Invoke();
            UpdateBattle();
            PlayerTurn();
        }
    }

    public void nextTurn()
    {

        if (currentTurn >= activeBattlers.Count - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
        highLight.JumpTo(activeBattlers[currentTurn].GetComponent<Transform>());
        turnWaiting = true;
        playerTurnJustStarted = true;
        UpdateBattle();
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        activeBattlersEnemy.Clear();
        activeBattlersFriendly.Clear();

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHP <= 0)
            {
                activeBattlers[i].SetSpriteToDead(true);

            }
            else
            {
                activeBattlers[i].SetSpriteToDead(false);
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersAreDead = false;
                    activeBattlersFriendly.Add(i);
                }
                else
                {
                    allEnemiesAreDead = false;
                    activeBattlersEnemy.Add(i);
                }
            }
        }

        if (allPlayersAreDead)
        {
            //End battle in lose
            //StartCoroutine(EndBattle());
            CloseBattle();
            return;
        }

        if (allEnemiesAreDead)
        {

            //End battle in Victory
            CloseBattle();
        }

        if (activeBattlers[currentTurn].currentHP == 0)
        {
            nextTurn();
        }
    }

    private void CloseBattle()
    {
        if (battleIsActive) {
            BattleHasEnded?.Invoke();
            GameManager.instance.ReturnFromBattle();
            StartCoroutine(GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().BlackAndGone());
            StartCoroutine(EndBattle(GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().transitionTime));
            }
        battleIsActive = false;
        
    }

    public IEnumerator EnemyMove()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(timeForEnemyToMove);
        EnemyAttack();
        BattlersHaveChanged?.Invoke();
        yield return new WaitForSeconds(timeForEnemyToMove);
        nextTurn();
    }

    private void EnemyAttack()
    {
        BattleAI AI = activeBattlers[currentTurn].GetComponent<BattleAI>();
        int target = AI.SelectTarget();
        BattleMoves attack = AI.SelectAttack();

        CalculateDamage(target, attack);

    }

    public void CalculateDamage(int target, BattleMoves move)
    {

        move.DisplayMove(activeBattlers[target].transform);

        float atkPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].weaponPWR;
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armorPWR;

        float DamageDealt = atkPwr / defPwr * move.movePower * UnityEngine.Random.Range(0.8f, 1.2f);

        DealDamage(target, DamageDealt);

    }
    public void DealDamage(int target, float damage)
    {
        DealDamage(target, Mathf.RoundToInt(damage));
    }

    public void DealDamage(int target, int damage)
    {

        Debug.Log(activeBattlers[currentTurn].name + " deals " + damage + " to " + activeBattlers[target].name);
        activeBattlers[target].DealDamage(damage);
        Instantiate(damageText, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damage);
        BattlersHaveChanged?.Invoke();

    }

    public void PlayerTurn()
    {
        playerBattleMenu.ShowButtons();

    }

    IEnumerator EndBattle(float transitionTime)
    {
        //BattleHasEnded?.Invoke();
        yield return new WaitForSeconds(transitionTime);
        GameManager.instance.LinkToBattleChars();

        
        foreach (BattleChars item in activeBattlers)
        {
            Destroy(item.gameObject);
        }
        battleScene.SetActive(false);
        
        yield return new WaitForSeconds(transitionTime);
        GameManager.instance.battleIsActive = false;

    }
}
