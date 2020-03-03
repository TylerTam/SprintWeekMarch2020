using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    private GridNavigation_Agent m_navAgent;

    public enum AIStates
    {
        CHASE, WANDER, SPAWING, STUN
    }

    public AIStates m_currentState;
    public Transform m_playerTransform;
    public float m_stoppingDistance;
    public float m_spawnInTime;
    [Header("Movement Variables")]
    public float m_defaultLerpTime = .2f;
    public float m_spookyTimeLerpTime = .15f;


    [Tooltip ("The time that they are stunned")]
    public float m_stunTime;
    private List<Node> m_path;
    private Entity_MovementController m_movementController;
    private AI_Manager m_aiManager;
    
    private void Start()
    {
        m_navAgent = GetComponent<GridNavigation_Agent>();
        m_movementController = GetComponent<Entity_MovementController>();
        m_path = m_navAgent.CreatePath(transform.position, m_playerTransform.position);
        m_aiManager = AI_Manager.Instance;
    }

    private void OnEnable()
    {
        ChangeState(AIStates.SPAWING);
    }
    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (!m_movementController.IsMoving())
        {
            switch (m_currentState)
            {
                case AIStates.CHASE:
                    CalculatePathToPlayer();

                    break;
                case AIStates.WANDER:
                    CalculatePathToRandomPoint();
                    break;
            }
        }

        MoveAI();
        
    }

    /// <summary>
    /// Changes the state of the ai.
    /// </summary>
    /// <param name="p_newState"></param>
    public void ChangeState(AIStates p_newState)
    {
        m_currentState = p_newState;
        switch (p_newState)
        {
            case AIStates.CHASE:
                break;
            case AIStates.WANDER:
                m_currentPatrolPoint = m_aiManager.GiveNewPatrolPoint();
                m_path = m_navAgent.CreatePath((Vector2)transform.position, (Vector2)m_currentPatrolPoint.position);
                break;
            case AIStates.SPAWING:
                StartCoroutine(GhostSpawn());
                break;
            case AIStates.STUN:
                StartCoroutine(GhostStun());
                break;
        }
    }

    #region Movement Functions

    private void MoveAI()
    {
        if (m_path.Count > 0)
        {
            if (CloseToPoint(m_path[0].m_worldPosition))
            {
                m_path.RemoveAt(0);
            }
            else
            {
                MoveToPoint(m_path[0].m_worldPosition);
            }
        }
    }

    private bool CloseToPoint(Vector3 p_targetPoint)
    {
        Debug.DrawLine(transform.position, (Vector2)p_targetPoint, Color.magenta);
        if (Vector3.Distance((Vector2)p_targetPoint, (Vector2)transform.position) < m_stoppingDistance)
        {
            return true;
        }
        return false;
    }

    private void MoveToPoint(Vector3 p_targetPoint)
    {
        m_movementController.MoveCharacter((p_targetPoint - transform.position), IsSpookyTime() ? m_spookyTimeLerpTime : m_defaultLerpTime);
    }

    #endregion

    #region PathCalculation
    private Vector3 m_lastPlayerPos = new Vector3();
    public float m_recalculatePathDistance;

    private void CalculatePathToPlayer()
    {
        if (Vector3.Distance((Vector2)m_playerTransform.position, (Vector2) m_lastPlayerPos) > m_recalculatePathDistance)
        {
            m_path = m_navAgent.CreatePath(transform.position, m_playerTransform.position);
        }
    }

    private Transform m_currentPatrolPoint;
    private void CalculatePathToRandomPoint()
    {
        if (Vector3.Distance((Vector2)transform.position, (Vector2)m_currentPatrolPoint.position) < m_stoppingDistance)
        {
            m_currentPatrolPoint = m_aiManager.GetNextPatrolPoint(m_currentPatrolPoint);
            m_path = m_navAgent.CreatePath((Vector2)transform.position, (Vector2)m_currentPatrolPoint.position);
        }
    }
    #endregion
    
    public void SetPlayerTransform(Transform p_playerTransform)
    {
        m_playerTransform = p_playerTransform;
    }


    /// <summary>
    /// Fades the ghost into the game, while having them idle in postion for a bit
    /// </summary>
    /// <returns></returns>
    private IEnumerator GhostSpawn()
    {
        float timer = 0;
        while (timer < m_spawnInTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        ChangeState(AIStates.WANDER);
    }

    private IEnumerator GhostStun()
    {
        float timer = 0;
        while(timer < m_stunTime){
            timer += Time.deltaTime;
            yield return null;
        }
        ChangeState(AIStates.WANDER);
    }


    [Header("Temp Sppoky Time")]
    public bool m_spookyTimeActive = false;

    private bool IsSpookyTime()
    {
        return m_spookyTimeActive;
    }
}
