using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject MenuSystem;
    public GameObject[] windows;
    CharacterFacade PlayerFacade;
    public SliderInfo hp;
    public SliderInfo mp;
    public SliderInfo exp;

    // Start is called before the first frame update
    void Start()
    {
        PlayerFacade = GameObject.FindWithTag("Player").GetComponent<CharacterFacade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (MenuSystem.activeInHierarchy)
            {
                CloseMenu();
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
        hp.SetValues(PlayerFacade.GetStat(CharacterStatsEnum.currentHP), PlayerFacade.GetStat(CharacterStatsEnum.maxHP));
        mp.SetValues(PlayerFacade.GetStat(CharacterStatsEnum.currentMP), PlayerFacade.GetStat(CharacterStatsEnum.maxMP));
        exp.SetValues(PlayerFacade.GetStat(CharacterStatsEnum.currentEXP), PlayerFacade.GetStat(CharacterStatsEnum.maxExp));
    }
    public void ToggleWindow(int number)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == number)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        MenuSystem.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }
}
