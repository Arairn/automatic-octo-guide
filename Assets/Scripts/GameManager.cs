using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterFacade playerFacade;
    public CharacterBattleStatsSystem playerBattleChars;

    public bool gameMenuOpen, dialogueActive, fadingbetweenAreas, battleIsActive;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        playerFacade = GetComponent<CharacterFacade>();
        playerBattleChars = GetComponent<CharacterBattleStatsSystem>();
        //LinkToBattleChars();


    }
    /*
    public void LinkToBattleChars()
    {
        playerBattleChars = GetComponent<CharacterBattleStatsSystem>();
        if (LogController.PlayerStatsMagicLog)
        {
            Debug.Log("Is linking to " + GetComponent<CharacterBattleStatsSystem>());
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogueActive || fadingbetweenAreas || battleIsActive) PlayerController.instance.StopMoving();
        else PlayerController.instance.StartMoving();
    }



        public void ReturnFromBattle()
    {
        //GetComponent<PlayerBattleInfoPrefabUpdater>().Renew(false);
        //instance.playerBattleChars = gameObject.AddComponent(BattleManager.instance.activeBattlers[0]);

    }

    
}
