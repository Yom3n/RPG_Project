using System;
using System.Collections;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [FormerlySerializedAs("WeaponRange")] [SerializeField]
        float weaponRange = 1f;

        [SerializeField] private float timeBetweenAttacks = 1f;

        private Mover mover;

        private float _timeSinceLastAttack;

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
            mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            print(_timeSinceLastAttack);
            if (_target == null) return;
            if (IsTargetInRange())
            {
                mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                mover.MoveTo(_target.transform.position);
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
        }
    }
}