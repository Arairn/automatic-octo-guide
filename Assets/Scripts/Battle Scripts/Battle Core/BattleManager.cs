using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Battle Settings")]
    private bool battleIsActive;
    public GameObject battleScene;

    [Header("Battle Visual")]

    //Это все для включения боя

    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    
    public DamageText damageText;
    public DamageText healText;



    [Header("Battle Math")]

    public GameObject playerPrefab;
    public CharacterFacade[] playerPrefabs;
    public CharacterFacade[] enemyPrefabs;

    public List<CharacterFacade> activeBattlers = new List<CharacterFacade>();
    public List<int> activeBattlersFriendly = new List<int>();
    public List<int> activeBattlersEnemy = new List<int>();

    public CharacterFacade player;
    [Header("Battle UI")]

    //public GameObject uiButtons;
    public BattleTurnManager battleTurnManager;
    public GameObject playerInfoParent, enemyInfoParent;
    public GameObject battlerInfo;
    public PlayerBattleMenu playerBattleMenu;

    List<int> placesPool;

    

    public static event Action BattleHasEnded;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        BattleTurnManager.BattlersHaveChanged += UpdateBattle;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y); //Немного геморойно, но т.к. камера не жестко привязана к персонажу, она может открывать бой с небольшим сдвигом. Так - работает.
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Eyeball", "Spider","Dummi" });

        }
#endif

    }


    public List<int> PlacesPool(Transform[] positions)
    {
        placesPool = new List<int>();
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].childCount == 0)
            {
                placesPool.Add(i);

            }
        }

        if(placesPool.Count == 0)
        {
            Debug.LogWarning("Невозможно найти пустое место!");
            placesPool.Add(UnityEngine.Random.Range(0, positions.Length));
        }

        return placesPool;

    }
    public void BattleStart(string[] enemiesToSpawn)
    {

        int[] randomPlaces = new int[enemiesToSpawn.Length];
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {

            List<int> placesPool = PlacesPool(enemyPositions);
            randomPlaces[i] = placesPool[UnityEngine.Random.Range(0, placesPool.Count)];
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
            //newBattlePlayer.GetComponent<PlayerBattleInfoPrefabUpdater>().Renew(true);
            player = newBattlePlayer.GetComponent<CharacterFacade>();
            //player = GameManager.instance.playerFacade;

            activeBattlers.Add(player);
            //activeBattlersFriendly.Add(0);
            Instantiate(battlerInfo).GetComponent<BattlerInfoUpdater>().SetParams(0,playerInfoParent);
            //highLight.JumpTo(playerPositions[0]);
            //highLight.transform.position = playerPositions[0].position;


            //Нужно добавить проверку на повтор места, чтобы 2 врага не могли попасть на уже занятую клетку.

            //Спаун врагов на сцене боя
            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {

                SpawnBattler(enemiesToSpawn[i], enemyPositions[enemyPlacesToSpawn[i]]);
                /*
                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].name == enemiesToSpawn[i])
                    {
                        if (LogController.BattleSpawnLog)
                        {
                            Debug.Log("Добавляем " + enemyPrefabs[j].name);
                        }

                        CharacterFacade newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[enemyPlacesToSpawn[i]].position, enemyPositions[enemyPlacesToSpawn[i]].rotation);
                        
                        newEnemy.transform.parent = enemyPositions[enemyPlacesToSpawn[i]];

                        activeBattlers.Add(newEnemy);
                        Instantiate(battlerInfo).GetComponent<BattlerInfoUpdater>().SetParams(activeBattlers.Count-1, enemyInfoParent);
                        //activeBattlersEnemy.Add(i);
                    }

                }
                */

            }

            

            
            UpdateBattle();
            battleTurnManager.StartTurn(true);
        }
    }

    public void SpawnBattler(string name, Transform place)
    {
        for (int j = 0; j < enemyPrefabs.Length; j++)
        {
            if (enemyPrefabs[j].name == name)
            {
                SpawnBattler(enemyPrefabs[j], place);


            }

        }
    }
    public void SpawnBattler(CharacterFacade character, Transform place)
    {
        if (LogController.instance.BattleSpawnLog)
        {
            Debug.Log("Добавляем " + character.name);
        }
        CharacterFacade newEnemy = Instantiate(character, place.position, place.rotation);

        newEnemy.transform.parent = place;

        activeBattlers.Add(newEnemy);
        Instantiate(battlerInfo).GetComponent<BattlerInfoUpdater>().SetParams(activeBattlers.Count - 1, enemyInfoParent);
        //activeBattlersEnemy.Add(i);
    }


    public void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        activeBattlersEnemy.Clear();
        activeBattlersFriendly.Clear();

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].GetStat(CharacterStatsEnum.currentHP) <= 0)
            {
                activeBattlers[i].SetToDead();

            }
            else
            {
                activeBattlers[i].SetToRevived();
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
            Debug.Log("Поражение!");
            CloseBattle();
            return;
        }

        if (allEnemiesAreDead)
        {
            Debug.Log("Победа!");
            //End battle in Victory
            CloseBattle();
        }

    }

    private void CloseBattle()
    {
        if (battleIsActive) {
            Debug.Log("Битва закончена");
            BattleHasEnded?.Invoke();
            StartCoroutine(GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().BlackAndGone());
            StartCoroutine(EndBattle(GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().transitionTime));
            }
        battleIsActive = false;
        
    }


    



    IEnumerator EndBattle(float transitionTime)
    {
        GameManager.instance.battleIsActive = false;
        //BattleHasEnded?.Invoke();
        yield return new WaitForSeconds(transitionTime);
        //GameManager.instance.LinkToBattleChars();

        
        foreach (var item in activeBattlers)
        {
            Debug.Log("Удаляем " + item.gameObject.name+ " после битвы");
            Destroy(item.gameObject);
            
        }
        battleScene.SetActive(false);
        
        yield return new WaitForSeconds(transitionTime);
        //GameManager.instance.battleIsActive = false;

    }
}
