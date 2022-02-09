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
    public BuffSystem battleBuffSystem;

    [SerializeField]
    bool mayNotHaveBattleSystem = false;
    [SerializeField]
    bool ShouldBeLinkedWithPlayer = false;
    // Start is called before the first frame update

    //Инициализация всего
    void Awake()
    {
        CharacterBattleStatsSystem = GetComponent<CharacterBattleStatsSystem>();
        ExperienceSystem = GetComponent<ExperienceSystem>();
        CharacterInfo = GetComponent<CharacterInfo>();
        animationController = GetComponent<CharacterAnimationController>();
        AI = GetComponent<BattleAI>();
        battleBuffSystem = GetComponent<BuffSystem>();


        if (ShouldBeLinkedWithPlayer)
        {
            if (LogController.instance.PlayerStatsMagicLog)
            {
                Debug.Log("пытаемся прицепить " + GameManager.instance.playerFacade.GetComponent<CharacterBattleStatsSystem>() + " к Баттл-префабу игрока");
            }
            CharacterBattleStatsSystem = GameManager.instance.playerFacade.GetComponent<CharacterBattleStatsSystem>();
            ExperienceSystem = GameManager.instance.playerFacade.GetComponent<ExperienceSystem>();
            CharacterInfo = GameManager.instance.playerFacade.GetComponent<CharacterInfo>();
            animationController = GameManager.instance.playerFacade.GetComponent<CharacterAnimationController>();
        }
        isPlayer = CharacterInfo.isPlayer;

        if (!battleBuffSystem)
        {
            battleBuffSystem = gameObject.AddComponent(typeof(BuffSystem)) as BuffSystem;

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

    public void CheckBuffs()
    {
        battleBuffSystem.CheckAllBuffs();
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
    public int GetStat(CharacterStatsEnum stat, TypeOfDamage typeOfDamage= TypeOfDamage.physical)
    {
        if (CharacterBattleStatsSystem)
        {
            switch (stat)
            {
                /*
                case CharacterStatsEnum.physicalPower:
                    
                    return CharacterBattleStatsSystem.physicalPower.Value;
                case CharacterStatsEnum.physicalDefence:
                    return (int)CharacterBattleStatsSystem.physicalDefence.Value;
                */
                case CharacterStatsEnum.currentHP:                
                    return CharacterBattleStatsSystem.HP.Get(false);
                case CharacterStatsEnum.maxHP:
                    return CharacterBattleStatsSystem.HP.Get(true);
                case CharacterStatsEnum.currentMP:
                    return CharacterBattleStatsSystem.MP.Get(false);
                case CharacterStatsEnum.maxMP:
                    return CharacterBattleStatsSystem.MP.Get(true);
                case CharacterStatsEnum.currentEXP:
                    return ExperienceSystem.GetExp(false);
                case CharacterStatsEnum.maxExp:
                    return ExperienceSystem.GetExp(true);
                    /*
                case CharacterStatsEnum.magicPower:
                    return CharacterBattleStatsSystem.magicPower.Value;
                case CharacterStatsEnum.magicalDefence:
                    return CharacterBattleStatsSystem.magicalDefence.Value;
                case CharacterStatsEnum.physicalPenetration:
                    return CharacterBattleStatsSystem.physicalPenetration.Value;
                case CharacterStatsEnum.magicalPenetration:
                    return CharacterBattleStatsSystem.magicalPenetration.Value;
                    */
                case CharacterStatsEnum.power:
                    return CharacterBattleStatsSystem.power.GetStat(typeOfDamage);
                case CharacterStatsEnum.defence:
                    return CharacterBattleStatsSystem.defence.GetStat(typeOfDamage);
                case CharacterStatsEnum.penetration:
                    return CharacterBattleStatsSystem.penetration.GetStat(typeOfDamage);
                default:
                    Debug.Log("Что-то не так, " + CharacterBattleStatsSystem + " не предусмотрен в switch ");
                    break;
            }
        }



        Debug.LogWarning("Вы хочете статов от " +gameObject.name +" их нет у нас");
        return 0;
    }

    public CharacterStat Stat(ModStat statNeeded)
    {
        switch (statNeeded)
        {
            case ModStat.PhysPower:
                return CharacterBattleStatsSystem.power.physical;
            case ModStat.MagPower:
                return CharacterBattleStatsSystem.power.magical;
            case ModStat.PhysDef:
                return CharacterBattleStatsSystem.defence.physical;
            case ModStat.MagDef:
                return CharacterBattleStatsSystem.defence.magical;
            case ModStat.PhysPen:
                return CharacterBattleStatsSystem.penetration.physical;
            case ModStat.MagPen:
                return CharacterBattleStatsSystem.penetration.magical;
        }
        Debug.LogError("Запросили стат который не прописан");
        return null;

    }


    public void StartEnemyTurn() 
    {
        AI.AITurn();
    }

    public void ChangeHP(int amount)//,TypeOfDamage typeOfDamage, float vampPercent)
    {
        Debug.LogWarning("Changing " + amount + ". Тип повреждений и вампиризм не обрабатывается");
        if (CharacterBattleStatsSystem)
        {
            Debug.Log(CharacterInfo.charName + ": HP +"+amount);
            CharacterBattleStatsSystem.HP.Change(amount);
            if(amount <0)Instantiate(BattleManager.instance.damageText, this.transform.position, this.transform.rotation).SetDamage(amount);
            else if( amount >0) Instantiate(BattleManager.instance.healText, this.transform.position, this.transform.rotation).SetDamage(amount);
            ChangesNeededToPassToBattle();
        }
    }/*
    public void Heal(int amount)
    {
        if (CharacterBattleStatsSystem)
        {
            if (amount > 0)
            {
                CharacterBattleStatsSystem.Heal(amount);
                Instantiate(BattleManager.instance.healText, this.transform.position, this.transform.rotation).SetDamage(amount);
                ChangesNeededToPassToBattle();
            }
        }
    }*/
    public void ChangeMP(int amount)
    {
        if (CharacterBattleStatsSystem)
        {
            Debug.Log("Dealing " + -amount + " mana damage to " + CharacterInfo.charName);
            CharacterBattleStatsSystem.MP.Change(-amount);
            //if (amount < 0) Instantiate(BattleManager.instance.damageText, this.transform.position, this.transform.rotation).SetDamage(amount);
            //else if (amount < 0) Instantiate(BattleManager.instance.healText, this.transform.position, this.transform.rotation).SetDamage(amount);
            ChangesNeededToPassToBattle();
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
        battleBuffSystem.ClearBuffs();
    }
    public void SetToRevived()
    {
        animationController.SetSpriteToDead(false);
    }

    //Баффы
    public void AddBuff(NewBuffBit buff, CharacterFacade attacker)
    {
        battleBuffSystem.AddBuff(buff, attacker);

    }


    void ChangesNeededToPassToBattle()
    {
        BattleManager.instance.battleTurnManager.InvokeBattleChanges();
    }
}
