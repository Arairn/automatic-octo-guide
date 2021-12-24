using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    const int maxLevel = 50;
    const int baseEXP = 1000;
    private bool maxLevelIsReached;
    CharacterFacade Facade;


    // Start is called before the first frame update
    void Start()
    {
        Facade = GetComponent<CharacterFacade>();
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = (int)(expToNextLevel[i - 1] * 1.05);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(1000);
        }
#endif
    }
    public int GetExp(bool max)
    {
        if (maxLevelIsReached)
        {
            return 999999;
        }
        if (max)
        {
            return expToNextLevel[currentLevel];
        }
        return currentEXP;
    }
    public void AddExp(int expAmount)
    {

        if (maxLevelIsReached)
        {
            return;
        }
        currentEXP += expAmount;
        while (currentEXP > expToNextLevel[currentLevel])
        {
            if (currentLevel < maxLevel)
            {
                currentEXP -= expToNextLevel[currentLevel];
                currentLevel++;
                LevelUp();
            }
            else
            {
                currentEXP = 0;
                maxLevelIsReached = true;
            }
            currentEXP += expAmount;


        }
    }

    public void LevelUp()
    {
        //strength++;
        //defence++;
        Facade.LevelUp();
        GameManager.instance.playerBattleChars.LevelUp(currentLevel);

    }
}
