using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HitDetection : MonoBehaviour
{
    public float m_radius;

    public LayerMask m_hitLayer;

    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;

    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, m_radius, m_hitLayer);
        foreach(Collider2D col in cols)
        {
            col.transform.parent.GetComponent<PlayerRespawn>().OnDied();
        }
    }
}
