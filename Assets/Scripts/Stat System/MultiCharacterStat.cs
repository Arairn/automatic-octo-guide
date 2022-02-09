using System;
using UnityEngine;

[Serializable]

public class MultiCharacterStat
    {

    public CharacterStat physical;
    public CharacterStat magical;

    public void Init()
    {
        physical.Init();
        magical.Init();
    }

    public int GetStat(TypeOfDamage typeOfDamage)
    {
        switch (typeOfDamage)
        {
            case TypeOfDamage.physical:
                return physical.Value;

            case TypeOfDamage.magical:
                return magical.Value;

            case TypeOfDamage.combined:
                return (physical.Value +magical.Value)/2;
                
            case TypeOfDamage.plain:
                return 1;
            default:
                Debug.Log("Вышли из свитча, type = " + typeOfDamage + ", и что-то явно пошло не так");
                return 1;
        }
    }

}
