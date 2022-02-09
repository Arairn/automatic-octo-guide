using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTime : NewBuffBit
{
    //public override bool continious => true;

    public int damage;
    public TypeOfDamage typeOfDamage;
    public float vampPercent;
    public int duration;
    

    public override void EffectStart(CharacterFacade _attacker, CharacterFacade target)
    {
        target.AddBuff(this, _attacker);
        attacker = _attacker;
    }
    public override void EffectTick(CharacterFacade target)
    {
        Debug.Log(attacker+" " + target + " " + damage + " " + typeOfDamage + " " + vampPercent);
        BattleCalculator.instance.ApplyDamage(attacker, target, damage, typeOfDamage, vampPercent);
        Debug.Log("Done");
        duration -= 1;
        if (duration < 1)
        {
            ShouldBeRemoved = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
