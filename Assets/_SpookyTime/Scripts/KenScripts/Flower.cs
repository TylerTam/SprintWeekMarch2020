using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    bool isPlayerOnMe = false;
    /// <summary>
    /// //////////////////////////////////////////////////////////
    /// </summary>


    public float m_radius;
    public LayerMask m_detectionLayer;
    public Color m_gizmosColor1;
    public bool m_debugging;
    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    [SerializeField] Transform Player;
    /// <summary>
    /// //////////////////////////////////////////////////////////////
    /// </summary>
    [SerializeField] float FlowerOnPlayerYOffSet;


    
    void Update()
    {
        CheckRadius();

        if(isPlayerOnMe== true)
        {
           FlowerSpawner.isPlayerCarryingFlower = true;
        }



        if(FlowerSpawner.isPlayerCarryingFlower == true)
        {
            gameObject.transform.position = Player.position + new Vector3(0, FlowerOnPlayerYOffSet);
        }


        
    }


    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
    private void CheckRadius()
    {
        Collider2D playercol = Physics2D.OverlapCircle(transform.position, m_radius, m_detectionLayer);
        Player = playercol.transform.parent.transform;
        isPlayerOnMe = playercol != null;

        
        /////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    /////////////////////////////////////////////////
    ///

}
