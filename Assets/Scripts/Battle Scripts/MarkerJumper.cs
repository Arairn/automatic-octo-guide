using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Синий маркер обозначения хода

public class MarkerJumper : MonoBehaviour
{

    public void JumpTo(Transform target)
    {
        transform.position = target.position;
        if (LogController.BattleMarkerLog)
        {
            Debug.Log(transform.position+" Should be== "+ target.position);
        }
    }
}
