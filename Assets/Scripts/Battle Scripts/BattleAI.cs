using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{
    int target,targetNumber;
    public int SelectTarget()
    {
        targetNumber = BattleManager.instance.activeBattlersFriendly[Random.Range(0, BattleManager.instance.activeBattlersFriendly.Count)];
        target = BattleManager.instance.activeBattlersFriendly[targetNumber];
        return target;
    }

    public BattleMoves SelectAttack()
    {
        BattleMoves[] battlemovesAvaliable = BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].battleMoves;
        int attack = Random.Range(0, battlemovesAvaliable.Length);
        //battlemovesAvaliable[attack].DisplayMove(BattleManager.instance.playerPositions[target]);
        
        Debug.Log(BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn] + " had selected " + battlemovesAvaliable[attack].name + " and dealing " + battlemovesAvaliable[attack].movePower + " damage");
        return battlemovesAvaliable[attack];
    }    

}
