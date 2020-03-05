using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpookyTimeEvent : UnityEngine.Events.UnityEvent { }
public class SpookyTimeManager : MonoBehaviour
{
    public float m_timeTillSpookyTime = 30;
    public SpookyTimeEvent m_spookyTimeActivate;
    public SpookyTimeEvent m_spookyTimeDeactivate;


    public UnityEngine.UI.Image m_spookyTimeBar;

    public static SpookyTimeManager Instance;

    public float m_startSpookyTimer;
    private bool m_spookyTimeActive;

    public Color m_spookyImageColor, m_spookyImageColorST;



    [Header("Spooky countdown")]
    public UnityEngine.UI.Text m_countdownText;
    public Vector3 m_powScale, m_startScale;
    public AnimationCurve m_animCurve;
    public int m_startingCountdownTextTime;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SpookyCountdown());
        UpdateUI(0);
    }

    private void UpdateUI(float p_percent)
    {
        m_spookyTimeBar.fillAmount = p_percent;
    }

    private bool m_startedNextPow;
    /// <summary>
    /// The coroutine for when spooky time is charging up
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpookyCountdown()
    {
        float timer = 0;
        while (timer < m_timeTillSpookyTime)
        {
            if (timer > (float)m_startingCountdownTextTime )
            {
                if (!m_startedNextPow)
                {
                    m_countdownText.gameObject.SetActive(true);
                    m_startedNextPow = true;
                    m_countdownText.text = ((int)(m_timeTillSpookyTime - timer)+1).ToString();
                    StartCoroutine(PowCountdown());
                }
            }
            if(timer > m_startSpookyTimer)
            {
                m_spookyTimeBar.color = m_spookyImageColor;
                UpdateUI((timer - m_startSpookyTimer) / (m_timeTillSpookyTime - m_startSpookyTimer));
            }
            timer += Time.deltaTime;
            yield return null;
        }

        m_countdownText.gameObject.SetActive(false);
        m_spookyTimeBar.color = m_spookyImageColorST;
        m_spookyTimeActivate.Invoke();
        m_spookyTimeActive = true;
        StartCoroutine(SpookyTimeDuration());
        
    }

    private IEnumerator PowCountdown()
    {
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            m_countdownText.transform.localScale = Vector3.Lerp(m_startScale, m_powScale,m_animCurve.Evaluate(timer / 1f));
            yield return null;
        }
        m_startedNextPow = false;
    }

    public bool IsSpookyTimeActive()
    {
        return m_spookyTimeActive;
    }

    private IEnumerator SpookyTimeDuration()
    {
        while (m_spookyTimeActive)
        {
            yield return null;
        }
        m_spookyTimeDeactivate.Invoke();
        StartCoroutine(SpookyCountdown());
        m_spookyTimeActive = false;
    }


    public void ChangeSpookyTime(bool p_activeState)
    {
        if (!p_activeState)
        {
            UpdateUI(0);
        }
        m_spookyTimeActive = p_activeState;
    }
}
