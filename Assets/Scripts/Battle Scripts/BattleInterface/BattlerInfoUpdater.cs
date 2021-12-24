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
        nameText.text = BattleManager.instance.activeBattlers[battler].GetName();
        if (BattleManager.instance.activeBattlers[battler].GetStat(CharacterStatsEnum.maxMP) == 0)
        {
            mp.gameObject.SetActive(false);
        }
        transform.SetParent(parent.transform);
        BattleTurnManager.BattlersHaveChanged += UpdateInfo;
        BattleManager.BattleHasEnded += DestroyInfo;



    }

    public void UpdateInfo()
    {

        hp.SetValues(BattleManager.instance.activeBattlers[battler].GetStat(CharacterStatsEnum.currentHP), BattleManager.instance.activeBattlers[battler].GetStat(CharacterStatsEnum.maxHP));
        if (mp.gameObject.activeInHierarchy)
        {
            mp.SetValues(BattleManager.instance.activeBattlers[battler].GetStat(CharacterStatsEnum.currentMP), BattleManager.instance.activeBattlers[battler].GetStat(CharacterStatsEnum.maxMP));
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
        BattleTurnManager.BattlersHaveChanged -= UpdateInfo;
        BattleManager.BattleHasEnded -= DestroyInfo;
    }
}
