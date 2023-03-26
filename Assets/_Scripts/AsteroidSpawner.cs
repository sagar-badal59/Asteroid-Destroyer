using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private Asteroid asteroidPrefab;
    [SerializeField]
    private float trajectoryVariance = 15f;
    [SerializeField]
    private float spawnRate = 2f;
    [SerializeField]
    private float spawnDistance = 15f;
    [SerializeField]
    private int spawnAmount = 1;
    public void InitializeSpawner()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPos = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPos, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
