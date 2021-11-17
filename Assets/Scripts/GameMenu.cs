using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject MenuSystem;
    Charstats charstats;
    public SliderInfo hp;
    public SliderInfo mp;
    public SliderInfo exp;

    // Start is called before the first frame update
    void Start()
    {
        charstats = GameObject.FindWithTag("Player").GetComponent<Charstats>();
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
                RefreshMenu();
            }

        }
    }
    void RefreshMenu()
    {
        hp.SetValues(charstats.currentHP, charstats.maxHP);
        mp.SetValues(charstats.currentMP, charstats.maxMP);
        exp.SetValues(charstats.currentEXP, charstats.expToNextLevel[charstats.currentLevel]);
    }
}
