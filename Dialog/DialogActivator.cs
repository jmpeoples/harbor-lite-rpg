using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialog
{


    public class DialogActivator : MonoBehaviour
    {
        public string[] lines;

        [System.Serializable]
        public class npcLines
        {
            public string[] npcDialog;
            public bool isDialogCompleted;
            public string questName;
        }

        [System.Serializable]
        public class npcQuests
        {
            public string questToMark;
            public bool isQuestCompleted;
            public bool shouldActivateQuest;
        }

        public npcLines[] npcLineGroup;
        public npcQuests[] npcQuestGroup;

        GameObject NPC;
        GameObject player;

        public bool canActivate;
        public string speakerName;

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log(setSpeakerName());
            NPC = GameObject.FindWithTag("NPC");
            player = GameObject.FindWithTag("Player");

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void npcTalk()
        {
            if (canActivate)
            {
                transform.LookAt(player.transform);
                string currentQuestToMark = QuestManager.instance.GetCurrentQuest();
                if (QuestManager.instance.GetCurrentQuest() == null)
                {

                    DialogManager.instance.ShowDialog(npcLineGroup[0].npcDialog, currentQuestToMark, 0);
                    return;
                }
                //Get currentCurrentQuest
                for (int i = 0; i < npcQuestGroup.Length; i++)
                {
                    if (npcQuestGroup[i].questToMark == QuestManager.instance.GetCurrentQuest())
                    {
                        DialogManager.instance.ShowDialog(npcLineGroup[i].npcDialog, currentQuestToMark, i);

                        talkingQuest();

                        return;
                    }
                }
            }
        }

        public void talkingQuest()
        {
            string currentQuest = QuestManager.instance.GetCurrentQuest();

            // is dialogActivator dialog equal to current quest
            for (int i = 0; i < npcLineGroup.Length; i++)
            {
                if (npcLineGroup[i].questName == currentQuest)
                {

                    if (npcLineGroup[i].isDialogCompleted)
                    {
                        if (npcQuestGroup[i].isQuestCompleted)
                        {

                            QuestManager.instance.MarkQuestComplete(currentQuest);
                            return;
                        }

                    }
                }
            }
        }

        public string[] GetLines()
        {
            return lines;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                canActivate = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                canActivate = false;
            }
        }

        public string setSpeakerName()
        {

            return speakerName;

        }

        public string getQuestMarker()
        {
            for (int i = 0; i < npcQuestGroup.Length; i++)
            {
                if (npcQuestGroup[i].isQuestCompleted == false)
                {
                    Debug.Log(npcQuestGroup[i].questToMark);
                    return npcQuestGroup[i].questToMark;
                }
            }

            return null;
        }





        public void setQuestComplete(string currentQuestName)
        {
            //find quest name in NPC Quest group
            for (int i = 0; i < npcQuestGroup.Length; i++)
            {
                if (npcQuestGroup[i].questToMark == currentQuestName)
                {
                    npcQuestGroup[i].isQuestCompleted = true;
                }
            }
        }
    }

}
