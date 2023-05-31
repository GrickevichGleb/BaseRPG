using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private float maxSpeed = 6f;
        [SerializeField] private float maxNavPathLenght = 20f;

        
        private NavMeshAgent _navMeshAgent;
        private Health _health;
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            // Checks that path is complete and point is reachable 
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if(path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLenght(path) > maxNavPathLenght) return false;

            return true;
        }
        
        
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction); 
            _navMeshAgent.isStopped = false;
           // Clamp01 ensures that value of speedFraction is between 0 and 1;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
        
        private float GetPathLenght(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }


        // ISaveable interface implementation
        
        //Alternative way of saving multiple params
        //Using struct
        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }
        public object CaptureState()
        {
            // Saving multiple params using Dictionary
            /*Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);*/
            
            
            // Saving multiple params using struct
            /*MoverSaveData data2 = new MoverSaveData();
            data2.position = new SerializableVector3(transform.position);
            data2.rotation = new SerializableVector3(transform.eulerAngles);*/
            
            //return data2;
            //return data;

            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            //Returns null if state is not of type SerializableVector3
            //SerializbleVector3 x = state as SerializableVector3;
            
            //Throws exception if state is not of type SerializableVector3
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false; // to prevent interfering with navMeshAgent
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            
            //Restoring data using Dictionary
            /*Dictionary<string, object> data = (Dictionary<string, object>) state;
            GetComponent<NavMeshAgent>().enabled = false; // to prevent interfering with navMeshAgent
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();*/
            
            //Restoring data using struct
            /*MoverSaveData data2 = (MoverSaveData)state;
            transform.position = data2.position.ToVector();
            transform.eulerAngles = data2.rotation.ToVector();*/
            
            
        }
        
    }
}
