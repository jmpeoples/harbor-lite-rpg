using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;
        // public delegate void ExperienceGainedDelegate();
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {

            // Call everything in delegate list

            experiencePoints += experience;
            onExperienceGained();

        }

        public float GetPoints()
        {
            return experiencePoints;
        }

        public float SetPoints(float xp)
        {
            experiencePoints = xp;
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }


        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }

}

