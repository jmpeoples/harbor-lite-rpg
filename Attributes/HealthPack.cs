using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
public class HealthPack : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    Health health;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // Add to health Inventory;
            // Add plus when game object is hit
            HealPackManager.instance.addHealthPack();
        }

    }
}
