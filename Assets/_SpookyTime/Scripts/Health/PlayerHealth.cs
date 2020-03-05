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

    public List<PlayerRespawn> m_playerRespawns;

    
    private SpookyTimeManager m_spookyTimeManager;

    public KeyCode m_coinKeycode, m_coinKeycode2;
    public int m_numberOfCoins;
    private int m_currentnumberOfCoins;
    public float m_waitTime;
    private Coroutine m_coinCheckDelay;
    public UnityEngine.UI.Text m_countdownText;
    public UnityEngine.UI.Text m_coinText;

    [Header("Spooky Stun")]
    public UnityEngine.UI.Image m_jumpscareImage;
    public AnimationCurve m_jumpscareAnimCurve;
    public float m_jumpscareStunTime;

    public PlayerHealthEvent m_playersDeathEvent;
    public PlayerHealthEvent m_outOfLivesEvent;
    public PlayerHealthEvent m_coinInputEvent;
    public PlayerHealthEvent m_continuePlayEvent;
    public PlayerHealthEvent m_jumpscareEvent;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_spookyTimeManager = SpookyTimeManager.Instance;
    }

    private IEnumerator DeathDelay()
    {
        if (m_spookyTimeManager.IsSpookyTimeActive())
        {
            m_jumpscareEvent.Invoke();
            m_jumpscareImage.gameObject.SetActive(true);
            m_jumpscareImage.color = Color.white;
            float timer2 = 0;
            while (timer2 < m_jumpscareStunTime)
            {
                timer2 += Time.deltaTime;
                m_jumpscareImage.color = Color.Lerp(Color.white,new Color(1, 1, 1, 0), m_jumpscareAnimCurve.Evaluate(timer2/ m_jumpscareStunTime));
                yield return null;
            }
            m_jumpscareImage.gameObject.SetActive(false);
        }

        float timer = m_waitTime;
        while (timer > 0 )
        {
            m_countdownText.text = ((int)timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
            CheckCoinInput();
        }
        m_playersDeathEvent.Invoke();
    }



    private void CheckCoinInput()
    {
        m_coinText.text = (m_numberOfCoins - m_currentnumberOfCoins).ToString();
        if (Input.GetKeyDown(m_coinKeycode) || Input.GetKeyDown(m_coinKeycode2))
        {
            print("Coin Inpu");
            m_currentnumberOfCoins++;
            if (m_currentnumberOfCoins >= m_numberOfCoins)
            {
                m_currentnumberOfCoins = 0;
                ResetPlayers();
                m_continuePlayEvent.Invoke();
            }
            else
            {
                m_coinInputEvent.Invoke();
            }
        }
    }




    private void ResetPlayers()
    {
        foreach (PlayerRespawn player in m_playerRespawns)
        {
            player.ForceRespawn();
        }

        StopAllCoroutines();
    }
    public void TakeDamage(PlayerRespawn p_playerRespawn)
    {

        m_health -= (m_spookyTimeManager.IsSpookyTimeActive()) ? m_spookyTimeDamage : m_defaultDamage;
        GameObject.Find("Health").GetComponent<ValueHandler>().ValueSet(m_health);
        if (m_health <= 0)
        {
            
            foreach (PlayerRespawn player in m_playerRespawns)
            {
                player.OtherPlayerDied();
            }
            m_outOfLivesEvent.Invoke();
            m_coinCheckDelay = StartCoroutine(DeathDelay());
        }
        else
        {
            p_playerRespawn.StartRespawnFunction();
        }
    }
}