using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Dialog;
public class QuestObjectActivator : MonoBehaviour
{

    public GameObject ObjectToActivate;
    public string questToCheck;

    public bool activeIfComplete;

    private bool intialCheckDone;

    private float totalHP;
    private float HP;

    DialogActivator dialogActivator;


    // Start is called before the first frame update
    void Start()
    {
        totalHP = gameObject.GetComponent<Health>().GetHealthPoints();
        dialogActivator = GameObject.FindWithTag("NPC").GetComponent<DialogActivator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!intialCheckDone)
        {
            intialCheckDone = true;
            CheckCompletion();
        }
        hitQuest();
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance.CheckIfComplete(questToCheck))
        {
            ObjectToActivate.SetActive(activeIfComplete);
            //Set current quest to next quest in quest manager
        }
    }

    //Find the Health Object
    //If quest object is hit complete quest
    public void hitQuest()
    {
        HP = gameObject.GetComponent<Health>().GetHP();
        //Debug.Log("dummy health points" + " " + hp);
        if (HP < totalHP)
        {
            activeIfComplete = true;
            QuestManager.instance.MarkQuestComplete(questToCheck);
        }

    }

    public void talkingQuest()
    {
        //if current quest is equal to name of current questgroup dialog complete
        //Mark quest complete

    }

}
