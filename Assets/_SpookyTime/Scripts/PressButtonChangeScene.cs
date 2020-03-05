using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonPressedChange : UnityEngine.Events.UnityEvent { }
public class PressButtonChangeScene : MonoBehaviour
{
    public KeyCode m_player1Coin, m_player2Coin;

    public ButtonPressedChange m_coinInserted, m_menuStart;

    public float m_delay;
    private bool m_pressed = false;

    public CanvasGroup m_canvasGroup;
    public void Update()
    {
        if (!m_pressed)
        {
            if (Input.GetKeyDown(m_player1Coin) || Input.GetKeyDown(m_player2Coin))
            {
                m_pressed = true;
                m_coinInserted.Invoke();
                StartCoroutine(ButtonPressedCor());
            }
        }

    }
    public bool m_fade;
    private IEnumerator ButtonPressedCor()
    {
        float timer = 0;
        while (timer < m_delay)
        {
            timer += Time.deltaTime;
            if (m_fade)
            {
                m_canvasGroup.alpha = 1 - (timer / m_delay);
            }
            yield return null;
        }

        m_menuStart.Invoke();
    }
}
