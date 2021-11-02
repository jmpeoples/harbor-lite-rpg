using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Stats;
public class SimpleSavingSystem : MonoBehaviour
{

    Experience experience;

    BaseStats baseStats;
    Health health;
    GameObject player;

    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

    }
    void Start()
    {
        health = player.GetComponent<Health>();
        experience = player.GetComponent<Experience>();
        baseStats = player.GetComponent<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }
    }

    public void SaveData()
    {

        // PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        // PlayerPrefs.SetFloat("Player_Position_x", player.transform.position.x);
        // PlayerPrefs.SetFloat("Player_Position_y", player.transform.position.y);
        // PlayerPrefs.SetFloat("Player_Position_z", player.transform.position.z);
        PlayerPrefs.SetFloat("Player_ExperiencePoints", experience.GetPoints());
        PlayerPrefs.SetFloat("Player_Health", health.GetHealthPoints());
        PlayerPrefs.SetFloat("Player_Max_Health", health.GetMaxHealthPoints());
        PlayerPrefs.SetInt("Player_Level", baseStats.GetLevel());
    }

    public void LoadData()
    {

        // player.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
        experience.SetPoints(PlayerPrefs.GetFloat("Player_ExperiencePoints"));
        health.SetHP(PlayerPrefs.GetFloat("Player_Health"));
        baseStats.currentLevel.value = PlayerPrefs.GetInt("Player_Level");

    }


}
