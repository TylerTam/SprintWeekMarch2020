﻿using System.Collections;
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
    public Collider2D m_playerDetectionCol, m_playerCollider;
    public Entity_MovementController m_movementController;

    private PlayerHealth m_playerHealth;

    public RespawnEvents m_respawnEvents;

    public GameObject m_visual;
    public SpriteRenderer m_sRend;
    public Color m_diedColor;
    public bool m_isDead;
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

        m_isDead = true;
        m_playerHealth.TakeDamage(this);
        m_playerInput.ChangeInputState(false);
        m_playerInput.m_respawning = true;
        m_sRend.color = m_diedColor;
    }

    public void OtherPlayerDied()
    {
        
        m_playerDetectionCol.enabled = false;
        m_playerCollider.enabled = false;
        m_visual.SetActive(false);
        m_isDead = true;
        m_playerInput.ChangeInputState(false);
        m_playerInput.m_respawning = true;
    }

    private IEnumerator RespawnMe()
    {
        float timer = 0;
        m_playerDetectionCol.enabled = false;
        m_playerCollider.enabled = false;
        #region Death Time
        m_movementController.ResetMe();
        m_movementController.enabled = false;
        m_respawnEvents.m_diedEvent.Invoke();

        while (timer < m_dieTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        #endregion

        #region Spawn Time
        timer = 0;
        while (timer < m_respawnTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        ForceRespawn();
        #endregion
    }

    public void StartRespawnFunction()
    {
        StartCoroutine(RespawnMe());
    }

    public void ForceRespawn()
    {
        
        m_visual.SetActive(true);
        transform.position = m_spawnPoint.position;
        m_playerInput.m_respawning = false;
        m_playerInput.ChangeInputState(true);
        m_respawnEvents.m_spawnEvent.Invoke();
        m_movementController.enabled = true;
        m_playerDetectionCol.enabled = true;
        m_playerCollider.enabled = true;
        m_isDead = false;
        m_sRend.color = Color.white;
    }
}
