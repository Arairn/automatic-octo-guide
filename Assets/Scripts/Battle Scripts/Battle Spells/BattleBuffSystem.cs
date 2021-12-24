using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleBuffSystem : MonoBehaviour
{

    CharacterFacade characterFacade;
    List<BattleBuff> buffs;

    public void Awake()
    {
        buffs = new List<BattleBuff>();
    }

    private void Start()
    {
        characterFacade = GetComponent<CharacterFacade>();
        if (!characterFacade) Debug.LogError("Система баффов не нашла фасад, к которому она применяется");
        //buffs = new List<BattleBuff>();
    }

    public void AddBuff(BattleBuff buff) 
    {
        buffs.Add(buff);
        buff.SetFacade(characterFacade);
    }

    public void CheckAllBuffs(bool inTheBeginning)
    {
        if (buffs.Count == 0) return;
        foreach(BattleBuff buff in buffs)
        {
            //if (!buff.triggersBeforeTargetsTurn && !buff.triggersAfterTargetsTurn) Debug.LogWarning("Бафф "+buff.buffName + " не триггерится ни в начале, ни в конце хода!");


            if (buff.triggersBeforeTargetsTurn&& inTheBeginning) { CheckBuff(buff); }
            
            if (!inTheBeginning)
            {
                //buff.Expire
            }
        }
    }

    void CheckBuff(BattleBuff buff)
    {
        Debug.LogError("CheckBuff не написан");
    }
}
