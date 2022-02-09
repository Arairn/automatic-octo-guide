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

    public void InvokeBattleChanges()
    {
        BattlersHaveChanged?.Invoke();
    }

    private void EnemyAttack()
    {
        BattleManager.instance.activeBattlers[currentTurn].StartEnemyTurn();


    }
    public void NextTurn()
    {
        if(LogController.instance.BattleTurns) Debug.Log("Заканчиваем ход " + currentTurn);

        BattleManager.instance.activeBattlers[currentTurn].CheckBuffs();
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
        if (LogController.instance.BattleTurns) Debug.Log("Начинаем ход " + currentTurn);
        highLight.JumpTo(BattleManager.instance.activeBattlers[currentTurn].GetComponent<Transform>());
        if (LogController.instance.BattleMarkerLog)
        {
            Debug.Log("Попытались перенести синий маркер на " + BattleManager.instance.activeBattlers[currentTurn].name);
        }
        
        BattleManager.instance.UpdateBattle();

        if(BattleManager.instance.activeBattlers[currentTurn].GetStat(CharacterStatsEnum.currentHP) <= 0)
        {
            NextTurn();
            return;
        }

        //Проверяем баффы начала хода
        //BattleManager.instance.activeBattlers[currentTurn].CheckBuffs(true);


        if (BattleManager.instance.activeBattlers[currentTurn].isPlayer)
        {
            playerBattleMenu.ShowButtons();
        }
        else if (!BattleManager.instance.activeBattlers[currentTurn].isPlayer)
        {
            StartCoroutine(EnemyMove()); 
        }
        //BattlersHaveChanged?.Invoke();
    } 
    public void StartTurn(bool newBattle)
    {
        if (newBattle)
        {
            currentTurn = 0;
        }
        StartTurn();
        BattlersHaveChanged?.Invoke();

    }

    public IEnumerator EnemyMove()
    {
        if (LogController.instance.BattleTurns) Debug.Log("Начинаем корутину следующего хода");
            yield return new WaitForSeconds(timeForEnemyToMove);
        EnemyAttack();
        BattlersHaveChanged?.Invoke();
        yield return new WaitForSeconds(timeForEnemyToMove);

        NextTurn();
    }
    public void NextTurnFromPlayer()
    {
        StartCoroutine(WaitForEndOfFrameForTheNextTurn());
    }
    public IEnumerator WaitForEndOfFrameForTheNextTurn()
    {
        yield return new WaitForSeconds(timeForEnemyToMove);
        NextTurn();
    }


}
