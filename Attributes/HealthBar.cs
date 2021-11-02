using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {


        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;


        Health playerHealth;

        private void Start()
        {

            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Update()
        {

            foreground.localScale = new Vector3(playerHealth.GetFraction(), 1, 1);

        }
    }
}
