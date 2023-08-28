using System;
using System.Collections;
using Core;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 1f;

        [SerializeField] private float timeBetweenAttacks = 1f;


        private float _timeSinceLastAttack;

        private Mover _mover;
        private CombatTarget _target;
        private ActionScheduler _actionScheduler;

        public bool IsTargetInRange()
        {
            if (_target == null) return false;
            Vector3 targetPosition = _target.transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            return distanceToTarget <= weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            _actionScheduler.StartAction(this);
            _target = target;
        }

        public void Cancel()
        {
            _target = null;
        }

        private void Start()
        {
            _timeSinceLastAttack = timeBetweenAttacks;
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (IsTargetInRange())
            {
                _mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                _mover.MoveTo(_target.transform.position);
            }
        }

        void AttackBehaviour()
        {
            if (timeBetweenAttacks <= _timeSinceLastAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }


        /// Animation event! 
        private void Hit()
        {
            _target.GetComponent<Health>().TageDamage(1f);
        }
    }
}