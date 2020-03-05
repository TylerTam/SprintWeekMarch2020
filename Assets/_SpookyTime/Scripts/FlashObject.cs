using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashMoi());
    }

    public GameObject m_flashMe;
    public float m_flashTime = 1, m_unFlashTime = .5f;
    private bool m_flashing = true;

    private IEnumerator FlashMoi()
    {
        float timer = 0;

        while (true)
        {
            while(timer < ((m_flashing)? m_flashTime : m_unFlashTime))
            {

                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            m_flashing = !m_flashing;
            m_flashMe.SetActive(m_flashing);
        }
    }
}
