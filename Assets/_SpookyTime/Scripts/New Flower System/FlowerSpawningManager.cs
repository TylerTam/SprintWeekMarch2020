using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawningManager : MonoBehaviour
{
    public List<Transform> m_spawnLocations;
    public FlowerObject m_flowerSpawn;
    public GameObject m_flowerGlow;

    private SpookyTimeManager m_spookyTime;
    

    private void Start()
    {
        m_spookyTime = SpookyTimeManager.Instance;
    }
    public void SpawnFlower()
    {
        m_flowerSpawn.gameObject.SetActive(true);
        m_flowerSpawn.transform.parent = null;
        m_flowerSpawn.transform.position = m_spawnLocations[Random.Range(0, m_spawnLocations.Count)].position;
        m_flowerSpawn.ResetFlower();
        
    }


    private void Update()
    {
        m_flowerGlow.SetActive(m_spookyTime.IsSpookyTimeActive());
    }
    public void FlowerPickedUp()
    {
        m_flowerGlow.transform.position = Vector3.zero;
    }
    public void FlowerDropped()
    {
        m_flowerGlow.transform.position = m_flowerSpawn.transform.position;
    }

    


}
