using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    private static DialogueManager manager;
    public TMPro.TextMeshProUGUI dialogueBox;
    public UnityEngine.UI.Image textCanvas;
    public float textSpeed = 0.15f;

    private Queue<string> dialogueSentences;
    private string currentLine;
    private static bool conversing = false;
    private bool typing = false;
    private List<GameObject> activated = new List<GameObject>();


    public void HandleUpdate() {
        if (Input.GetMouseButtonDown(0) && !typing && conversing) {
            if (this.GetComponent<AudioSource>() != null) this.GetComponent<AudioSource>().Play();
            if (dialogueSentences.Count != 0) {
                currentLine = dialogueSentences.Dequeue();
                StartCoroutine(writeLine());
            }
            else {
                endConversation();
                conversing = false;
            }
        }
    }

    public static bool isConversing() {
        return conversing;
    }
    private void endConversation() {
        dialogueBox.text = string.Empty;
        dialogueBox.gameObject.SetActive(false);
        textCanvas.gameObject.SetActive(false);
        foreach (GameObject active in this.activated)
        {
            active.SetActive(false);
        }
        activated.Clear();
        GameController.Instance.Dialog(false);
    }
    private void Start() {
        GameObject gameObject = GameObject.FindGameObjectWithTag("DialogueManager");
        manager = gameObject.GetComponent<DialogueManager>();
    }

    public static DialogueManager getInstance() {
        return manager;
    }

    public bool inDialogue() {
        return conversing;
    }

    public void startDialogue(Queue<string> sentences) {
        dialogueBox.gameObject.SetActive(true);
        textCanvas.gameObject.SetActive(true);
        this.dialogueSentences = sentences;
        currentLine = dialogueSentences.Dequeue();
        StartCoroutine(writeLine());
        conversing = true;
    }

    public void startDialogue(Queue<string> sentences, GameObject toActivate)
    {
        toActivate.SetActive(true);
        this.activated.Add(toActivate);
        this.startDialogue(sentences);
    }

    private IEnumerator writeLine() {
        typing = true;
        dialogueBox.text = string.Empty;
        foreach (char c in currentLine.ToCharArray()) {
            dialogueBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }

}
