using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Move", menuName = "BattleMove")]
public class BattleMoves : ScriptableObject//, IBattleMoves
{

    public new string name;
    public int movePower;
    public int manaCost;
    public int specialCost;
    public AttackEffect effect;


    public void DisplayMove(Transform target)
    {
        Instantiate(effect, target.position, target.rotation);
    }
}
