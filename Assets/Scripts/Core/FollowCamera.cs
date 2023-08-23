using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private Vector3 vectorToTarget;

        // Start is called before the first frame update
        void Start()
        {
            vectorToTarget = transform.position - target.transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.transform.position + vectorToTarget;
        }
    }
}