using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Синий маркер обозначения хода

public class MarkerJumper : MonoBehaviour
{
    public Transform trnsfm;
    private void Start()
    {
        trnsfm = GetComponent<Transform>();
    }
    public void JumpTo(Transform target)
    {
        trnsfm.position = target.position;
    }
}
