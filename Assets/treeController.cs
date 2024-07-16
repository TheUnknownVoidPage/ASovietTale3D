using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // The tree prefab to spawn
    public int treeCount = 50; // Number of trees to spawn
    public float spawnRadius = 50f; // Radius in which trees can spawn
    public float exclusionRadius = 10f; // Radius of the area where trees should not spawn
    public float minDistanceBetweenTrees = 2f; // Minimum distance between trees
    
    
    
    public GameObject grassPrefab; // The tree prefab to spawn
    public int grassCount = 50; // Number of trees to spawn
    public float grassSpawnRadius = 50f; // Radius in which trees can spawn
    public float minDistanceBetweenGrass = 2f; // Minimum distance between trees

    private List<Vector3> treePositions = new List<Vector3>();
    private List<Vector3> grassPositions = new List<Vector3>();

    void Start()
    {
        SpawnTrees();
        SpawnGrass();
    }

    void SpawnTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(treePrefab, spawnPosition, Quaternion.identity);
                treePositions.Add(spawnPosition);
            }
        }
    }
    void SpawnGrass()
    {
        for (int i = 0; i < grassCount; i++)
        {
            Vector3 spawnPosition = GetRandomPositionGrass();
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
                grassPositions.Add(spawnPosition);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        int maxAttempts = 100; // Maximum attempts to find a valid position
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            // Check if the position is outside the exclusion radius
            if (randomPosition.magnitude < exclusionRadius)
                continue;

            // Check if the position is far enough from other trees
            bool validPosition = true;
            foreach (Vector3 pos in treePositions)
            {
                if (Vector3.Distance(pos, randomPosition) < minDistanceBetweenTrees)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
                return randomPosition;
        }

        return Vector3.zero; // Return zero vector if no valid position is found
    }
    
    Vector3 GetRandomPositionGrass()
    {
        int maxAttempts = 100; // Maximum attempts to find a valid position
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-grassSpawnRadius, grassSpawnRadius),
                0f,
                Random.Range(-grassSpawnRadius, grassSpawnRadius)
            );

            // Check if the position is outside the exclusion radius


            // Check if the position is far enough from other trees
            bool validPosition = true;
            foreach (Vector3 pos in grassPositions)
            {
                if (Vector3.Distance(pos, randomPosition) < minDistanceBetweenGrass)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
                return randomPosition;
        }

        return Vector3.zero; // Return zero vector if no valid position is found
    }
}