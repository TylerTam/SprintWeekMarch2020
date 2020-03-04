using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHealthEvent : UnityEngine.Events.UnityEvent { }
public class PlayerHealth : MonoBehaviour
{
    public int m_health;
    public int m_defaultDamage,m_spookyTimeDamage;

    public static PlayerHealth Instance;

    public PlayerHealthEvent m_playersDeathEvent;
    private SpookyTimeManager m_spookyTimeManager;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_spookyTimeManager = SpookyTimeManager.Instance;
    }

    public void TakeDamage()
    {
        m_health -= (m_spookyTimeManager.IsSpookyTimeActive()) ? m_spookyTimeDamage : m_defaultDamage;
        if (m_health <= 0)
        {
            m_playersDeathEvent.Invoke();
        }

    }


}
