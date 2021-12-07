using System;
using System.Collections.Generic;

[Serializable]
public class CharacterStat
{
    public float BaseValue;
    private bool isDirty = true;
    private float _value;

    public float Value
    {
        get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    private readonly List<StatModifier> statModifiers;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
        isDirty = true;
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
        isDirty = true;
    }
    public void RemoveAllModifiersFromSource(object source)
    {
        for (int i = statModifiers.Count - 1; i>= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                statModifiers.RemoveAt(i);
                isDirty = true;
            }
        } 
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if(i+1>=statModifiers.Count||statModifiers[i+1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                }

            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1+mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 3);
    }
}
