using System.Collections.Generic;

[System.Serializable]
public class BattleBuff
{
    public CharacterFacade facade;
    public static int MaxBuffID;
    public int BuffID;
    

    public string buffName;
    public string Description;
    public int buffDuration;


    public StatModifier buffStatModifier;


    public int damagePerTurn;
    public int damageInTheEnd;
    public float vampPercent;
    public int manaDrainPerTurn;
    public float manaVampPercent;
    public TypeOfDamage typeOfDamage;
    public bool triggersBeforeTargetsTurn = false;


    public bool displayThiSBuff;

    public float armorpiersing, attack;


    public void SetFacade (CharacterFacade characterFacade)
    {
        facade = characterFacade;

    }
    public void Expire()
    {
        buffDuration--;
        if(buffDuration <= 0)
        {
            if(damageInTheEnd != 0)
            {
                //BattleCalculator.instance.ApplyDamage();
                //facade.DealDamage(damageInTheEnd, typeOfDamage, vampPercent);
                
            }





            facade.RemoveBuff(this);
        }
    }
    public void CheckBuff()
    {

    }

}
