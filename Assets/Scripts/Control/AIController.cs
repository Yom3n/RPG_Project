using System;
using Combat;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject _player;
        private Fighter _fighter;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
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
    }
}