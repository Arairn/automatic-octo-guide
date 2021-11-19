using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool battleIsActive;
    public GameObject battleScene;   //Это все для включения боя

    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChars[] playerPrefabs;
    public BattleChars[] enemyPrefabs;

    public List<BattleChars> activeBattlers = new List<BattleChars>();
    public List<int> activeBattlersFriendly = new List<int>();
    public List<int> activeBattlersEnemy = new List<int>();


    public int currentTurn;
    public bool turnWaiting;

    float timeForEnemyToMove = 1f;

    public GameObject uiButtons;

    //public BattleMoves[] battleMoves;

    //public GameObject slash;


    // Start is called before the first frame update
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
                    uiButtons.SetActive(true);
                }
                else
                {
                    uiButtons.SetActive(false);
                    StartCoroutine(EnemyMove());
                }

                
                
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.N))
            {
                
                nextTurn();
            }
#endif
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        int[] randomPlaces = new int[enemiesToSpawn.Length];
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            randomPlaces[i] = Random.Range(0, enemyPositions.Length);
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
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);


            // Временная затычка для проверки геймплея. Затем, если можно будет обзавестись спутниками, нужно будет переделать как для врагов.
            BattleChars player = Instantiate(playerPrefabs[0], playerPositions[0].position, playerPositions[0].rotation);
            player = GameManager.instance.playerBattleChars;
            activeBattlers.Add(player);
            activeBattlersFriendly.Add(0);
            






            //Спаун врагов на сцене боя
            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {


                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                    {
                        Debug.Log("Добавляем " + enemyPrefabs[j].charName);

                        BattleChars newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[enemyPlacesToSpawn[i]].position, enemyPositions[enemyPlacesToSpawn[i]].rotation);
                        newEnemy.transform.parent = enemyPositions[enemyPlacesToSpawn[i]];
                        activeBattlers.Add(newEnemy);
                        activeBattlersEnemy.Add(i);
                    }

                }

            }

            currentTurn = 0;
            turnWaiting = true;
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
        turnWaiting = true;

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
                activeBattlers[i].currentHP = 0;
                //Handle Dead battler
            }
            else
            {
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
            CloseBattle();
            return;
        }

        if (allEnemiesAreDead)
        {
            //End battle in Victory
            CloseBattle();
        }


    }

    private void CloseBattle()
    {
        GameManager.instance.battleIsActive = false;
        battleScene.SetActive(false);
        battleIsActive = false;
    }

    public IEnumerator EnemyMove()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(timeForEnemyToMove);
        EnemyAttack();
        yield return new WaitForSeconds(timeForEnemyToMove);
        nextTurn();
    }

    private void EnemyAttack()
    {
        int selectedTarget = activeBattlersFriendly[Random.Range(0, activeBattlersFriendly.Count)];

        activeBattlers[currentTurn].SelectRandomAttack(playerPositions[selectedTarget]);




        //activeBattlers[selectedTarget].currentHP -= 20;



    }
}
