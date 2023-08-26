using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;

        private Animator _animator;

        private ActionScheduler _actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 globalVelocity = _navMeshAgent.velocity;
            // Converts global velocity to local
            Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity);
            float speed = localVelocity.z;
            _animator.SetFloat("forwardSpeed", speed);
        }

        /// <summary>
        /// MoveTo is called for example by Fighter every frame
        /// StartMoveAction is called wen you start move, and Fighter action can be canceled
        /// </summary>
        /// <param name="destination"></param>
        public void StartMoveToAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(destination);
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}