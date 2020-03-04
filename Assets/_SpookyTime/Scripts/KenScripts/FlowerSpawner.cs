using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public static bool isPlayerCarryingFlower = false;

    public static bool isFlowersInScene = false;
    int RandomNumber;

    private SpookyTimeManager m_spookyTime;

    [SerializeField] Transform FlowerPrefab;

    [SerializeField] Transform FlowerSpawn1;
    [SerializeField] Transform FlowerSpawn2;
    [SerializeField] Transform FlowerSpawn3;
    [SerializeField] Transform FlowerSpawn4;
    void Start()
    {
        m_spookyTime = SpookyTimeManager.Instance;
    }

    void Update()
    {
        if(m_spookyTime.IsSpookyTimeActive() && isFlowersInScene == false)
        {
            SpawnFlower();
        }
    }

    void SpawnFlower()
    {
        RandomNumber = Random.Range(1, 4);
        if (RandomNumber == 1) { Instantiate(FlowerPrefab, FlowerSpawn1); isFlowersInScene = true; }
        if (RandomNumber == 2) { Instantiate(FlowerPrefab, FlowerSpawn2); isFlowersInScene = true; }
        if (RandomNumber == 3) { Instantiate(FlowerPrefab, FlowerSpawn3); isFlowersInScene = true; }
        if (RandomNumber == 4) { Instantiate(FlowerPrefab, FlowerSpawn4); isFlowersInScene = true; }
    }
}
