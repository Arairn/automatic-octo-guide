using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ArmorStats", menuName = "Battle")]

public class ArmorStats : ScriptableObject
{
    [SerializeField]
    List<float> armorStats;
    [SerializeField]
    float baseArmorIncrease = 0.05f;

    


    public void Init()
    {
        //List<float> armorStats = new List<float>();
        if(armorStats.Count<1) armorStats.Add(baseArmorIncrease);
        //Debug.Log(armorStats[0]);
        Recount(50);

        /*for (int i = 1; i<50;i++)
        {
            armorStats.Add(1f -((1f- armorStats[i-1])*baseArmorIncrease+ armorStats[i - 1]));
        }
        */
    }

    public float ArmorResist(int armor)
    {
        if(armor <= 0)
        {
            return 0;
        }
        if (armor > armorStats.Count)
        {
            Recount(armor);
        }
        return armorStats[armor];
    }

    public void Recount(int lastPosition)
    {
        if (lastPosition< armorStats.Count)
        {
            Debug.Log("Что-то пошло не так, тут не нужен рекаунт");
            return;
        }
        for (int i = armorStats.Count; i <= lastPosition; i++)
        {
            Debug.Log(i);
            armorStats.Add(((1f - armorStats[i - 1]) * baseArmorIncrease) + armorStats[i - 1]);
        }
    }

}
