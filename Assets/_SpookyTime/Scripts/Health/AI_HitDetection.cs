using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HitDetection : MonoBehaviour
{
    public float m_radius;

    public LayerMask m_hitLayer;
    private AI_PlayerDetection m_playerDetection;
    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;

    private bool m_canCollide = true;
    public void ChangeCollisionState(bool p_active)
    {
        m_canCollide = p_active;
    }

    private void OnDrawGizmos()
    {
        if (!m_debugging || !m_canCollide) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    private void Update()
    {
        if (!m_canCollide) return;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, m_radius, m_hitLayer);
        foreach(Collider2D col in cols)
        {
            col.transform.parent.GetComponent<PlayerRespawn>().OnDied();
            m_playerDetection.KilledPlayer(col.transform.parent.gameObject);
        }
    }
}
