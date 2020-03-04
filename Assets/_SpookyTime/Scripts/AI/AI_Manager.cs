using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    public List<Transform> m_patrolPoints;
    public static AI_Manager Instance;

    public GridNavigation m_navGrid;

    public GameObject m_aiPrefab;
    

    [Header("Debugging")]
    public List<GameObject> m_debuggingGhosts;
    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawnInitialGhosts());
    }

    public Transform GetNextPatrolPoint(Transform p_currentPatrolPoint)
    {
        List<Transform> newPatrolPoints = new List<Transform>();
        foreach(Transform patrol in m_patrolPoints)
        {
            if(patrol != p_currentPatrolPoint)
            {
                newPatrolPoints.Add(patrol);
            }
        }
        return newPatrolPoints[Random.Range(0, newPatrolPoints.Count)];
    }

    public Transform GiveNewPatrolPoint()
    {
        return m_patrolPoints[Random.Range(0, m_patrolPoints.Count)];
    }



    #region Debugging
    public void SpawnGhosts()
    {
        foreach(GameObject newGhost in m_debuggingGhosts)
        {
            newGhost.SetActive(true);
        }
    }
    #endregion

    [Header("Spawning AI")]
    public int m_maxAIOnScene;
    private int m_currentAICount;
    public float m_aiSpawnChance;
    private int m_currentSpawnerCounter;
    public List<Transform> m_spawnLocations;
    public float m_initialSpawnGhostTime = 3;

    private IEnumerator SpawnInitialGhosts()
    {
        yield return new WaitForSeconds(m_initialSpawnGhostTime);
        for (int i = 0; i < 4; i++)
        {
            m_currentAICount++;
            GameObject newGhost = Instantiate(m_aiPrefab, m_spawnLocations[i].position, Quaternion.identity);
        }
    }
    public void SpawnAI( Vector3 p_spawnLocation)
    {
        float chance = Random.Range(0f, 1f);
        if (chance < m_aiSpawnChance)
        {
            m_currentSpawnerCounter = 0;
            if (m_currentAICount < m_maxAIOnScene)
            {
                GameObject newGhost = Instantiate(m_aiPrefab, p_spawnLocation, Quaternion.identity);
                m_currentAICount++;
            }
        }
        else
        {
            m_currentSpawnerCounter++;
        }

    }
}
