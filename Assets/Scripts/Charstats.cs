using UnityEngine;

public class Charstats : MonoBehaviour
{

    public string charName;
    public int currentLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    const int maxLevel = 50;
    const int baseEXP = 1000;

    public BattleChars playerBattleChars;


    //public int currentHP, maxHP, currentMP, maxMP;
    //public CharacterStat Defence = new CharacterStat(5);
    //public int strength, defence, weaponPWR, armorPWR;

    public string equippedWeapon, equippedArmor;

    private bool maxLevelIsReached;
    public Sprite charImage;





    // Start is called before the first frame update
    void Start()
    {

        playerBattleChars = GameManager.instance.playerBattleChars;
        //Debug.Log(playerBattleChars.maxHP);
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
    void LevelUp()
    {
        //strength++;
        //defence++;
        GameManager.instance.playerBattleChars.LevelUp(currentLevel);

    }
}
