using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
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

    public void MoveTo(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
    }
}