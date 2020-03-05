using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private bool m_enableInput = true;
    public bool m_respawning;
    [System.Serializable]
    public class PlayerInputAxis
    {
        public string m_movementXAxis, m_movementYAxis;
        public float m_deadzone;
    }
    public float m_lerpTime;

    public PlayerInputAxis m_playerInputs;
    public Entity_MovementController m_movementController;

    private void Update()
    {
        if (!m_enableInput) return;
        m_movementController.MoveCharacter(GetInput(), m_lerpTime);
        Debug.DrawLine(transform.position, transform.position + m_movementController.GetCurrentForward(), Color.red);
    }
    private Vector2 GetInput()
    {
        float xInput = Input.GetAxis(m_playerInputs.m_movementXAxis);
        float yInput = Input.GetAxis(m_playerInputs.m_movementYAxis);
        return new Vector2(Mathf.Abs(xInput) > m_playerInputs.m_deadzone ? xInput : 0, Mathf.Abs(yInput) > m_playerInputs.m_deadzone ? yInput : 0) ;
    }

    public void ChangeInputState(bool p_activeState)
    {
        if (m_respawning) return;
        m_enableInput = p_activeState;
    }
}
