using System;
using Combat;
using Core;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject _player;
        private Fighter _fighter;
        private Health _health;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead()) return;

            if (IsInAttackRange() && _fighter.IsTargetValid(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
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