using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        Health health;



        private void Awake()
        {
            // navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            // health = gameObject.GetComponent<Health>();

        }

        private void Start()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            health = gameObject.GetComponent<Health>();
        }

        // Update is called once per frame


        void Update()

        {
            // navMeshAgent.enabled = !health.IsDead();
            // Debug.Log("Health is death" + " " + health.IsDead());
            // Debug.Log("Nav mesh is" + " " + navMeshAgent.enabled);
            UpdateAnimator();

        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {

            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }



        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
