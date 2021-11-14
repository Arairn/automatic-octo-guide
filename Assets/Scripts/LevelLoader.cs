using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    Animator sceneAnimator;
    public GameObject image;
    public float transitionTime = 1f;

    private void Start()
    {
        image.SetActive(true);
        sceneAnimator = GetComponentInChildren<Animator>();
    }
    public void LeavingScene(string LevelName)
    {
        StartCoroutine(LoadNewScene(LevelName));
    }

    IEnumerator LoadNewScene(string NextLevelName)
    {
        sceneAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(NextLevelName);
    }
}
