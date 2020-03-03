using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_MovementController : MonoBehaviour
{



    #region Components
    public LayerMask m_terrainLayer;
    public Transform m_myCollider;
    #endregion

    #region Movement Properties
    public enum AxisPriority { Vetical, Horizontal }
    public AxisPriority m_priorityAxis;
    public float m_lerpTime;
    private Coroutine m_movementCoroutine;
    private bool m_moving;
    #endregion

    public float m_gridSize = .5f;


    public virtual void Start()
    {

        bool fixX = false, fixY = false;
        if (transform.position.x % m_gridSize != 0)
        {
            fixX = true;
        }
        if (transform.position.y % m_gridSize != 0)
        {
            fixY = true;
        }

        transform.position = new Vector3((fixX) ? Mathf.Round(transform.position.x) + m_gridSize : transform.position.x,
                                        (fixY) ? Mathf.Round(transform.position.y) + m_gridSize : transform.position.y,
                                        0f);
    }





  
    /// <summary>
    /// Checks if there are any obstacles in the way
    /// </summary>
    public void MoveCharacter(Vector2 p_movement)
    {
        if (m_moving) return;
        Vector2 priorityMovement = GetPriority(p_movement);
        


        Collider2D cols = Physics2D.OverlapCircle(transform.position + (Vector3)priorityMovement.normalized, .25f, m_terrainLayer);
        bool hit = false;
        if (cols != null)
        {
            if (cols.transform.parent.gameObject != this.gameObject)
            {
                hit = true;
            }
        }
        if (!hit)
        {

            StartMovement(priorityMovement);
        }
    }

    private Vector2 GetPriority(Vector2 p_givenAxis)
    {
        if (p_givenAxis.y != 0)
        {
            return new Vector2(0, p_givenAxis.y);
        }
        else
        {
            return new Vector2(p_givenAxis.x, 0);
        }
    }

    private void StartMovement(Vector2 p_movement)
    {
        if (p_movement.magnitude != 0)
        {


            if (m_movementCoroutine == null)
            {
                m_movementCoroutine = StartCoroutine(MoveMe(p_movement, false));

            }
        }
    }

    /// <summary>
    /// Moves the character root through a lerp
    /// </summary>
    private IEnumerator MoveMe(Vector2 p_currentDir, bool p_running)
    {
        m_moving = true;
        float m_currentMovementTimer = 0;
        float lerpTime =  m_lerpTime;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + (new Vector3((p_currentDir.x == 0) ? 0 : Mathf.Sign(p_currentDir.x), (p_currentDir.y == 0) ? 0 : Mathf.Sign(p_currentDir.y),transform.position.z));
        

        while (m_currentMovementTimer / lerpTime < 1)
        {
            m_currentMovementTimer += Time.deltaTime;
            m_myCollider.transform.position = endPos;
            float percent = m_currentMovementTimer / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, percent);
            Debug.DrawLine(startPos, endPos, Color.magenta);
            yield return null;
        }
        transform.position = endPos;
        m_myCollider.transform.position = transform.position;
        m_movementCoroutine = null;
        m_moving = false;
    }

    public bool IsMoving()
    {
        return m_moving;
    }

}
