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
        float coeff = CalculateCoefficient(attacker, defender, type);
        defender.DealDamage((int)(coeff*damage));
        attacker.Heal((int)(coeff * damage * vampPercent));
    }

    public void ApplyDamage(int power, int penetration, float vampPercent, CharacterFacade attacker, CharacterFacade defender, int damage, TypeOfDamage type)
    {

        float coeff = CalculateCoefficient(power, penetration, attacker, defender, type);
        defender.DealDamage((int)(coeff * damage));
        attacker.Heal((int)(coeff * damage * vampPercent));
    }



    float CalculateCoefficient(int power, int penetration, CharacterFacade attacker, CharacterFacade defender, TypeOfDamage type)
    {
        int defence = 0;
        float armorDef;
        switch (type)
        {
            case TypeOfDamage.physical:
                defence = defender.GetStat(CharacterStatsEnum.physicalDefence);
                break;
            case TypeOfDamage.magical:
                defence = defender.GetStat(CharacterStatsEnum.magicalDefence);
                break;
            case TypeOfDamage.combined:
                defence = (defender.GetStat(CharacterStatsEnum.physicalDefence) + defender.GetStat(CharacterStatsEnum.magicalDefence)) / 2;
                break;
            case TypeOfDamage.plain:
                return 1;
            default:
                Debug.Log("Вышли из свитча, type = " + type + ", и что-то явно пошло не так");
                break;
        }

        armorDef = 1f-armorStats.ArmorResist(defence - penetration);
        return power * armorDef;
    }
    float CalculateCoefficient(CharacterFacade attacker, CharacterFacade defender, TypeOfDamage type)
    {
        int power=0, defence=0, penetration=0;
        float armorDef;
        switch (type)
        {
            case TypeOfDamage.physical:
                power = attacker.GetStat(CharacterStatsEnum.physicalPower);
                defence = defender.GetStat(CharacterStatsEnum.physicalDefence);
                penetration = attacker.GetStat(CharacterStatsEnum.physicalPenetration);
                break;
            case TypeOfDamage.magical:
                power = attacker.GetStat(CharacterStatsEnum.magicPower);
                defence = defender.GetStat(CharacterStatsEnum.magicalDefence);
                penetration = attacker.GetStat(CharacterStatsEnum.magicalPenetration);
                break;
            case TypeOfDamage.combined:
                power = (attacker.GetStat(CharacterStatsEnum.physicalPower)+attacker.GetStat(CharacterStatsEnum.magicPower))/2;
                defence = (defender.GetStat(CharacterStatsEnum.physicalDefence)+defender.GetStat(CharacterStatsEnum.magicalDefence))/2;
                penetration = (attacker.GetStat(CharacterStatsEnum.physicalPenetration)+attacker.GetStat(CharacterStatsEnum.magicalPenetration))/2 ;
                break;
            case TypeOfDamage.plain:
                return 1;
            default:
                Debug.Log("Вышли из свитча, type = "+type+", и что-то явно пошло не так");
                break;
        }

        armorDef = armorStats.ArmorResist(defence-penetration);
        return power * armorDef;
        
    }




}
