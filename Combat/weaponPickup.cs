using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class weaponPickup : MonoBehaviour
    {
        // Start is called before the first frame update


        // [SerializeField] float respawnTime = 5;
        public Weapon swordTrigger = null;
        public Weapon bowTrigger = null;
        GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }


        public void equipSword()
        {
            player.GetComponent<Fighter>().EquipWeapon(swordTrigger);

        }

        public void equipBow()
        {
            player.GetComponent<Fighter>().EquipWeapon(bowTrigger);
        }




        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.tag == "Player")
        //     {
        //         other.GetComponent<Fighter>().EquipWeapon(weapon);
        //         StartCoroutine(HideForSeconds(respawnTime));
        //     }
        // }

        // private IEnumerator HideForSeconds(float seconds)
        // {
        //     ShowPickup(false);

        //     yield return new WaitForSeconds(seconds);
        //     ShowPickup(true);
        // }

        // private void ShowPickup(bool shouldShow)
        // {
        //     GetComponent<Collider>().enabled = shouldShow;
        //     foreach (Transform child in transform)
        //     {

        //         child.gameObject.SetActive(shouldShow);
        //     }
        // }
    }

}