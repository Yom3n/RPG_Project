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
        [SerializeField] private float weaponDamage = 3f;

        [SerializeField] private float timeBetweenAttacks = 1f;


        private float _timeSinceLastAttack;

        private Mover _mover;
        private Health _target;
        private ActionScheduler _actionScheduler;
        private Animator _animator;

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
            _target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            _animator.SetTrigger("stopAttack");
            _target = null;
        }

        private void Start()
        {
            _timeSinceLastAttack = timeBetweenAttacks;
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (_target.IsDead()) return;
            if (IsTargetInRange())
            {
                _mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                _mover.MoveTo(GetTargetPosition());
            }
        }


        private void AttackBehaviour()
        {
            if (timeBetweenAttacks <= _timeSinceLastAttack)
            {
                //This will trigger Hit() event 
                _animator.SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }


        /// Animation event! 
        private void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage);
        }

        private Vector3 GetTargetPosition()
        {
            return _target.transform.position;
        }
    }
}