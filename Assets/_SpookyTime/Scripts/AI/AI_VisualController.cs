using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_VisualController : MonoBehaviour
{
    private SpookyTimeManager m_spookyTimeManager;
    public string m_sppokyTimeBoolName = "IsSpookyTime";
    public string m_stunnedBool = "Stunned";
    private bool m_spookyTimeSwitch;
    public Animator m_aCont;
    public GameObject m_stunnedObject;
    private void Start()
    {
        m_spookyTimeManager = SpookyTimeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spookyTimeManager.IsSpookyTimeActive() != m_spookyTimeSwitch)
        {
            m_spookyTimeSwitch = m_spookyTimeManager.IsSpookyTimeActive();
            ChangeAnimState(m_spookyTimeSwitch);
        }
    }

    private void ChangeAnimState(bool p_state)
    {
        m_aCont.SetBool(m_sppokyTimeBoolName, p_state);
    }

    public void TurnOnStunnedState()
    {
        m_aCont.SetBool(m_stunnedBool, true);
        m_stunnedObject.SetActive(true);
    }
    public void TurnOffStunnedState()
    {
        m_aCont.SetBool(m_stunnedBool, false);
        m_stunnedObject.SetActive(false);
    }
}
