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


        private float _timeSinceLastAttack = Mathf.Infinity;

        private Mover _mover;
        private Health _target;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        
        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        public bool IsTargetValid(GameObject combatTarget)
        {
            return combatTarget != null && combatTarget.GetComponent<Health>().IsAlive();
        }

        public bool IsTargetInRange()
        {
            if (_target == null) return false;
            Vector3 targetPosition = _target.transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            return distanceToTarget <= weaponRange;
        }

        public void Attack(GameObject target)
        {
            _actionScheduler.StartAction(this);
            _target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            _animator.SetTrigger("stopAttack");
            _animator.ResetTrigger("attack");
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
            transform.LookAt(_target.transform);
            if (timeBetweenAttacks <= _timeSinceLastAttack)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            //This will trigger Hit() event 
            _animator.SetTrigger("attack");
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