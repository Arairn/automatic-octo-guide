using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    public TMP_Text text;
    public float lifeTime = 1.5f;
    public float speed = 0.8f;
    public float deltaUp = 5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0f, deltaUp, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damage)
    {
        text.text = damage.ToString();

    }
}
