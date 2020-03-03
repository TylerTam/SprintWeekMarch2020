using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    public List<Transform> m_patrolPoints;
    public static AI_Manager Instance;

    public GridNavigation m_navGrid;

    [Header("Debugging")]
    public List<GameObject> m_debuggingGhosts;
    private void Start()
    {
        Instance = this;
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
}
