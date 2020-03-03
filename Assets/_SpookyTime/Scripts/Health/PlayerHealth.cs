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
    private void Awake()
    {
        Instance = this;
    }

    public void TakeDamage()
    {
        m_health -= (TemporaryDataContainer.TemporarySpookyTimeActivated) ? m_defaultDamage : m_spookyTimeDamage;
        if (m_health <= 0)
        {
            m_playersDeathEvent.Invoke();
        }

    }


}
