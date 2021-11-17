using System;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour

{
    public string teleportName;
    public string leadsTo;
    public Transform pointOfExit;
    public bool doubleway = true;

    

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
        if (collision.tag == "Player" && doubleway)
        {

            GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().LeavingScene(leadsTo);
            //SceneManager.LoadScene(leadsTo);
            PlayerController.instance.teleportCameFrom = teleportName;
            
        }

    }
}
