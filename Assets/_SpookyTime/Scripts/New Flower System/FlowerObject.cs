using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerObjectSounds : UnityEngine.Events.UnityEvent { }
public class FlowerObject : MonoBehaviour
{
    public LayerMask m_playerMask, m_safeZone;
    public float m_flowerRadius;
    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;
    private Transform m_currentPlayerTransform;
    public Transform m_flowerVisual;
    public Vector3 m_flowerOffset;

    public float m_swapTime;
    private bool m_canSwap;
    private SpookyTimeManager m_spookyManager;
    private PlayerRespawn m_playerRespawn;

    public FlowerSpawningManager m_flowerManager;

    public FlowerObjectSounds m_flowerPickedUp;
    public FlowerObjectSounds m_flowerSwapped;
    public FlowerObjectSounds m_flowerReturned;

    private void Start()
    {
        m_spookyManager = SpookyTimeManager.Instance;
        
    }
    public void ResetFlower()
    {

        m_flowerManager.FlowerDropped();
        m_flowerVisual.transform.localPosition = Vector3.zero;
        transform.parent = null;
        m_playerRespawn = null;
        m_currentPlayerTransform = null;
    }

    private void Update()
    {
        CheckFlowerRadius();
        if (m_playerRespawn != null)
        {
            if (m_playerRespawn.m_isDead)
            {
                ResetFlower();
            }
        }
    }

    public void ObjectStatus(bool p_pickedUp)
    {
        if (p_pickedUp)
        {
            m_flowerVisual.transform.localPosition = m_flowerOffset;
        }
        else {
            StopAllCoroutines();
            ResetFlower();
        }
    }

    private IEnumerator CanPickUp()
    {
        m_canSwap = false;
        yield return new WaitForSeconds(m_swapTime);
        m_canSwap = true;
    }

    
    private void CheckFlowerRadius()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, m_flowerRadius, m_playerMask);
        foreach(Collider2D col in cols)
        {
            if (m_currentPlayerTransform != null)
            {
                if (col.gameObject.transform.parent != m_currentPlayerTransform)
                {
                    if (m_canSwap)
                    {
                        m_currentPlayerTransform = col.gameObject.transform.parent;
                        
                        transform.parent = m_currentPlayerTransform;
                        transform.localPosition = Vector3.zero;
                        m_playerRespawn = m_currentPlayerTransform.GetComponent<PlayerRespawn>();
                        ObjectStatus(true);
                        m_flowerSwapped.Invoke();
                        StartCoroutine(CanPickUp());
                    }
                }
            }
            else
            {
                m_currentPlayerTransform = col.gameObject.transform.parent;
                
                transform.parent = m_currentPlayerTransform;
                transform.localPosition = Vector3.zero;
                m_playerRespawn = m_currentPlayerTransform.GetComponent<PlayerRespawn>();
                ObjectStatus(true);
                StartCoroutine(CanPickUp());
                m_flowerPickedUp.Invoke();
                m_flowerManager.FlowerPickedUp();
            }
        }


        if (Physics2D.OverlapCircle(transform.position, m_flowerRadius, m_safeZone) != null)
        {
            ResetFlower();
            m_flowerReturned.Invoke();
            m_spookyManager.ChangeSpookyTime(false);
            gameObject.SetActive(false);

        }

    }

 

    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_flowerRadius);
    }
}
