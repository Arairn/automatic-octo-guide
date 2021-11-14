using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText, DialogueText;
    public GameObject dialogueSystem, nameBox;
    public string[] dialogueLines;
    public int currentLine;
    public bool justStarted;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            if (!justStarted)
            {
                currentLine++;
                if (currentLine < dialogueLines.Length) ShowLine();
                else
                {
                    dialogueSystem.SetActive(false);
                }
            }
            else
            {
                justStarted = false;
            }
        }
    }


    void ShowLine()
    {
        DialogueText.text = dialogueLines[currentLine];
    }
    public void ShowDialogue(string[] newLines)
    {
        dialogueLines = newLines;
        currentLine = 0;
        dialogueSystem.SetActive(true);
        ShowLine();
        justStarted = true;
    }
}
