using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDiggingEvent : UnityEngine.Events.UnityEvent { }
public class PlayerDigging : MonoBehaviour
{
    public KeyCode m_digKey;
    public LayerMask m_digLayer;
    public float m_digRadius = .25f;

    private IsPlayerOnGrave m_currentGrave;

    public PlayerDiggingEvent m_startDigging;
    private bool m_canDig = true;
    // Update is called once per frame
    void Update()
    {
        if (m_canDig)
        {
            if (CheckInput())
            {
                m_startDigging.Invoke();
                if (GraveInRadius())
                {
                    m_canDig = false;
                    
                    m_currentGrave.playerDiggedGrave();
                }
            }
        }
    }

    private bool GraveInRadius()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, m_digRadius, m_digLayer);
        if (col != null)
        {
            m_currentGrave = col.gameObject.GetComponent<IsPlayerOnGrave>();
        }
        else
        {
            m_currentGrave = null;
        }

        return m_currentGrave != null;
    }

    private bool CheckInput()
    {
        return Input.GetKeyDown(m_digKey);
    }

    public void ChangeDigState(bool p_activeState)
    {
        m_canDig = p_activeState;
    }

}
