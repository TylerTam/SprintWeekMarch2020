using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyTimeEnable : MonoBehaviour
{
    public GameObject m_spookyObject;
    public GameObject m_nonSpookyObject;

    private SpookyTimeManager m_spookyManager;
    private void Start()
    {
        m_spookyManager = SpookyTimeManager.Instance;
    }

    private void Update()
    {
        if (m_spookyManager.IsSpookyTimeActive())
        {
            m_spookyObject.SetActive(true);
            m_nonSpookyObject.SetActive(false);
        }
        else
        {
            m_spookyObject.SetActive(false);
            m_nonSpookyObject.SetActive(true);
        }
    }
}
