using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PlayerDetection : MonoBehaviour
{
    public float m_detectionRange = 2;
    public LayerMask m_detectionMask, m_blockingMask;
    private AI_Controller m_aiCont;

    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;


    private GameObject m_currentTarget;
    private void Start()
    {
        m_aiCont = GetComponent<AI_Controller>();
    }

    private void Update()
    {
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        if (m_currentTarget == null)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, m_detectionRange, m_detectionMask);
            if (col != null)
            {
                if (!Physics2D.Linecast(transform.position, col.transform.position, m_blockingMask))
                {
                    m_currentTarget = col.gameObject;
                    m_aiCont.SetPlayerTransform(m_currentTarget.transform);
                    m_aiCont.ChangeState(AI_Controller.AIStates.CHASE);
                }
                
            }
        }
        else
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, m_detectionRange, m_detectionMask);
            bool currentTargetInBounds = false;
            foreach(Collider2D col in cols)
            {
                if(col.gameObject == m_currentTarget)
                {
                    currentTargetInBounds = true;
                    break;
                }
            }
            if (!currentTargetInBounds)
            {
                m_currentTarget = null;
                m_aiCont.SetPlayerTransform(null);
                m_aiCont.ChangeState(AI_Controller.AIStates.WANDER);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);
    }
}
