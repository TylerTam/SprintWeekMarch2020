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
    private bool m_spookyTimeActive;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SpookyCountdown());
    }

    private void UpdateUI(float p_percent)
    {
        m_spookyTimeBar.fillAmount = p_percent;
    }


    /// <summary>
    /// The coroutine for when spooky time is charging up
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpookyCountdown()
    {
        float timer = 0;
        while (timer < m_timeTillSpookyTime)
        {
            timer += Time.deltaTime;
            UpdateUI(timer / m_timeTillSpookyTime);
            yield return null;
        }

        m_spookyTimeActivate.Invoke();
        m_spookyTimeActive = true;
        StartCoroutine(SpookyTimeDuration());
        
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
        m_spookyTimeActive = p_activeState;
    }
}
