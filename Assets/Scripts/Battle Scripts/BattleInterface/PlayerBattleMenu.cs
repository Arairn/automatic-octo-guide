using UnityEngine;

public class PlayerBattleMenu : MonoBehaviour
{
    public BattleButtonsManager battleButtonsManager;
    //CharacterFacade currentCharFacade;
    public GameObject notification;
    [SerializeField]
    BattleTurnManager battleTurnManager;

    BattleMoves[] damageMoves, spawnMoves, curseMoves;

    CharacterFacade currentCharFacade;
    enum currentActionState
    {
        isWaitingForSchool,
        isWaitingForAction,
        isWaitingForBattleMove,
        isWaitingForTarget
    }
    currentActionState actionState;
    BattleMoves[] movesToSelectFrom;


    BattleMoves selectedBattleMove;
    CharacterFacade target;
    private void Start()
    {
        if (!battleTurnManager)
        {
            Debug.LogWarning("Не назначен battleTurnManager для PlayerBattleMenu");
        }
    }


    public void ShowButtons() //Начало боя, спауним кнопки Атака, спаун и керс, в зависимости от их наличия у ходящего
    {
        battleButtonsManager.Clean();
        actionState = currentActionState.isWaitingForAction;
        GetMoves();
        //currentCharFacade = BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn];


        if (damageMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Attack", 1);
        }
        if (spawnMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Spawn", 2);
        }
        if (curseMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Curse", 3);
        }

    }

    // Update is called once per frame
    public void OnButtonPressed(int i) // В зависимости от текущего состояния, и номера кнопки i, вызываем свитч
    {

        battleButtonsManager.Clean();
        GetMoves();
        //currentCharFacade = BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn];
        if (LogController.BattleInterfaceLog)
        {
            Debug.Log("" + BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn] + "" + damageMoves[0].name + " из "); //+ movesToSelectFrom[j].name);


            Debug.Log("Нажата кнопка, " + i + "; текущее состояние " + actionState);
        }

        switch (actionState)
        {
            case currentActionState.isWaitingForSchool:
                break;

            case currentActionState.isWaitingForAction:

                if (i == 1)
                {

                    movesToSelectFrom = new BattleMoves[damageMoves.Length];

                    for (int j = 0; j < damageMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (damageMoves[j]);
                        if (LogController.BattleInterfaceLog)
                        {
                            Debug.Log("checked battleMoves, got " + movesToSelectFrom[j].name);
                            Debug.Log("currentBattleChars.battleMoves.Length = " + damageMoves.Length);
                        }
                    }


                }
                if (i == 2)
                {
                    movesToSelectFrom = new BattleMoves[spawnMoves.Length];

                    for (int j = 0; j < spawnMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (spawnMoves[j]);
                    }
                    //BattleMoves[] movesToSelectFrom = currentBattleChar.spawnMoves;

                }
                if (i == 3)
                {
                    movesToSelectFrom = new BattleMoves[curseMoves.Length];
                    for (int j = 0; j < curseMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (curseMoves[j]);
                    }
                    // _ = currentBattleChar.curseMoves;

                }


                //Debug.Log(movesToSelectFrom.name);
                for (int j = 0; j < movesToSelectFrom.Length; j++)
                {

                    battleButtonsManager.SpawnButton(movesToSelectFrom[j].name, j);
                }

                actionState = currentActionState.isWaitingForBattleMove;




                break;
            case currentActionState.isWaitingForBattleMove:
                selectedBattleMove = movesToSelectFrom[i];

                if (selectedBattleMove.manaCost > BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn].GetStat(CharacterStatsEnum.currentMP))
                {
                    Instantiate(notification, this.transform).GetComponent<BattleNotification>().SetNotification("Not Enough Mana");
                    ShowButtons();

                    return;
                }
                //movesToSelectFrom.Clear();
                for (int j = 0; j < BattleManager.instance.activeBattlersEnemy.Count; j++)
                {
                    battleButtonsManager.SpawnButton(BattleManager.instance.activeBattlers[BattleManager.instance.activeBattlersEnemy[j]].GetName(), j);
                }

                actionState = currentActionState.isWaitingForTarget;
                break;

            case currentActionState.isWaitingForTarget:

                target = BattleManager.instance.activeBattlers[BattleManager.instance.activeBattlersEnemy[i]];

                selectedBattleMove.ApplyMove(currentCharFacade, target);
                //BattleCalculator.instance.ApplyDamage(currentCharFacade, target, selectedBattleMove.movePower, selectedBattleMove.);

                battleTurnManager.NextTurn();




                break;
            default:
                Debug.Log("swich goes wrong");
                break;
        }

    }

    void GetMoves()
    {
        currentCharFacade = BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn];
        

        damageMoves = new BattleMoves[currentCharFacade.CharacterBattleStatsSystem.damageMoves.Length];

        for (int j = 0; j < currentCharFacade.CharacterBattleStatsSystem.damageMoves.Length; j++)
        {
            damageMoves[j] = (currentCharFacade.CharacterBattleStatsSystem.damageMoves[j]);
        }


        spawnMoves = new BattleMoves[currentCharFacade.CharacterBattleStatsSystem.spawnMoves.Length];

        for (int j = 0; j < currentCharFacade.CharacterBattleStatsSystem.spawnMoves.Length; j++)
        {
            spawnMoves[j] = (currentCharFacade.CharacterBattleStatsSystem.spawnMoves[j]);
        }

        curseMoves = new BattleMoves[currentCharFacade.CharacterBattleStatsSystem.curseMoves.Length];

        for (int j = 0; j < currentCharFacade.CharacterBattleStatsSystem.curseMoves.Length; j++)
        {
            curseMoves[j] = (currentCharFacade.CharacterBattleStatsSystem.curseMoves[j]);
        }


    }
}
