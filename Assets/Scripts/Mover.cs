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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

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

    private void MoveToCursor()
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray: ray, hitInfo: out hit);
        if (hasHit)
        {
            _navMeshAgent.SetDestination(hit.point);
        }
    }
}