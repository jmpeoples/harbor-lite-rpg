using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // Start is called before the first f
    [SerializeField] float talkDistance = 5f;
    GameObject player;
    void Start()
    {

    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (InTalkRangeOfPlayer())
        {
            Debug.Log("Can Talk");
        }
    }

    private bool InTalkRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer <= talkDistance;
    }
}
