using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour

{
    public string teleportName;
    public string leadsTo;
    public Transform pointOfExit;
    public bool doubleway = true;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance != null)
        {
            if (PlayerController.instance.teleportCameFrom == teleportName)
            {
                PlayerController.instance.JumpToPoint(pointOfExit);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&doubleway)
        {
            SceneManager.LoadScene(leadsTo);
            PlayerController.instance.teleportCameFrom = teleportName;
        }

    }
}
