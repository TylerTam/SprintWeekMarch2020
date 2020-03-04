using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource m_aSource;
    public List<Audioclips> m_allClips;
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
        m_aSource.Stop();

        float random = Random.Range(0f, 1f);
        float currentProbablilty = 0;
        AudioClip currentClip = null;
        foreach(Audioclips clip in m_allClips)
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
