using UnityEngine;

[System.Serializable]
public class InstantDamage : NewBuffBit
{
    public int mindamage;
    public int maxdamage;
    public TypeOfDamage typeOfDamage;
    public float vampPercent;
    


    //public override bool continious  => false; 

    public override void EffectStart(CharacterFacade attacker, CharacterFacade target)
    {
        int damage = Random.Range(mindamage, maxdamage+1);
        BattleCalculator.instance.ApplyDamage(attacker, target, damage, typeOfDamage, vampPercent);
    }

    public override void EffectEnd(CharacterFacade target)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
       
    }


}
