using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    
    public static LogController instance;
    [SerializeField]
    public bool BattleDamageLog, BattleAnimationsLog, BattleMarkerLog, Ideas, BattleSpawnLog, PlayerStatsMagicLog, BattleInterfaceLog, BattleTurns, StatLog;
    //public static bool BattleDamageLog, BattleAnimationsLog, BattleMarkerLog, Ideas, BattleSpawnLog, PlayerStatsMagicLog, BattleInterfaceLog, BattleTurns;
    // Start is called before the first frame update
    void Start()
    {

      
            if (instance == null)
            {
                instance = this;
            }

            
        }
    /*
            BattleDamageLog = battleDamageLog;
        BattleAnimationsLog = battleAnimationsLog;
        BattleMarkerLog = battleMarkerLog;
        BattleSpawnLog = battleSpawnLog;
        BattleTurns = battleTurns;
        PlayerStatsMagicLog = playerStatsMagicLog;
        BattleInterfaceLog = battleInterfaceLog;

        Ideas = ideas;

        if (!BattleDamageLog && !BattleAnimationsLog && !BattleMarkerLog && !BattleSpawnLog && !PlayerStatsMagicLog && !BattleInterfaceLog) Debug.Log("Логи Боя отключены");
        else if (BattleDamageLog && BattleAnimationsLog && BattleMarkerLog && BattleSpawnLog && PlayerStatsMagicLog && BattleInterfaceLog) Debug.Log("Логи Боя включены");
        else Debug.Log("Логи включены частично");
        if (!Ideas) Debug.Log("Идеи отключены");
    

    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
