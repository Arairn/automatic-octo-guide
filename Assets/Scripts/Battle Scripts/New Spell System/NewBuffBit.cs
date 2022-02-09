using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NewBuffBit:MonoBehaviour
{
    public bool ShouldBeRemoved=false;
    [HideInInspector]
    public CharacterFacade attacker;

    /*
    public abstract bool continious
    {
        get;
    }
    */
    public abstract void EffectStart(CharacterFacade attacker, CharacterFacade target);


    public virtual void EffectTick(CharacterFacade target) { }


    public virtual void EffectEnd(CharacterFacade target) { }

    public virtual void SpellDestroy(BuffSystem target)
    {
        if (!ShouldBeRemoved) Debug.Log("Почему-то удаляем бафф, хотя не должны");
        else Debug.Log("Удаляем бафф");

        if (target)
        {
            target.RemoveBuff(this);
            Destroy(gameObject);
        }
    }

    internal void SetAttacker(CharacterFacade _attacker)
    {
        attacker = _attacker;
    }
}

