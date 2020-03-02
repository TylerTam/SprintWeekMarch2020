using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public class PlayerInputAxis
    {
        public string m_movementXAxis, m_movementYAxis;

    }

    public PlayerInputAxis m_playerInputs;

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis(m_playerInputs.m_movementXAxis), Input.GetAxis(m_playerInputs.m_movementYAxis));
    }
}
