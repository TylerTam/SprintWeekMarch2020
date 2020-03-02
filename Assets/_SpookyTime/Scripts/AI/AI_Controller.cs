using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    private GridNavigation_Agent m_navAgent;

    public Transform m_playerTransform;
    public float m_stoppingDistance;
    private List<Node> m_path;
    private Entity_MovementController m_movementController;
    private void Start()
    {
        m_navAgent = GetComponent<GridNavigation_Agent>();
        m_movementController = GetComponent<Entity_MovementController>();
        m_path = m_navAgent.CreatePath(transform.position, m_playerTransform.position);
    }

    private void Update()
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
        print("Distance: " + Vector3.Distance((Vector2)p_targetPoint, (Vector2)transform.position));
        Debug.DrawLine(transform.position, (Vector2)p_targetPoint, Color.magenta);
        if (Vector3.Distance((Vector2)p_targetPoint, (Vector2)transform.position) < m_stoppingDistance)
        {
            return true;
        }
        return false;
    }

    private void MoveToPoint(Vector3 p_targetPoint)
    {
        m_movementController.MoveCharacter((p_targetPoint - transform.position));
    }
}
