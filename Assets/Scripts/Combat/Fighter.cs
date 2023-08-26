using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float WeaponRange = 1.0f;
        private Mover mover;

        private CombatTarget _target;
        private ActionScheduler _actionScheduler;

        private void Start()
        {
            mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_target == null) return;
            if (isTargetInRange())
            {
                mover.StopMove();
            }
            else

            {
                mover.MoveTo(_target.transform.position);
            }
        }

        public bool isTargetInRange()
        {
            Vector3 targetPosition = _target.transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            return distanceToTarget <= WeaponRange;
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
    }
}