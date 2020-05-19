using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private GameObject dirt;
    [SerializeField] private Transform dirtSpawner;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private GameObject deadParticles;

    private EnemyMovement enemyMovement;
    private float minCoolDown = 3;
    private float maxCoolDown = 5;
    private float cooldown;
    private float currentCooldown = 0;
    private bool dead = false;
    private IEnumerator attackCoroutine;

    void Start()
    {
        enemyMovement = transform.GetComponent<EnemyMovement>();
        cooldown = Random.Range(minCoolDown, maxCoolDown);
    }

    void Update()
    {
        if (IsDead()) return;
        if (currentCooldown <= 0) return;
        currentCooldown -= Time.deltaTime;
    }

    public void ThrowDirt(Vector3 direction)
    {
        if (currentCooldown > 0) return;
        currentCooldown = cooldown;
        attackCoroutine = ThrowRock(direction);
        StartCoroutine(attackCoroutine);
    }

    IEnumerator ThrowRock(Vector3 direction)
    {
        animator.SetTrigger("throwRock");
        yield return new WaitForSeconds(1.5f);
        GameObject dirtInstance = Instantiate(dirt, dirtSpawner.position, Quaternion.identity);
        Rigidbody rb = dirtInstance.GetComponent<Rigidbody>();
        float force = Random.Range(10, 40);
        rb.AddForce(direction * force, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        animator.ResetTrigger("throwRock");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (IsDead()) return;
        if (!collider.CompareTag("Skate")) return;
        Skate skate = collider.GetComponent<Skate>();
        if (skate == null || !skate.IsFlying()) return;

        Vector3 position = collider.ClosestPoint(transform.position);
        Instantiate(hitParticles, position, Quaternion.identity);
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            animator.ResetTrigger("throwRock");
        }
        Die();
    }

    void Die()
    {
        animator.SetTrigger("die");
        dead = true;
        enemyMovement.Die();
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(5);
        Instantiate(deadParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return dead;
    }
}
