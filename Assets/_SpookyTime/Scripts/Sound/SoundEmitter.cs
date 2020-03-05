using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{

    private AudioSource m_aSource;

    [Header("Default Sounds")]
    public List<Audioclips> m_allClips;
    public bool m_hasSpookyTimeVariations;
    public List<Audioclips> m_spookyTimeClips;
    private SpookyTimeManager m_spookyTime;
    public bool m_playSound = true;

    [System.Serializable]
    public struct Audioclips
    {
        public AudioClip m_clip;
        public float m_probablility;
    }
    private void Awake()
    {
        m_aSource = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        if (!m_playSound) return;

        m_aSource.Stop();

        float random = Random.Range(0f, 1f);
        float currentProbablilty = 0;
        AudioClip currentClip = null;
        if(m_spookyTime == null) { 
m_spookyTime = SpookyTimeManager.Instance;
        }
        foreach (Audioclips clip in (m_spookyTime.IsSpookyTimeActive() ? m_spookyTimeClips : m_allClips))
        {
            if(random < clip.m_probablility + currentProbablilty)
            {
                currentClip = clip.m_clip;
                break;
            }
            else
            {
                currentProbablilty += clip.m_probablility;
            }
        }

        m_aSource.clip = currentClip;
        m_aSource.Play();
    }
}
