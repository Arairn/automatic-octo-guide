using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculator : MonoBehaviour
{

    public static BattleCalculator instance;
    public ArmorStats armorStats;
    public List<CharacterFacade> activeBattlers = new List<CharacterFacade>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        activeBattlers = BattleManager.instance.activeBattlers;
        armorStats.Init();
    }
    

    public void ApplyDamage(CharacterFacade attacker, CharacterFacade defender, int damage, TypeOfDamage type, float vampPercent)
    {

        int _power = attacker.GetStat(CharacterStatsEnum.power, type);
        int _penetration = attacker.GetStat(CharacterStatsEnum.penetration, type);

        ApplyDamage(attacker, defender,damage, type, vampPercent, _power, _penetration);

    }

    public void ApplyDamage(CharacterFacade attacker, CharacterFacade defender, int damage, TypeOfDamage type, float vampPercent, int power, int penetration)
    {
        float coeff;

            coeff = CalculateCoefficient(attacker, defender, type, power, penetration);



            if (LogController.instance.BattleDamageLog) Debug.Log("Калькулятор пытается применить " + damage + " урона, типа " + type + " от " + attacker.GetName() + " к " + defender.GetName() + "; Коэффициент проникновения " + coeff);

                defender.ChangeHP(-(int)(coeff * damage));
                attacker.ChangeHP((int)(coeff * damage * vampPercent));

       



    }

    

    float CalculateCoefficient(CharacterFacade attacker, CharacterFacade defender, TypeOfDamage type, int power, int penetration)
    {
        int defence = defender.GetStat(CharacterStatsEnum.defence,type);
        float armorDef;
        

        
        armorDef = 1-armorStats.ArmorResist(defence - penetration);
        Debug.Log("Считали коэффициент, вышло " + power + " * " + armorDef +" = "+ defence+ "-"+ penetration);
        return power * armorDef;
    }
    /*
    int GetPower(CharacterFacade attacker, TypeOfDamage type)
    {
        switch (type)
        {
            case TypeOfDamage.physical:
                return attacker.GetStat(CharacterStatsEnum.physicalPower);
            case TypeOfDamage.magical:
                return attacker.GetStat(CharacterStatsEnum.magicPower);

            case TypeOfDamage.combined:
                return (attacker.GetStat(CharacterStatsEnum.physicalPower) + attacker.GetStat(CharacterStatsEnum.magicPower)) / 2;
        }
                return 1;

    }
    int GetPenetration(CharacterFacade attacker, TypeOfDamage type)
    {
        switch (type)
        {
            case TypeOfDamage.physical:
                return attacker.GetStat(CharacterStatsEnum.physicalPenetration);
            case TypeOfDamage.magical:
                return attacker.GetStat(CharacterStatsEnum.magicalPenetration);

            case TypeOfDamage.combined:
                return (attacker.GetStat(CharacterStatsEnum.physicalPenetration) + attacker.GetStat(CharacterStatsEnum.magicalPenetration)) / 2;
        }
        return 1;
    }
    */
    /*
    float CalculateCoefficient(CharacterFacade attacker, CharacterFacade defender, TypeOfDamage type)
    {
        int power=0, defence=0, penetration=0;
        float armorDef;
        switch (type)
        {
            case TypeOfDamage.physical:
                power = attacker.GetStat(CharacterStatsEnum.physicalPower);
                //defence = defender.GetStat(CharacterStatsEnum.physicalDefence);
                penetration = attacker.GetStat(CharacterStatsEnum.physicalPenetration);
                break;
            case TypeOfDamage.magical:
                power = attacker.GetStat(CharacterStatsEnum.magicPower);
                //defence = defender.GetStat(CharacterStatsEnum.magicalDefence);
                penetration = attacker.GetStat(CharacterStatsEnum.magicalPenetration);
                break;
            case TypeOfDamage.combined:
                power = (attacker.GetStat(CharacterStatsEnum.physicalPower)+attacker.GetStat(CharacterStatsEnum.magicPower))/2;
                //defence = (defender.GetStat(CharacterStatsEnum.physicalDefence)+defender.GetStat(CharacterStatsEnum.magicalDefence))/2;
                penetration = (attacker.GetStat(CharacterStatsEnum.physicalPenetration)+attacker.GetStat(CharacterStatsEnum.magicalPenetration))/2 ;
                break;
            case TypeOfDamage.plain:
                return 1;
            default:
                Debug.Log("Вышли из свитча, type = "+type+", и что-то явно пошло не так");
                break;
        }
    */

        //return CalculateCoefficient(attacker, defender, type, power, penetration);
        
        
    }

    



