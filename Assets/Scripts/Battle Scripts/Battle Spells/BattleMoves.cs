using System.Collections;
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
        
[System.Serializable]
public class ElementaryEffect
{

    


    public int damage;
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




[CreateAssetMenu(fileName ="New Move", menuName = "BattleMove")]
public class BattleMoves : ScriptableObject
{
    //public BattleMoveType type;
    public new string name;
    //public int movePower;
    public int manaCost;
    public int specialCost;

    public bool targetShouldBeSelected;
    public TargetsAvialable targetsAvialable;
    public AttackEffect visualEffectOnTarget;

    public List<ElementaryEffect> elementaryEffects = new List<ElementaryEffect>();
    public List<BattleBuff> elementaryBuffs = new List<BattleBuff>();
    public List<ElementarySummon> elementarySummon = new List<ElementarySummon>();
    public SpecialBattleMoveEffect specialEffect;

    CharacterFacade caster;

    public void ApplyMove(CharacterFacade currentCharFacade, CharacterFacade target) 
    {

        foreach (var item in elementaryEffects)
        {
            BattleCalculator.instance.ApplyDamage(currentCharFacade, target, item.damage, item.typeOfDamage, item.vampPercent);
            //BattleCalculator.instance.ApplyManaDrain(currentCharFacade, target, item.manaDrain, item.manaVampPercent);
        }

        foreach (var item in elementaryBuffs)
        {
            target.AddBuff(item);
        }



            foreach (var item in elementarySummon)
        {
            Debug.Log("пока не учитываются модификаторы суммона");
            //BattleManager.instance.Summon(item.summoningObject);
            
        }





            DisplayMove(target.transform);

    }
    void Start()
    {
        //caster = GetComponent<CharacterFacade>();
    }


    void DisplayMove(Transform target)
    {

        if (visualEffectOnTarget)
        {

            Instantiate(visualEffectOnTarget, target.position, target.rotation);
        }
        else
        {
            if (LogController.BattleAnimationsLog)
            {
                Debug.Log("BattleMove не имеет анимации применения");
            }
        }
    }



    



}
