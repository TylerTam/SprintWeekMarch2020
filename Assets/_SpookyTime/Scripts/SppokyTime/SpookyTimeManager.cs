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

    public float m_spookyTimeDuration;

    public UnityEngine.UI.Image m_spookyTimeBar;

    public static SpookyTimeManager Instance;
    private bool m_spookyTimeActive;

    private void Start()
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
        StartCoroutine(SpookyTimeDuration());
        m_spookyTimeActive = true;
    }

    /// <summary>
    /// The coroutine for when spooky time is happening
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpookyTimeDuration()
    {
        float timer = 0;
        while (timer < m_spookyTimeDuration)
        {
            timer += Time.deltaTime;
            //UpdateUI(timer / m_spookyTimeDuration);
            yield return null;
        }
        m_spookyTimeDeactivate.Invoke();
        StartCoroutine(SpookyCountdown());
        m_spookyTimeActive = false;
    }


    public bool IsSpookyTimeActive()
    {
        return m_spookyTimeActive;
    }
}
