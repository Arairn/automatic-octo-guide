using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[Serializable]
public class CharacterStat
{
    public int BaseValue;
    
    private float _value;
    private List<StatModifier> statModifiers = new List<StatModifier>();


    void Awake()
    {
        statModifiers = new List<StatModifier>();
        statModifiers.Add(new StatModifier(0, StatModType.Flat));
    }
    public void Init()
    {
        statModifiers = new List<StatModifier>();

    }
    public int Value
    {
        
        get
        {
            if (LogController.instance.StatLog) Debug.Log("Кто-то спросил про стат!");

                _value = CalculateFinalValue();

            if (LogController.instance.StatLog) Debug.Log("Получилось " + _value);
            return (int)Math.Round(_value);
        }
    }


   
    public CharacterStat(int baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();

    }
 

    public void AddModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
        statModifiers.Sort(CompareModifierOrder);

    }

    public int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order > b.Order) return 1;
        else if (b.Order > a.Order) return -1;
        return 0;
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        statModifiers.Remove(statModifier);
        
    }
    public void RemoveAllModifiersFromSource(object source)
    {
        for (int i = statModifiers.Count - 1; i>= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                statModifiers.RemoveAt(i);
                
            }
        } 
    }

    private int CalculateFinalValue()
    {
        float finalValue = BaseValue;
        if (LogController.instance.StatLog) Debug.Log("База "+finalValue + " statModifiers" + statModifiers);
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            //Debug.Log("82");
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
                if (LogController.instance.StatLog) Debug.Log("сложение "+finalValue);
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if(i+1>=statModifiers.Count||statModifiers[i+1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    if (LogController.instance.StatLog) Debug.Log("аддитивный процент " + finalValue);
                }

            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1+mod.Value;
                if (LogController.instance.StatLog) Debug.Log("мульт " + finalValue);
            }
        }
        
        if(LogController.instance.StatLog) Debug.Log("итог " + finalValue);
        return (int)Math.Round(finalValue);
    }
}
