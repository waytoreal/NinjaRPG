using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [Header("NPC quest panel")]
    [SerializeField] private QuestCardNPC questCardNPCPrefab;
    [SerializeField] private Transform npcPanelContainer;

    [Header("Player quest panel")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;

    private void Start()
    {
        LoadQuestsIntroNPCPanel();
    }

    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    public void AddProgress(string questID, int amount)
    {
        Quest questToUpdate = QuestExists(questID);
        if (questToUpdate == null) return;
        if (questToUpdate.QuestAccepted)
        {
            questToUpdate.AddProgress(amount);
        }
    }

    private Quest QuestExists(string qeustID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == qeustID)
            {
                return quest;
            }
        }
        return null;
    }

    private void LoadQuestsIntroNPCPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCardNPC npcCard = Instantiate(questCardNPCPrefab, npcPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);
        }
    }

    private void OnEnable()
    {
        for (int i=0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }
}
