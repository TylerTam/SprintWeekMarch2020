using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PlayerDetection : MonoBehaviour
{
    public float m_detectionRange = 2, m_spookyTimeDetectionRadius = 4;
    public LayerMask m_detectionMask, m_blockingMask, m_safezoneMask;
    private AI_Controller m_aiCont;

    public float m_lostDetectionTime;
    private Coroutine m_lostCoroutine;
    private bool m_lostPlayer;

    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;


    private GameObject m_currentTarget;

    private SpookyTimeManager m_spookyTimeManager;

    private void Start()
    {
        m_aiCont = GetComponent<AI_Controller>();
        m_spookyTimeManager = SpookyTimeManager.Instance;
    }

    private void Update()
    {
        if (m_aiCont.IsStunned()) return;
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        
        if (m_currentTarget == null)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, (m_spookyTimeManager.IsSpookyTimeActive()) ? m_spookyTimeDetectionRadius : m_detectionRange, m_detectionMask);
            if (col != null)
            {



                if (!Physics2D.Linecast(transform.position, col.transform.position, m_blockingMask))
                {
                    if (m_lostPlayer)
                    {
                        StopCoroutine(m_lostCoroutine);
                        m_lostPlayer = false;
                    }
                    m_currentTarget = col.gameObject;
                    m_aiCont.SetPlayerTransform(m_currentTarget.transform);
                    m_aiCont.ChangeState(AI_Controller.AIStates.CHASE);
                }
                
            }


           
        }
        else
        {

            
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, (m_spookyTimeManager.IsSpookyTimeActive()) ? m_spookyTimeDetectionRadius : m_detectionRange, m_detectionMask);
            bool currentTargetInBounds = false;
            foreach(Collider2D col in cols)
            {
                if(col.gameObject == m_currentTarget)
                {
                    if (!Physics2D.Linecast(transform.position, col.transform.position, m_blockingMask))
                    {
                        currentTargetInBounds = true;
                        break;
                    }
                }
            }
            if (!currentTargetInBounds)
            {
                if (!m_lostPlayer)
                {
                    m_lostPlayer = true;
                    m_lostCoroutine = StartCoroutine(LostTimer());
                }

            }
        }
    }


    /// <summary>
    /// Detection time for if they lose the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator LostTimer()
    {
        float timer = 0;
        while (timer < m_lostDetectionTime)
        {
            timer += Time.deltaTime;
            yield return null;
            if (Physics2D.Linecast(transform.position, m_currentTarget.transform.position, m_safezoneMask))
            {
                timer = m_lostDetectionTime;
            }
        }


        m_lostPlayer = false;
        m_currentTarget = null;
        m_aiCont.SetPlayerTransform(null);
        if (m_aiCont.IsStunned()) yield break;
        m_aiCont.ChangeState(AI_Controller.AIStates.WANDER);
    }

    public void KilledPlayer(GameObject p_hitPlayer)
    {
        if(p_hitPlayer == m_currentTarget)
        {
            m_currentTarget = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);
    }
}
