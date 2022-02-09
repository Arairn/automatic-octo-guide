using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDebuff : NewBuffBit
{
    public StatModifier modifier;
    public int duration;
    public ModStat stat;
    //public override bool continious => throw new System.NotImplementedException();

    public override void EffectStart(CharacterFacade attacker, CharacterFacade target)
    {
        target.Stat(stat).AddModifier(modifier);
        target.AddBuff(this, attacker);

    }

    public override void EffectTick(CharacterFacade target)
    {
        duration -= 1;
        if (duration < 1)
        {
            ShouldBeRemoved = true;
        }
    }

    public override void EffectEnd(CharacterFacade target)
    {
        target.Stat(stat).RemoveModifier(modifier);
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
