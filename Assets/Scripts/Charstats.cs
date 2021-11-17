using UnityEngine;

public class Charstats : MonoBehaviour
{

    public string charName;
    public int currentLevel;
    public int currentEXP;
    public int[] expToNextLevel;
    const int maxLevel = 50;
    const int baseEXP = 1000;

    public int currentHP, maxHP, currentMP, maxMP;
    public int strength, defence, weaponPWR, armorPWR;

    public string equippedWeapon, equippedArmor;

    private bool maxLevelIsReached;
    public Sprite charImage;




    public Charstats()
    {
        currentLevel = 1;
        maxHP = 100;
        currentHP = maxHP;
        maxMP = 30;
    }
    public Charstats(int level, int maximumHP, int maximumMP, Sprite characterImage)
    {
        currentLevel = level;
        maxHP = maximumHP;
        maxMP = maximumMP;
        charImage = characterImage;
        if (level >= maxLevel) maxLevelIsReached = true;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(1000);
        }
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
        strength++;
        defence++;
        maxHP += 5;
        currentHP = maxHP;
        maxMP += 2;
        currentMP = maxMP;
    }
}
