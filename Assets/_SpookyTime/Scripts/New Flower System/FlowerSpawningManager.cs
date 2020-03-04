using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawningManager : MonoBehaviour
{
    public List<Transform> m_spawnLocations;
    public FlowerObject m_flowerSpawn;

    public void SpawnFlower()
    {
        m_flowerSpawn.gameObject.SetActive(true);
        m_flowerSpawn.transform.parent = null;
        m_flowerSpawn.transform.position = m_spawnLocations[Random.Range(0, m_spawnLocations.Count)].position;
        m_flowerSpawn.ResetFlower();
        
    }


}
