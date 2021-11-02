using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialog;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerNames;


    public bool[] currentQuest;

    public bool[] questMarkerComplete;
    public static QuestManager instance;
    DialogActivator dialogActivator;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkerComplete = new bool[questMarkerNames.Length];
        dialogActivator = GameObject.FindWithTag("NPC").GetComponent<DialogActivator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckIfComplete("quest test"));
            MarkQuestComplete("quest test");
            MarkQuestIncomplete("fight the demon");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.Log("Quest " + questToFind + " does not exist");
        return 0;
    }

    public string GetCurrentQuest()
    {
        for (int i = 0; i < currentQuest.Length; i++)
        {
            if (currentQuest[i] == true)
            {
                return questMarkerNames[i];
            }
        }

        return null;
    }

    public void setNextQuest()
    {
        for (int i = 0; i < questMarkerComplete.Length; i++)
        {
            if (questMarkerComplete[i] == true)
            {

                currentQuest[i] = false;
                currentQuest[i + 1] = true;
            }

        }
    }




    public bool CheckIfComplete(string questToCheck)
    {

        //found correct number
        if (GetQuestNumber(questToCheck) != 0)
        {
            return questMarkerComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkerComplete[GetQuestNumber(questToMark)] = true;
        //mark quest complete in dialogActivator as well; 
        //if questToMark is equal to dialogActivator questToMark mark is complete to true;
        for (int i = 0; i < dialogActivator.npcQuestGroup.Length; i++)
        {
            if (questToMark == dialogActivator.npcQuestGroup[i].questToMark)
            {
                dialogActivator.npcQuestGroup[i].isQuestCompleted = true;
            }
        }
        setNextQuest();
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkerComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        //call this function after any quest has been marked complete or incomplete
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if (questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }

    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {

                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }
            if (valueToSet == 0)
            {
                questMarkerComplete[i] = false;
            }
            else
            {

                questMarkerComplete[i] = true;
            }

        }
    }
}
