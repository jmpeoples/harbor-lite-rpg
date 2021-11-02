using UnityEngine;
using UnityEngine.UI;


namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            // cache health on awake
            health = GameObject.FindWithTag("Player").GetComponent<Health>();

        }

        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}