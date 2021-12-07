using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMoveType
{
    damage,
    heal,
    summon
}

[CreateAssetMenu(fileName ="New Move", menuName = "BattleMove")]
public class BattleMoves : ScriptableObject//, IBattleMoves
{
    //public BattleMoveType type;
    public new string name;
    public int movePower;
    public int manaCost;
    public int specialCost;
    public AttackEffect visualEffectOnTarget;


    public void DisplayMove(Transform target)
    {
        if (visualEffectOnTarget)
        {

            Instantiate(visualEffectOnTarget, target.position, target.rotation);
        }
        else Debug.Log("BattleMove не имеет анимации применения");
    }
}
