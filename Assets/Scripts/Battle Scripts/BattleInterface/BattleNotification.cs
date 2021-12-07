using TMPro;
using UnityEngine;


public class BattleNotification : MonoBehaviour
{
    public TMP_Text text;
    public float lifeTime = 1.5f;

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetNotification(string notificationText)
    {
        text.text = notificationText;

    }
}
