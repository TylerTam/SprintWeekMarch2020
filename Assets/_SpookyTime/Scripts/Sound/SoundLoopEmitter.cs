using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundLoopEmitter : MonoBehaviour
{
    public bool m_hasSpookyTimeClip;
    public AudioClip m_nonSpookyTimeClip;
    public AudioClip m_spookyTimeClip;
    private AudioSource m_aSource;
    private SpookyTimeManager m_spookyManager;

    private void Start()
    {
        m_aSource = GetComponent<AudioSource>();
        m_aSource.loop = true;
        m_spookyManager = SpookyTimeManager.Instance;
    }

    public void PlayClip()
    {
        m_aSource.Stop();

        if (m_hasSpookyTimeClip)
        {
            m_aSource.clip = ((m_spookyManager.IsSpookyTimeActive()) ? m_spookyTimeClip : m_nonSpookyTimeClip);
        }
        else
        {
            m_aSource.clip = m_nonSpookyTimeClip;
        }
        
    }

    public void StopClip()
    {
        m_aSource.Stop();
    }

}
