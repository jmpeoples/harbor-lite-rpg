using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthPackDisplay : MonoBehaviour
    {



        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0}", HealPackManager.instance.GetHealthPackAmount());
        }

    }
}