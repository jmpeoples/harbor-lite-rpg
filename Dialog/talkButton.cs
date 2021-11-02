using UnityEngine;
using RPG.Dialog;
using UnityEngine.UI;
public class talkButton : MonoBehaviour
{
    DialogActivator npc;

    public GameObject button = null;

    private void Start()
    {
        npc = GameObject.FindWithTag("NPC").GetComponent<DialogActivator>();

    }

    private void Update()
    {
        if (npc.canActivate)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }


    public void npcSpeak()
    {
        npc.npcTalk();
    }
}