using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameMenuOpen, dialogueActive, fadingbetweenAreas;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogueActive || fadingbetweenAreas) PlayerController.instance.StopMoving();
        else PlayerController.instance.StartMoving();
    }

    

    
}
