using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.Dialog
{
    public class DialogManager : MonoBehaviour
    {

        [SerializeField] float talkDistance = 5f;
        public Text dialogText;
        public Text nameText;
        public GameObject dialogBox;
        public GameObject nameBox;

        public static DialogManager instance;

        public string[] dialogLines;

        public int currentLine;

        private string questToMark;
        private bool MarkQuestComplete;
        private bool shouldMarkQuest;

        DialogActivator dialogActivator;
        GameObject NPC;
        GameObject Player;

        // Start is called before the first frame update


        void Start()
        {
            instance = this;
            dialogBox.SetActive(false);
            NPC = GameObject.FindWithTag("NPC");
            Player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowDialog(string[] newLines, string currentQuestToMark, int dialogPosition)
        {
            nameText.text = NPC.GetComponent<DialogActivator>().setSpeakerName();
            if (currentLine >= newLines.Length)
            {

                dialogBox.SetActive(false);

                currentLine = 0;
                ShouldActivateQuestAtEnd(currentQuestToMark);
                NPC.GetComponent<DialogActivator>().npcLineGroup[dialogPosition].isDialogCompleted = true;
                NPC.GetComponent<DialogActivator>().npcQuestGroup[dialogPosition].isQuestCompleted = true;
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = newLines[currentLine];
                currentLine++;
            }


        }



        public void ShouldActivateQuestAtEnd(string questName)
        {
            questToMark = questName;
            for (int i = 0; i < QuestManager.instance.questMarkerComplete.Length; i++)
            {
                if (QuestManager.instance.questMarkerComplete[i] == false)
                {
                    QuestManager.instance.currentQuest[i] = true;
                    return;

                }
            }
        }

        public bool getQuestMarker()
        {
            return shouldMarkQuest;
        }


    }

}