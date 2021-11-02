using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        LazyValue<float> healthPoints;
        bool isDead = false;
        public TriggerDoorController triggerDoor = null;

        Youdied deadMessage;


        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {


        }

        // public Rigidbody rb;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);


        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void Start()
        {
            healthPoints.ForceInit();


        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()

        {
            return isDead;
        }



        public void TakeDamage(GameObject instigator, float damage)
        {

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value == 0)
            {
                Die();
                triggerDoor.dropDoor();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public bool canHeal()
        {
            float currentHealthPoints = GetHealthPoints();
            float maxHealthPoints = GetMaxHealthPoints();

            if (currentHealthPoints < maxHealthPoints)
            {
                return true;
            }
            return false;
        }

        public void healPlayer(float healthPackPoints)
        {
            float currentHealthPoints = GetHealthPoints();
            float maxHealthPoints = GetMaxHealthPoints();
            float healthAppliedPoints = currentHealthPoints + healthPackPoints;

            if (canHeal())
            {
                if (healthAppliedPoints >= maxHealthPoints)
                {
                    Debug.Log("Health Over Max" + " " + healthAppliedPoints);
                    healthPoints.value = maxHealthPoints;
                    return;
                }
                // how to stop health from increasing over max
                healthPoints.value = healthPackPoints + healthPoints.value;
                HealPackManager.instance.subtractHealthPack();
                HealPackManager.instance.playHealthAnimation();
            }

        }

        public float GetMaxHealthPoints()
        {
            // forward one of the dependencies forward the method on
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetHP()
        {
            return healthPoints.value;
        }
        public float SetHP(float hp)
        {
            healthPoints.value = hp;
            return healthPoints.value;
        }
        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            StartCoroutine(gameObject.GetComponentInChildren<deathDissolve>().dissolve());

        }

        private void AwardExperience(GameObject instigator)
        {

            Experience experience = instigator.GetComponent<Experience>();
            if (instigator == null) return;
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));

        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}