using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private NPCDialogue dialogueToShow; // 스크립트 오브젝트
    [SerializeField] private GameObject interactionBox;

    public NPCDialogue DialogueToShow => dialogueToShow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSeleted = this;
            interactionBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSeleted = null;
            DialogueManager.Instance.CloseDialoguePanel();
            interactionBox.SetActive(false);
        }
    }
}
