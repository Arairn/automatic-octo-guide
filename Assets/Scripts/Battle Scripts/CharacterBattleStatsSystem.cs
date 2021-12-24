using UnityEngine;

public enum CharacterStatsEnum
{
    currentHP,
    maxHP,
    currentMP,
    maxMP,
    currentEXP,
    maxExp,
    physicalPower,
    magicPower,
    physicalDefence,
    magicalDefence,
    physicalPenetration,
    magicalPenetration
}
public class CharacterBattleStatsSystem : MonoBehaviour
{



    //public new string name;


    [Header("HP")]
    public int maxHP;
    public int currentHP;

    [Header("MP")]
    public int currentMP;
    public int maxMP;

    public bool hasDied;

    [Header("Stats")]

    public CharacterStat physicalPower;
    public CharacterStat physicalDefence;
    public CharacterStat physicalPenetration;

    public CharacterStat magicPower;
    public CharacterStat magicalDefence;
    public CharacterStat magicalPenetration;

    [Header("Abilities")]

    public BattleMoves[] damageMoves;
    public BattleMoves[] spawnMoves;
    public BattleMoves[] curseMoves;


    


  
    void Start()
    {

        physicalPower.Init();
        physicalDefence.Init();
        magicPower.Init();
        magicalDefence.Init();
        physicalPenetration.Init();
        magicalPenetration.Init();
    }

   


    public void DealDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
    }



    public void LevelUp(int level)
    {
        maxHP += 5;
        currentHP = maxHP;
        maxMP += 2;
        currentMP = maxMP;
    }

    




}
