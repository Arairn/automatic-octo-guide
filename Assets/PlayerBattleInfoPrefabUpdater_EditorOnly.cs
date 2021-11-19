#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleInfoPrefabUpdater_EditorOnly : MonoBehaviour
{
    BattleChars battleChars;
    // Start is called before the first frame update
    void Start()
    {
        battleChars = GetComponent<BattleChars>();
    }

    // Update is called once per frame
    void Update()
    {
        battleChars = GameManager.instance.playerBattleChars;
    }
}
#endif