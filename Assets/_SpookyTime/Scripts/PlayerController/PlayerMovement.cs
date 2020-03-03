using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput m_playerInput;


    private void Start()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }


}
