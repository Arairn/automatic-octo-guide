using System.Collections.Generic;
using UnityEngine;
public class BuffSystem: MonoBehaviour
{

    
    List<NewBuffBit> buffs;
    CharacterFacade characterFacade;
    private NewBuffBit buff;
    bool isDirty = false;

    public void Awake()
    {
        buffs = new List<NewBuffBit>();
    }

    private void Start()
    {
        characterFacade = GetComponent<CharacterFacade>();
        if (!characterFacade) Debug.LogError("Система баффов не нашла фасад, к которому она применяется");
        //buffs = new List<BattleBuff>();
    }

    private void Update()
    {
        if (isDirty) RemoverLoop();
    }

    public void AddBuff(NewBuffBit _buff, CharacterFacade attacker)
    {
        buff = Instantiate(_buff, this.transform);
        buff.SetAttacker(attacker);
        buffs.Add(buff);



    }

    public void RemoveBuff(NewBuffBit _buff)
    {
        buffs.Remove(_buff);
    }



    public void CheckAllBuffs()
    {
        if (buffs.Count == 0) return;
        foreach (NewBuffBit buff in buffs)
        {
            buff.EffectTick(characterFacade);
            
        }
        isDirty = true;

    }

    public void RemoverLoop()
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            if (buffs[i].ShouldBeRemoved)
            {
                buffs[i].SpellDestroy(this);
            }
        }
        isDirty = false;
    }

    public void ClearBuffs()
    {
        if (buffs.Count == 0) return;
        foreach (NewBuffBit buff in buffs)
        {
            buff.ShouldBeRemoved = true;

        }
        isDirty = true; 
    }




    /*

    
    CharacterFacade characterFacade;
    List<BattleBuffWrapper> buffs;

    public void Awake()
    {
        buffs = new List<BattleBuffWrapper>();
    }

    private void Start()
    {
        characterFacade = GetComponent<CharacterFacade>();
        if (!characterFacade) Debug.LogError("Система баффов не нашла фасад, к которому она применяется");
        //buffs = new List<BattleBuff>();
    }

    public void AddBuff(BattleBuff _buff)
    {
        BattleBuffWrapper buff = new BattleBuffWrapper(_buff, characterFacade);

        buffs.Add(buff);

    }

    public void CheckAllBuffs(bool inTheBeginning)
    {
        if (buffs.Count == 0) return;
        foreach (BattleBuffWrapper buff in buffs)
        {
            //if (!buff.triggersBeforeTargetsTurn && !buff.triggersAfterTargetsTurn) Debug.LogWarning("Бафф "+buff.buffName + " не триггерится ни в начале, ни в конце хода!");


            if (buff.buffInfo.triggersBeforeTargetsTurn && inTheBeginning) { CheckBuff(buff, false); }

            if (!inTheBeginning)
            {
                Debug.LogWarning("пока не работает");
                CheckBuff(buff);
                Expire(buff);
            }
        }
        CheckForRemoving();

    }

    void CheckBuff(BattleBuffWrapper buff, bool expire = true)
    {
        if (buff.buffInfo.damagePerTurn != 0)
        {

            BattleCalculator.instance.ApplyDamage(buff.attackerFacade, characterFacade, buff.buffInfo.damagePerTurn, buff.buffInfo.typeOfDamage, buff.buffInfo.vampPercent, buff.power, buff.penetration);
        }
        Debug.Log("CheckBuff");


        if (expire)
        {
            Expire(buff);
        }
    }

    void Expire(BattleBuffWrapper buff)
    {
        buff.timer--;
        if (buff.timer <= 0)
        {
            Debug.Log("Бафф закончился");
            if (buff.buffInfo.damageInTheEnd != 0)
            {
                BattleCalculator.instance.ApplyDamage(buff.attackerFacade, characterFacade, buff.buffInfo.damageInTheEnd, buff.buffInfo.typeOfDamage, buff.buffInfo.vampPercent, buff.power, buff.penetration);

            }

            Debug.Log("Нужно удалить все стат.модификаторы");
            buff.ToRemove = true;


        }
    }
    public void RemoveBuff(BattleBuffWrapper buff)
    {
        buff.ToRemove = true;
        CheckForRemoving();
    }

    void CheckForRemoving()
    {
        for (int i = buffs.Count - 1; i > 0; i--)
        {
            if (buffs[i].ToRemove)
            {
                buffs.Remove(buffs[i]);
            }
        }
    }

    public void ClearBuffs()
    {
        buffs = new List<BattleBuffWrapper>();
    }

    */


}
