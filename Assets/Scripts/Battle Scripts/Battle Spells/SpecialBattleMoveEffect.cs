using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialBattleMoveEffect
{
    public abstract void ApplySpecialEffect();
}




public class DestroyAllEnemies : SpecialBattleMoveEffect
{
    public override void ApplySpecialEffect()
    {
        Debug.Log("Мы как будто убили всех врагов");
    }
}