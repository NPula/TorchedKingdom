using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : Singleton<DialogueController>
{
    [SerializeField] TextMeshProUGUI dialogueText, nameText;
    [SerializeField] GameObject dialogueBox, nameBox;
    private string[] dialogueSentences;
    private int currentSentence;

    private void Start()
    {
        nameBox.SetActive(false);
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentSentence < dialogueSentences.Length)
                {
                    // Change Text
                    dialogueText.text = dialogueSentences[currentSentence++];
                }
                else
                {
                    CloseDialogueText();
                }
            }
        }
    }

    public void DisplayDialogueText(string charName, string[] dialogue)
    {
        nameText.text = charName;
        dialogueSentences = dialogue;
        currentSentence = 0;
        nameBox.SetActive(true);
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueSentences[currentSentence++];
    }

    public void CloseDialogueText()
    {
        nameBox.SetActive(false);
        dialogueBox.SetActive(false);
        currentSentence = 0;
    }
}
