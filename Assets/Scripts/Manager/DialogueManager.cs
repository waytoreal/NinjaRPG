using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [Header("Config")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;

    public NPCInteraction NPCSeleted {get; set;}

    private bool dialogueStarted;
    private PlayerActions actions;
    private Queue<string> dialogueQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialogue();
        actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }

    private void LoadDialogueFromNPC()
    {
        if (NPCSeleted.DialogueToShow.Dialogue.Length <= 0) return;

        foreach(string sentence in NPCSeleted.DialogueToShow.Dialogue)
        {
            dialogueQueue.Enqueue(sentence);
        }
    }

    private void ShowDialogue()
    {
        if (NPCSeleted == null) return;
        if (dialogueStarted) return;
        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();
        npcIcon.sprite = NPCSeleted.DialogueToShow.Icon;
        npcNameTMP.text = NPCSeleted.DialogueToShow.Name;
        npcDialogueTMP.text = NPCSeleted.DialogueToShow.Greeting;
        dialogueStarted = true;
    }

    private void ContinueDialogue()
    {
        if (NPCSeleted == null)
        {
            dialogueQueue.Clear();
            return;
        }

        if (dialogueQueue.Count <= 0)
        {
            CloseDialoguePanel();
            dialogueStarted = false;
            if (NPCSeleted.DialogueToShow.HasInteraction)
            {
                OnExtraInteractionEvent?.Invoke(NPCSeleted.DialogueToShow.InteractionType);
            }
            return;
        }

        npcDialogueTMP.text = dialogueQueue.Dequeue();
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
        dialogueQueue.Clear();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

}
