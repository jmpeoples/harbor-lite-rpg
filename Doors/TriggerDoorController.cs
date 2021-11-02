using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Stats;


namespace RPG.Attributes
{


    public class TriggerDoorController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Animator myDoor = null;
        public EnemyFloorLevel GateLevel;

        Fighter currentEnemy;
        Health health;

        GameObject[] floorEnemy;

        // Update is called once per frame

        private void Start()
        {
            // find all GameObjects with Tag Enemy.
            // print each fighters Floor level
            floorEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.O))
            // {

            //     dropDoor();

            // }
        }

        public void dropDoor()
        {
            if (isFloorCleared() == 0)
            {
                myDoor.Play("DropGate", 0, 0.0f);
            }
        }

        public int isFloorCleared()
        {
            int activeFloorEnemies = 0;
            for (int i = 0; i < floorEnemy.Length; i++)
            {
                currentEnemy = floorEnemy[i].GetComponent<Fighter>();
                health = floorEnemy[i].GetComponent<Health>();
                if (currentEnemy.enemyFloorLevel == GateLevel)
                {
                    if (!health.IsDead())
                    {
                        activeFloorEnemies++;
                    }
                }

            }
            Debug.Log("Active Floor Enemies" + " " + activeFloorEnemies);
            return activeFloorEnemies;
        }
    }

}
