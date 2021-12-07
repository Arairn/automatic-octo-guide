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
        GameManager.instance.fadingbetweenAreas = true;
        image.SetActive(true);
        sceneAnimator = GetComponentInChildren<Animator>();
        StartCoroutine(StartNewScene());
    }
    public void LeavingScene(string LevelName)
    {
        StartCoroutine(LoadNewScene(LevelName));
    }

    IEnumerator LoadNewScene(string NextLevelName)
    {
        GameManager.instance.fadingbetweenAreas = true;
        sceneAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(NextLevelName);
    }
    IEnumerator StartNewScene()
    {
        
        yield return new WaitForSeconds(transitionTime);
        GameManager.instance.fadingbetweenAreas = false;
    }

    public IEnumerator BlackAndGone()
    {
        GameManager.instance.fadingbetweenAreas = true;
        sceneAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        sceneAnimator.SetTrigger("OutOfBlack");
        yield return new WaitForSeconds(transitionTime);
        GameManager.instance.fadingbetweenAreas = false;
    }

}
