using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowThePath : MonoBehaviour
{
    // Stan I - patrol
    [SerializeField]
    private GameObject[] _waypoints;

    private int _currInWayp = 0;
    private int _currDeWayp;

    public NavMeshAgent agent;

    // Stan II - podążanie
    public Transform thePlayer;
    private bool _death = false;

    public float viewRadius;
    public float viewKillingRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public List<Transform> visibleKillingTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    private void Update()
    {
        if (visibleKillingTargets.Count != 0 || _death == true)
        {
            KillPlayer();
        }
        else if (visibleTargets.Count == 0)
        {
            FollowPath();
        }
        else
        {
            FollowPlayer();
        }

    }

// Wykrywanie gracza
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        visibleKillingTargets.Clear();
        Collider[] targetsInViewKillingRadius = Physics.OverlapSphere(transform.position, viewKillingRadius, targetMask);
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int j = 0; j < targetsInViewKillingRadius.Length; j++)
        {
            Transform target = targetsInViewKillingRadius[j].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleKillingTargets.Add(target);
                }
            }
        }
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Patrolowanie
    private void FollowPath()
    {
        if (_waypoints.Length != 0)
        {
            if (_currInWayp <= _waypoints.Length - 1)
            {
                agent.SetDestination(_waypoints[_currInWayp].transform.position);
                if (Vector3.Distance(_waypoints[_currInWayp].transform.position, agent.transform.position) < 3.0f)
                {
                    _currInWayp += 1;
                    _currDeWayp = _waypoints.Length - 1;
                }
            }
            else if (_currDeWayp >= 0)
            {
                agent.SetDestination(_waypoints[_currDeWayp].transform.position);
                if (Vector3.Distance(_waypoints[_currDeWayp].transform.position, agent.transform.position) < 3.0f)
                {
                    _currDeWayp -= 1;
                }
            }
            else
            {
                _currInWayp = 0;
            }
        }
        else
        {
            return;
        }
    }

// Podążanie lub uśmiercenie gracza
    private void FollowPlayer()
    {
        agent.SetDestination(thePlayer.position);
    }

    private void KillPlayer()
    {
        agent.speed *= 2;
        agent.SetDestination(thePlayer.position);
        _death = true;
    }

}

