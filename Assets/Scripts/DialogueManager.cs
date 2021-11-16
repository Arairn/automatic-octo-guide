using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText, dialogueText;
    public GameObject dialogueSystem, nameBox;
    public string[] dialogueLines;
    public int currentLine;
    public bool justStarted;
    string restOfSentence;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueSystem.activeInHierarchy&&(Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump")))
        {
            if (!justStarted)
            {
                currentLine++;
                if (currentLine < dialogueLines.Length) ShowLine();
                else
                {
                    dialogueSystem.SetActive(false);
                    PlayerController.instance.StartMoving();
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
        //dialogueText.text = dialogueLines[currentLine];
        CheckIfName();
        dialogueText.text = restOfSentence;
    }
    public void ShowDialogue(string[] newLines)
    {
        dialogueLines = newLines;
        currentLine = 0;
        dialogueSystem.SetActive(true);
        nameBox.SetActive(true);
        ShowLine();
        justStarted = true;
        PlayerController.instance.StopMoving();
    }
    void CheckIfName()
    {

        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            string sentence = dialogueLines[currentLine];
            int firstSpaceLocation = sentence.IndexOf(' ');
            
            nameText.text = sentence.Substring(2, firstSpaceLocation - 1);
            restOfSentence = dialogueLines[currentLine].Substring(firstSpaceLocation + 1);
        }
        else if (dialogueLines[currentLine].StartsWith("name-"))
        {
            string sentence = dialogueLines[currentLine];
            int firstSpaceLocation = sentence.IndexOf(' ');
            nameBox.SetActive(false);
            restOfSentence = dialogueLines[currentLine].Substring(firstSpaceLocation + 1);
        }
        else
        {
            restOfSentence = dialogueLines[currentLine];
            //currentLine++;
        }

    }
}
