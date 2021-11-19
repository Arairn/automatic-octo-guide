using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChars : MonoBehaviour
{

    public bool isPlayer;

    public string charName;

    

    public int currentHP, maxHP, currentMP, maxMP, strength, defence, weaponPWR, armorPWR;
    public bool hasDied;

    public BattleMoves[] battleMoves;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectRandomAttack(Transform target)
    {
        battleMoves[Random.Range(0, battleMoves.Length)].DisplayMove(target);
    }

    public void LevelUp(int level)
    {
        maxHP += 5;
        currentHP = maxHP;
        maxMP += 2;
        currentMP = maxMP;
    }
}
