using System;
using UnityEngine;

public enum CharacterStatsEnum
{
    currentHP,
    maxHP,
    currentMP,
    maxMP,
    currentEXP,
    maxExp,
    /*
    physicalPower,
    magicPower,
    physicalDefence,
    magicalDefence,
    physicalPenetration,
    magicalPenetration,
    */
    power,
    defence,
    penetration
        
}

public enum ModStat
{
    PhysPower,
    MagPower,
    PhysDef,
    MagDef,
    PhysPen,
    MagPen
}

[Serializable]
public class Points
{
    [SerializeField]
    int current;
    [SerializeField]
    int max;
    /*
    public void Change(int amount)
    {
        Change(amount, false);
    }
    */
    public void Change(int amount,bool overCap = false)
    {
        current += amount;
        if (current > max&&!overCap) current = max;
    }
    public void ChangeMax(int amount, bool refill)
    {
        max = +amount;
        if (refill)
        {
            current = max;
        }
    }
    public int Get(bool ifmax)
    {
        if (ifmax) return max;
        else return current;
    }

    public Points(int _current, int _max)
    {
        current = _current;
        max = _max;
    }
    public Points(int _max)
    {
        current = max = _max;
    }
}

public class CharacterBattleStatsSystem : MonoBehaviour
{



    //public new string name;


    
    public Points HP;
    public Points MP;

    [HideInInspector]
    public bool hasDied;

    [Header("Stats")]

    public MultiCharacterStat power;
    public MultiCharacterStat defence;
    public MultiCharacterStat penetration;


    //public CharacterStat physicalPower;
    //public CharacterStat physicalDefence;
    //public CharacterStat physicalPenetration;

    [Space(10)]

    //public CharacterStat magicPower;
    //public CharacterStat magicalDefence;
    //public CharacterStat magicalPenetration;

    [Header("Spells")]

    public Spell[] damageMoves;
    public Spell[] spawnMoves;
    public Spell[] curseMoves;






    void Start()
    {
        power.Init();
        defence.Init();
        penetration.Init();
        /*
        physicalPower.Init();
        physicalDefence.Init();
        magicPower.Init();
        magicalDefence.Init();
        physicalPenetration.Init();
        magicalPenetration.Init();
        */
    }

   


    public void LevelUp(int level)
    {
        HP.ChangeMax(10, true);
        MP.ChangeMax(5, true);
    }

    




}
