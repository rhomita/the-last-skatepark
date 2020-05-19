using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask skateparkLayer;
    [SerializeField] private Animator animator;

    private Transform skatepark;

    private EnemyCombat enemyCombat;
    private NavMeshAgent agent;

    private Vector3 closestSkateparkPosition;

    private float radius = 100f;

    private bool skateparkFound = false;

    private float minSpeed = 4f;
    private float maxSpeed = 10f;



    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        enemyCombat = transform.GetComponent<EnemyCombat>();

        agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (enemyCombat.IsDead()) return;

        if (skateparkFound)
        {
            if (Vector3.Magnitude(closestSkateparkPosition - transform.position) - 1 < agent.stoppingDistance)
            {
                Vector3 direction = (closestSkateparkPosition - transform.position).normalized;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20);
                animator.SetBool("isRunning", false);
                ThrowDirt();
                return;
            }

            animator.SetBool("isRunning", true);
            agent.SetDestination(closestSkateparkPosition);
            return;
        }


        float closestDistance = float.MaxValue;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, skateparkLayer);
        if (hitColliders.Length > 0)
        {
            skateparkFound = true;
            foreach (Collider collider in hitColliders)
            {
                Vector3 position = collider.ClosestPoint(transform.position);
                float distance = Vector3.Magnitude(position - transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSkateparkPosition = position;
                }
            }
        }
    }

    void ThrowDirt()
    {
        Vector3 direction = closestSkateparkPosition - transform.position;
        enemyCombat.ThrowDirt(direction.normalized);
    }

    public void Die()
    {
        Destroy(agent);
    }
}
