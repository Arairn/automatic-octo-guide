using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleInfoPrefabUpdater : MonoBehaviour
{
    //BattleChars battleChars;



    public void Renew(bool ToBattle)
    {

        if (gameObject.GetComponent<BattleChars>()) gameObject.GetComponent<BattleChars>().DestroyIt();
        Debug.Log("Running Renew on "+gameObject.name);
        if (ToBattle)
        {
            BattleChars battleChars = gameObject.AddComponent(GameManager.instance.gameObject.GetComponent<BattleChars>());
        }
        else
        {
            BattleChars battleChars = gameObject.AddComponent(BattleManager.instance.player);
            GameManager.instance.LinkToBattleChars();
        }
        //Debug.Log(GameManager.instance.playerBattleChars.maxMP + " " + battleChars.maxMP);


    }
}
