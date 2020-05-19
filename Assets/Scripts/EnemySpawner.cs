using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private float cooldown = 3f;

    private float radius = 40f;
    private float currentCooldown = 0f;

    void Start()
    {
        if (DifficultyManager.instance == null) {
            return;
        }

        int difficulty = DifficultyManager.instance.GetDifficulty();
        if (difficulty == 0)
        {
            cooldown = 5f;
        } else if (difficulty == 1)
        {
            cooldown = 4f;
        } else if (difficulty == 2)
        {
            cooldown = 2f;
        }
    }

    void Update()
    {
        if (currentCooldown <= 0f)
        {
            RandomSpawn();
            currentCooldown = cooldown;
        }
        currentCooldown -= Time.deltaTime;
    }

    void RandomSpawn()
    {
        Vector2 randomInCircle = Random.insideUnitCircle.normalized * radius;
        Vector3 randomPosition = transform.position + new Vector3(randomInCircle.x, 1, randomInCircle.y);
        Instantiate(enemy, randomPosition, Quaternion.identity);
    }
}
