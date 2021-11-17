using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject MenuSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (MenuSystem.activeInHierarchy)
            {
                MenuSystem.SetActive(false);
                GameManager.instance.gameMenuOpen = false;
            }
            else
            {
                MenuSystem.SetActive(true);
                GameManager.instance.gameMenuOpen = true;
            }

        }
    }
}
