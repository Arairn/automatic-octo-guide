using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleButtonsManager : MonoBehaviour
{
    public Transform parent;
    public GameObject button;
    public List<GameObject> activeButtons = new List<GameObject>();
    public PlayerBattleMenu playerBattleMenu;
    private void Awake()
    {
        Clean();

    }
    // Start is called before the first frame update
    /*
    void SpawnEnemyTargetButtons()
    {
        foreach (var item in BattleManager.instance.activeBattlers)
        {
            if (!item.isPlayer)
            {
                if (item.currentHP > 0)
                {

                }
            }
        }
    }
    */

    public void SpawnButton(string _text, int i)
    {
        GameObject newGameObjectButton = Instantiate(button);
        newGameObjectButton.transform.SetParent(parent);
        newGameObjectButton.GetComponentInChildren<TMP_Text>().text = _text;
        Button newButton = newGameObjectButton.GetComponent<Button>();
        newButton.onClick.AddListener(() => playerBattleMenu.OnButtonPressed(i));
        activeButtons.Add(newGameObjectButton);
    }



    // Update is called once per frame
    public void Clean()
    {
        foreach (GameObject item in activeButtons)
        {
            Destroy(item);
        }
    }

}
