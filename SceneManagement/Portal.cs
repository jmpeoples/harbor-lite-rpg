using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Stats;
using RPG.Attributes;
using RPG.Combat;
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {

            A, B, C, D, E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        Health health;
        BaseStats baseStats;
        Experience experience;

        GameObject player;
        SimpleSavingSystem wrapper;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {

                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);



            Fader fader = FindObjectOfType<Fader>();


            yield return fader.FadeOut(fadeOutTime);

            // Save Current Level
            savePlayer();

            // Load current level
            yield return SceneManager.LoadSceneAsync(sceneToLoad);


            Portal otherPortal = GetOtherPortal();

            // also loads player data
            UpdatePlayer(otherPortal);

            // wrapper.SaveData();

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Experience experience = player.GetComponent<Experience>();
            Health health = player.GetComponent<Health>();
            Fighter fighter = player.GetComponent<Fighter>();
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
            experience.SetPoints(PlayerPrefs.GetFloat("Player_ExperiencePoints"));
            health.SetHP(PlayerPrefs.GetFloat("Player_Health"));
            fighter.LoadWeapon(PlayerPrefs.GetString("Player_Weapon"));
        }

        private void savePlayer()
        {
            player = GameObject.FindWithTag("Player");
            Experience experience = player.GetComponent<Experience>();
            Health health = player.GetComponent<Health>();
            BaseStats baseStats = player.GetComponent<BaseStats>();
            Fighter fighter = player.GetComponent<Fighter>();
            //Save current Game

            PlayerPrefs.SetFloat("Player_ExperiencePoints", experience.GetPoints());
            PlayerPrefs.SetFloat("Player_Health", health.GetHealthPoints());
            PlayerPrefs.SetFloat("Player_Max_Health", health.GetMaxHealthPoints());
            PlayerPrefs.SetInt("Player_Level", baseStats.GetLevel());
            PlayerPrefs.SetString("Player_Weapon", fighter.GetWeapon());
            Debug.Log(fighter.GetWeapon());

        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }
    }
}
