using UnityEngine;

public class PlayerBattleMenu : MonoBehaviour
{
    public BattleButtonsManager battleButtonsManager;
    BattleChars currentBattleChars;
    public GameObject notification;
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
    int target;
    // Start is called before the first frame update


    public void ShowButtons()
    {
        battleButtonsManager.Clean();
        actionState = currentActionState.isWaitingForAction;
        currentBattleChars = BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn];

        if (currentBattleChars.battleMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Attack", 1);
        }
        if (currentBattleChars.spawnMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Spawn", 2);
        }
        if (currentBattleChars.curseMoves.Length > 0)
        {
            battleButtonsManager.SpawnButton("Curse", 3);
        }

    }

    // Update is called once per frame
    public void OnButtonPressed(int i)
    {

        battleButtonsManager.Clean();
        currentBattleChars = BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn];
        Debug.Log("" + BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn] + "" + currentBattleChars.battleMoves[0].name + " из "); //+ movesToSelectFrom[j].name);


        Debug.Log("Нажата кнопка, " + i + "; текущее состояние " + actionState);

        switch (actionState)
        {
            case currentActionState.isWaitingForSchool:
                break;

            case currentActionState.isWaitingForAction:

                if (i == 1)
                {

                    movesToSelectFrom = new BattleMoves[currentBattleChars.battleMoves.Length];

                    for (int j = 0; j < currentBattleChars.battleMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (currentBattleChars.battleMoves[j]);
                        Debug.Log("checked battleMoves, got " + movesToSelectFrom[j].name);
                        Debug.Log("currentBattleChars.battleMoves.Length = " + currentBattleChars.battleMoves.Length);

                    }


                }
                if (i == 2)
                {
                    movesToSelectFrom = new BattleMoves[currentBattleChars.spawnMoves.Length];

                    for (int j = 0; j < currentBattleChars.spawnMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (currentBattleChars.spawnMoves[j]);
                    }
                    //BattleMoves[] movesToSelectFrom = currentBattleChar.spawnMoves;

                }
                if (i == 3)
                {
                    movesToSelectFrom = new BattleMoves[currentBattleChars.curseMoves.Length];
                    for (int j = 0; j < currentBattleChars.curseMoves.Length; j++)
                    {
                        movesToSelectFrom[j] = (currentBattleChars.curseMoves[j]);
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

                if (selectedBattleMove.manaCost > currentBattleChars.currentMP)
                {
                    Instantiate(notification, this.transform).GetComponent<BattleNotification>().SetNotification("Not Enough Mana");
                    ShowButtons();

                    return;
                }
                //movesToSelectFrom.Clear();
                for (int j = 0; j < BattleManager.instance.activeBattlersEnemy.Count; j++)
                {
                    battleButtonsManager.SpawnButton(BattleManager.instance.activeBattlers[BattleManager.instance.activeBattlersEnemy[j]].name, j);
                }

                actionState = currentActionState.isWaitingForTarget;
                break;

            case currentActionState.isWaitingForTarget:

                target = BattleManager.instance.activeBattlersEnemy[i];
                BattleManager.instance.CalculateDamage(target, selectedBattleMove);
                BattleManager.instance.nextTurn();




                break;
            default:
                Debug.Log("swich goes wrong");
                break;
        }

    }
}
