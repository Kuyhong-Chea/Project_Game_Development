using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    public GameObject candyPrefab;
    public Transform[] spawnPoints;
    public int candyCount = 20;

    void Start()
    {
        List<Transform> freePoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < candyCount && freePoints.Count > 0; i++)
        {
            int r = Random.Range(0, freePoints.Count);

            Instantiate(candyPrefab, freePoints[r].position, Quaternion.identity);

            freePoints.RemoveAt(r);
        }
    }
}