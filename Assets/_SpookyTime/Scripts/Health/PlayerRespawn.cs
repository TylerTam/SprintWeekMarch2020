using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRespawnEvent : UnityEngine.Events.UnityEvent { }

/// <summary>
/// The script on the player that starts the respawn and death animations;
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    public Transform m_spawnPoint;
    public float m_dieTime,m_respawnTime;
    public PlayerInput m_playerInput;
    public Collider2D m_playerCol;
    public Entity_MovementController m_movementController;

    private PlayerHealth m_playerHealth;

    public RespawnEvents m_respawnEvents;
    [System.Serializable]
    public struct RespawnEvents
    {
        public PlayerRespawnEvent m_diedEvent;
        public PlayerRespawnEvent m_spawnEvent;
    }
    private void Start()
    {
        m_playerHealth = PlayerHealth.Instance;
    }
    public void OnDied()
    {
        m_playerHealth.TakeDamage();
        m_playerInput.ChangeInputState(false);
        StartCoroutine(RespawnMe());
    }

    private IEnumerator RespawnMe()
    {
        float timer = 0;
        m_playerCol.enabled = false;

        #region Death Time
        m_movementController.ResetMe();
        m_movementController.enabled = false;
        m_respawnEvents.m_diedEvent.Invoke();

        while (timer > m_dieTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        #endregion

        #region Spawn Time
        timer = 0;
        while (timer > m_respawnTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = m_spawnPoint.position;
        m_playerInput.ChangeInputState(true);
        m_respawnEvents.m_spawnEvent.Invoke();
        m_movementController.enabled = true;
        m_playerCol.enabled = true;
        #endregion
    }
}
