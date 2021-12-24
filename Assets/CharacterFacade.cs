using System;
using UnityEngine;

public class CharacterFacade : MonoBehaviour
{
    public bool isPlayer;
    public ExperienceSystem ExperienceSystem;
    public CharacterBattleStatsSystem CharacterBattleStatsSystem;
    public CharacterInfo CharacterInfo;
    public CharacterAnimationController animationController;
    public BattleAI AI;
    public BattleBuffSystem buffSystem;

    [SerializeField]
    bool mayNotHaveBattleSystem = false;
    [SerializeField]
    bool ShouldBeLinkedWithPlayer = false;
    // Start is called before the first frame update
    void Awake()
    {
        CharacterBattleStatsSystem = GetComponent<CharacterBattleStatsSystem>();
        ExperienceSystem = GetComponent<ExperienceSystem>();
        CharacterInfo = GetComponent<CharacterInfo>();
        animationController = GetComponent<CharacterAnimationController>();
        AI = GetComponent<BattleAI>();
        buffSystem = GetComponent<BattleBuffSystem>();


        if (ShouldBeLinkedWithPlayer)
        {
            if (LogController.PlayerStatsMagicLog)
            {
                Debug.Log("пытаемся прицепить " + GameManager.instance.playerFacade.GetComponent<CharacterBattleStatsSystem>() + " к Баттл-префабу игрока");
            }
            CharacterBattleStatsSystem = GameManager.instance.playerFacade.GetComponent<CharacterBattleStatsSystem>();
            ExperienceSystem = GameManager.instance.playerFacade.GetComponent<ExperienceSystem>();
            CharacterInfo = GameManager.instance.playerFacade.GetComponent<CharacterInfo>();
            animationController = GameManager.instance.playerFacade.GetComponent<CharacterAnimationController>();
        }
        isPlayer = CharacterInfo.isPlayer;

        if (!buffSystem)
        {
            buffSystem = gameObject.AddComponent(typeof(BattleBuffSystem)) as BattleBuffSystem;

        }

        if (!CharacterInfo)
        {
            CharacterInfo = gameObject.AddComponent(typeof(CharacterInfo)) as CharacterInfo;
            CharacterInfo.charName = "NewRandomName" + Time.time;
        }
        if (!animationController)
        {
            Debug.LogWarning("У " + gameObject.name + " отсутствует CharacterAnimationController");


        }

        if (!mayNotHaveBattleSystem && !CharacterBattleStatsSystem)
        {
            Debug.LogWarning("Фасад " + CharacterInfo.charName + " не содержит боевой системы, хотя должен");
        }

    }

    public void CheckBuffs(bool nowItIsBeginning)
    {
        buffSystem.CheckAllBuffs(nowItIsBeginning);
    }

    void Start()
    {



    }

    //Связки с Экспой

    public void ExperienceAdd(int amount)
    {
        if (ExperienceSystem)
        {
            ExperienceSystem.AddExp(amount);
        }

    }
    public void LevelUp() //Вызывается из ЭкспСистемы
    {

    }


    //CharInfo

    public string GetName()
    {
        return CharacterInfo.charName;
    }

    //Статы
    public int GetStat(CharacterStatsEnum stat)
    {
        if (CharacterBattleStatsSystem)
        {
            switch (stat)
            {
                case CharacterStatsEnum.physicalPower:
                    //
                    return CharacterBattleStatsSystem.physicalPower.Value;
                case CharacterStatsEnum.physicalDefence:
                    return (int)CharacterBattleStatsSystem.physicalDefence.Value;
                case CharacterStatsEnum.currentHP:
                    return CharacterBattleStatsSystem.currentHP;
                case CharacterStatsEnum.maxHP:
                    return CharacterBattleStatsSystem.maxHP;
                case CharacterStatsEnum.currentMP:
                    return CharacterBattleStatsSystem.currentMP;
                case CharacterStatsEnum.maxMP:
                    return CharacterBattleStatsSystem.maxMP;
                case CharacterStatsEnum.currentEXP:
                    return ExperienceSystem.GetExp(false);
                case CharacterStatsEnum.maxExp:
                    return ExperienceSystem.GetExp(true);
                case CharacterStatsEnum.magicPower:
                    return CharacterBattleStatsSystem.magicPower.Value;
                case CharacterStatsEnum.magicalDefence:
                    return CharacterBattleStatsSystem.magicalDefence.Value;
                case CharacterStatsEnum.physicalPenetration:
                    return CharacterBattleStatsSystem.physicalPenetration.Value;
                case CharacterStatsEnum.magicalPenetration:
                    return CharacterBattleStatsSystem.magicalPenetration.Value;
                default:
                    Debug.Log("Что-то не так, " + CharacterBattleStatsSystem + " не предусмотрен в switch ");
                    break;
            }
        }



        Debug.LogWarning("Вы хочете статов от " +gameObject.name +" их нет у нас");
        return 0;
    }



    public void StartEnemyTurn() 
    {
        AI.AITurn();
    }

    public void DealDamage(int amount)//,TypeOfDamage typeOfDamage, float vampPercent)
    {
        //Debug.LogWarning("Тип повреждений и вампиризм не обрабатывается");
        if (CharacterBattleStatsSystem)
        {
            Debug.Log("Dealing " + amount + " damage to " + CharacterInfo.charName);
            CharacterBattleStatsSystem.DealDamage(amount);
            Instantiate(BattleManager.instance.damageText, this.transform.position, this.transform.rotation).SetDamage(amount);
        }
    }
    public void Heal(int amount)
    {
        if (CharacterBattleStatsSystem)
        {
            CharacterBattleStatsSystem.Heal(amount);
        }
    }


    public void DestroyIt()
    {
        Destroy(this);
    }


    //Аниматор
    public void SetToDead()
    {
        animationController.SetSpriteToDead(true);
    }
    public void SetToRevived()
    {
        animationController.SetSpriteToDead(false);
    }

    //Баффы
    public void AddBuff(BattleBuff buff)
    {

    }
    public void RemoveBuff(BattleBuff buff)
    {

    }
}
