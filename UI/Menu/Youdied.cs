using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Attributes
{
    public class Youdied : MonoBehaviour
    {
        Health health;

        public GameObject deathMeassage = null;
        // Start is called before the first frame update
        private void Awake()
        {
            // cache health on awake
            health = GameObject.FindWithTag("Player").GetComponent<Health>();

            deathMeassage.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            showDiedMessage();
        }


        public void showDiedMessage()
        {


            float playerHP = health.GetHealthPoints();
            if (playerHP == 0)
            {
                //show message
                deathMeassage.SetActive(true);
            }
            else
            {
                deathMeassage.SetActive(false);
            }
        }

    }
}
