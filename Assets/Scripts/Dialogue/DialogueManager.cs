using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager manager;
    public TMPro.TextMeshProUGUI dialogueBox;
    public float textSpeed;

    private Queue<string> dialogueSentences;
    private string currentLine;
    private static bool conversing = false;
    private bool typing = false;


    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !typing && conversing)
        {
            if(dialogueSentences.Count != 0)
            {
                currentLine = dialogueSentences.Dequeue();
                StartCoroutine(writeLine());
            } else
            {
                endConversation();
                conversing = false;
            }
        }
    }

    public static bool isConversing()
    {
        return conversing;
    }
    private void endConversation()
    {
        dialogueBox.text = string.Empty;
        dialogueBox.gameObject.SetActive(false);
    }
    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("DialogueManager");
        manager = gameObject.GetComponent<DialogueManager>();
    }

    public static DialogueManager getInstance()
    {
        return manager;
    }

    public bool inDialogue()
    {
        return conversing;
    }

    public void startDialogue(Queue<string> sentences)
    {
        dialogueBox.gameObject.SetActive(true);
        this.dialogueSentences = sentences;
        currentLine = dialogueSentences.Dequeue();
        StartCoroutine(writeLine());
        conversing = true;
    }

    private IEnumerator writeLine()
    {
        typing = true;
        dialogueBox.text = string.Empty;
        foreach (char c in currentLine.ToCharArray())
        {
            dialogueBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }

}
