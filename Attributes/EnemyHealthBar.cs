using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class EnemyHealthBar : MonoBehaviour
    {


        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;

        Health health;
        private void Awake()
        {
            health = gameObject.GetComponentInParent<Health>();
        }

        void Update()
        {
            //turn off health bar for enenmy when dead
            if (Mathf.Approximately(health.GetFraction(), 0))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;

            foreground.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}
