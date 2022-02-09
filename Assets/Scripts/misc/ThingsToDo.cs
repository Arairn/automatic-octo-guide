using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsToDo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (LogController.instance.Ideas)
        {
            Debug.Log("Идеи для реализации:\n Скрипт Things TO DO!");
            /*
             * Баги:
             * 1. спаун в одной клетке
             * 
             * Идеи:
             * 1.Нормальная система инвентаря и врагов через scriptable object
             * 2.Система диалогов из второй обучалки
             * 3.Приподнять камеру когда открыт диалог
             * 4.Система талантов
             * 5. BattleAI нужно хорошенечко доделать
            */
        }


    }


}
