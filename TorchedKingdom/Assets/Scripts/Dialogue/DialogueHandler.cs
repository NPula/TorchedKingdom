using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public string npcName;
    public string[] sentences;
    private bool canActivateBox;

    private void Update()
    {
        if (canActivateBox)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                DialogueController.Instance.DisplayDialogueText(npcName, sentences);
            }
        }
        else
        {
            DialogueController.Instance.CloseDialogueText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateBox = false;
        }
    }
}
