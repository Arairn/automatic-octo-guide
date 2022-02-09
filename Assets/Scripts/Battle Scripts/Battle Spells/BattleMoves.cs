using System.Collections.Generic;
using UnityEngine;


public enum TypeOfDamage
{
    physical,
    magical,
    combined,
    plain
}
public enum TargetsAvialable
{
    self,
    friendly,
    allFriends,
    friendlyNotSelf,
    enemy,
    allEnemies
}

/*

[System.Serializable]
public class ElementaryEffect
{



    public int mindamage;
    public int maxdamage;
    public TypeOfDamage typeOfDamage;
    public float vampPercent;
    public int manaDrain;
    public float manaVampPercent;
    public int bloodResourceRecovery;

}
[System.Serializable]
public class ElementarySummon

{
    public GameObject summoningObject;
    public List<StatModifier> summoningModifiers = new List<StatModifier>();
}




[CreateAssetMenu(fileName = "New Move", menuName = "BattleMove")]
public class BattleMoves : ScriptableObject
{

    public new string name;
    public int manaCost;
    public int specialCost;

    public bool targetShouldBeSelected;
    public TargetsAvialable targetsAvialable;
    public AttackEffect visualEffectOnTarget;
    public AttackEffect visualEffectOnSelf;

    public List<ElementaryEffect> elementaryEffects = new List<ElementaryEffect>();
    public List<OldBattleBuff> elementaryBuffs = new List<OldBattleBuff>();
    public List<ElementarySummon> elementarySummon = new List<ElementarySummon>();
    public SpecialBattleMoveEffect specialEffect;
    

    public void ApplyMove(CharacterFacade currentCharFacade, CharacterFacade target)
    {

        foreach (var item in elementaryEffects)
        {
            int damage;
            if (item.mindamage != 0)
            {
                damage = Random.Range(item.mindamage, item.maxdamage);
            }
            else damage = item.maxdamage;
            BattleCalculator.instance.ApplyDamage(currentCharFacade, target, damage, item.typeOfDamage, item.vampPercent);
            Debug.LogWarning("пока не работает");
            //BattleCalculator.instance.ApplyManaDrain(currentCharFacade, target, item.manaDrain, item.manaVampPercent);
        }

        foreach (var item in elementaryBuffs)
        {
            //target.AddBuff(item);
        }

        foreach (var item in elementarySummon)
        {
            Debug.Log("пока не учитываются модификаторы суммона");
            Debug.LogWarning("пока не работает");
            //BattleManager.instance.Summon(item.summoningObject);
        }


        DisplayMove(currentCharFacade.transform, target.transform);

    }



    void DisplayMove(Transform self, Transform target)
    {

        if (visualEffectOnTarget)
        {

            Instantiate(visualEffectOnTarget, target.position, target.rotation);
        }
        if (visualEffectOnSelf)
        {

            Instantiate(visualEffectOnSelf, self.position, self.rotation);
        }
        else
        {
            if (LogController.instance.BattleAnimationsLog)
            {
                Debug.Log("BattleMove не имеет анимации применения");
            }
        }
    }


}

*/



    