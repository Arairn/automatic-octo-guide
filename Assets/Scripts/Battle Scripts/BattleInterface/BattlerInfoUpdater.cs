using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattlerInfoUpdater : MonoBehaviour
{
    public SliderInfo hp, mp;
    public TMP_Text nameText;
    int battler;
    


    public void SetParams(int i, GameObject parent)
    {
        battler = i;
        nameText.text = BattleManager.instance.activeBattlers[battler].name;
        if (BattleManager.instance.activeBattlers[battler].maxMP == 0)
        {
            mp.gameObject.SetActive(false);
        }
        transform.SetParent(parent.transform);
        BattleManager.BattlersHaveChanged += UpdateInfo;
        BattleManager.BattleHasEnded += DestroyInfo;



    }

    public void UpdateInfo()
    {

        hp.SetValues(BattleManager.instance.activeBattlers[battler].currentHP, BattleManager.instance.activeBattlers[battler].maxHP);
        if (mp.gameObject.activeInHierarchy)
        {
            mp.SetValues(BattleManager.instance.activeBattlers[battler].currentMP, BattleManager.instance.activeBattlers[battler].maxMP);
        }
    }

    public void DestroyInfo()
    {
        Destroy(gameObject);
    }
    public void OnDisable()
    {

    }
    public void OnDestroy()
    {
        BattleManager.BattlersHaveChanged -= UpdateInfo;
        BattleManager.BattleHasEnded -= DestroyInfo;
    }
}
