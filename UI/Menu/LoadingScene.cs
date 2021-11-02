using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{

    public float waitToLoad;

    SimpleSavingSystem simpleSave;
    // Start is called before the first frame update
    void Start()
    {
        simpleSave = FindObjectOfType<SimpleSavingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

                simpleSave.LoadData();

            }
        }
    }
}
