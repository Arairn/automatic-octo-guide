using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnManager : MonoBehaviour
{

    public PlayerBattleMenu playerBattleMenu;
    public static int currentTurn;
    public MarkerJumper highLight;
    float timeForEnemyToMove = 1f;
    public static event Action BattlersHaveChanged;



    private void EnemyAttack()
    {
        BattleManager.instance.activeBattlers[currentTurn].StartEnemyTurn();


    }
    public void NextTurn()
    {
        if(LogController.BattleTurns) Debug.Log("Заканчиваем ход " + currentTurn);

        BattleManager.instance.activeBattlers[currentTurn].CheckBuffs(false);
        BattlersHaveChanged?.Invoke();

        if (currentTurn >= BattleManager.instance.activeBattlers.Count - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
        StartTurn();

    }
    public void StartTurn()
    {
        if (LogController.BattleTurns) Debug.Log("Начинаем ход " + currentTurn);
        highLight.JumpTo(BattleManager.instance.activeBattlers[currentTurn].GetComponent<Transform>());
        if (LogController.BattleMarkerLog)
        {
            Debug.Log("Попытадись перенести синий маркер на " + BattleManager.instance.activeBattlers[currentTurn].name);
        }
        
        BattleManager.instance.UpdateBattle();

        if(BattleManager.instance.activeBattlers[currentTurn].GetStat(CharacterStatsEnum.currentHP) <= 0)
        {
            NextTurn();
            return;
        }

        //Проверяем баффы начала хода
        BattleManager.instance.activeBattlers[currentTurn].CheckBuffs(true);


        if (BattleManager.instance.activeBattlers[currentTurn].isPlayer)
        {
            playerBattleMenu.ShowButtons();
        }
        else if (!BattleManager.instance.activeBattlers[currentTurn].isPlayer)
        {
            StartCoroutine(EnemyMove()); 
        }
        BattlersHaveChanged?.Invoke();
    } 
    public void StartTurn(bool newBattle)
    {
        if (newBattle)
        {
            currentTurn = 0;
        }
        StartTurn();

    }

    public IEnumerator EnemyMove()
    {
        if (LogController.BattleTurns) Debug.Log("Начинаем корутину следующего хода");
            yield return new WaitForSeconds(timeForEnemyToMove);
        EnemyAttack();
        BattlersHaveChanged?.Invoke();
        yield return new WaitForSeconds(timeForEnemyToMove);

        NextTurn();
    }


}
