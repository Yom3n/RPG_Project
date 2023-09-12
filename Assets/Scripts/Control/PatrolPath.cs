using System;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WaypointGizmoRadius = 0.25f;

        private void OnDrawGizmos()
        {
            Transform previousWaypoint = null;
            foreach (Transform waypoint in transform) //Iterates over gameObject children transforms
            {
                Gizmos.DrawSphere(waypoint.position, WaypointGizmoRadius);

                if (previousWaypoint != null)
                {
                    Gizmos.DrawLine(waypoint.position, previousWaypoint.position);
                }

                previousWaypoint = waypoint;
            }
        }
    }
}