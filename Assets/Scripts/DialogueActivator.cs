using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    DialogueManager dialogueManager;

    public string[] DialogueLines;

    public bool canBeActivated;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindWithTag("Interface").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeActivated && (Input.GetButton("Jump") || Input.GetButton("Fire1")) && !dialogueManager.dialogueSystem.activeInHierarchy)
        {
            dialogueManager.ShowDialogue(DialogueLines);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canBeActivated = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canBeActivated = false;
        }

    }

}
