using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialog;
using UnityEngine.UI;
public class CurrentQuest : MonoBehaviour
{

    DialogActivator dialogActivator;


    public bool isActiveCurrentQuest;


    // Start is called before the first frame update
    void Start()
    {
        dialogActivator = GameObject.FindWithTag("NPC").GetComponent<DialogActivator>();
    }

    // Update is called once per frame
    void Update()
    {
        setCurrentQuest();
    }

    public void setCurrentQuest()
    {
        //dialogActivator.npcQuestGroup[i].shouldActivateQuest
        // QuestManager.instance.questMarkerComplete[] false

        //current Quest needs to know which is active from Quest Manager


        //current Quest needs to know if Quest has been completed


        GetComponent<Text>().text = QuestManager.instance.GetCurrentQuest();

    }
}
