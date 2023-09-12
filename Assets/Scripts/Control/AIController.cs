using System;
using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionDuration = 3f;
        private Vector3 _initialPosition;

        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private ActionScheduler _actionScheduler;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            _initialPosition = transform.position;
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_health.IsDead()) return;

            if (IsInAttackRange() && _fighter.IsTargetValid(_player))
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }

            else if (_timeSinceLastSawPlayer < suspicionDuration)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehaviour();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            _mover.StartMoveToAction(_initialPosition);
        }

        private void SuspicionBehavior()
        {
            _actionScheduler.CancelCurrent();
        }

        private void AttackBehaviour()
        {
            _fighter.Attack(_player);
        }

        private bool IsInAttackRange()
        {
            var distancetoPlayer = Vector3.Distance(transform.position, _player.transform.position);
            return distancetoPlayer < chaseDistance;
        }

        //Called by unity. Don't delete!  Draw only when object is selected in scene
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}