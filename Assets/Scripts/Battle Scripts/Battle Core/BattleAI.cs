using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{
    int target,targetNumber;
    CharacterFacade thisCharacterFacade;

    private void Start()
    {
        thisCharacterFacade = GetComponent<CharacterFacade>();
    }

    CharacterFacade SelectTarget()
    {
        targetNumber = BattleManager.instance.activeBattlersFriendly[Random.Range(0, BattleManager.instance.activeBattlersFriendly.Count)];
        target = BattleManager.instance.activeBattlersFriendly[targetNumber];
        return BattleManager.instance.activeBattlers[target];
    }

    Spell SelectAttack()
    {
        Spell[] damagemovesAvaliable = BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn].CharacterBattleStatsSystem.damageMoves;
        int attack = Random.Range(0, damagemovesAvaliable.Length);
        //battlemovesAvaliable[attack].DisplayMove(BattleManager.instance.playerPositions[target]);
        if (LogController.instance.BattleDamageLog)
        {
            Debug.Log(BattleManager.instance.activeBattlers[BattleTurnManager.currentTurn] + " had selected " + damagemovesAvaliable[attack].name + " and going to run it");
        }
            return damagemovesAvaliable[attack];
    }

    public void AITurn()
    {
        SelectAttack().ApplyMove(thisCharacterFacade,SelectTarget());
        //BattleCalculator.instance.CalculateDamage(BattleTurnManager.currentTurn, SelectTarget(), SelectAttack());
    }
}
