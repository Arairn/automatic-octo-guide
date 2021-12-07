using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int soundEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Done()
    {
        Destroy(gameObject);
    }


}
