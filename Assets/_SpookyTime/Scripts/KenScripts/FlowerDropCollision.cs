using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDropCollision : MonoBehaviour
{
    bool isPlayerOnMe = false;
    public float m_radius;
    public LayerMask m_detectionLayer;
    public Color m_gizmosColor1;
    public bool m_debugging;
    GameObject[] FlowerPrefabsInScene;


    private SpookyTimeManager m_spookyTime;

    void Start()
    {
        m_spookyTime = SpookyTimeManager.Instance;

    }


    void Update()
    {
        CheckRadius();
        if(isPlayerOnMe == true && FlowerSpawner.isPlayerCarryingFlower == true)
        {
            FlowerDropped();
        }
    }


    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(gameObject.transform.position, m_radius);
    }
    private void CheckRadius()
    {
        isPlayerOnMe = Physics2D.OverlapCircle(gameObject.transform.position, m_radius, m_detectionLayer) != null;
    }

    void FlowerDropped()
    {
        FlowerSpawner.isPlayerCarryingFlower = false;
        FlowerSpawner.isFlowersInScene = false;

        // find all flowers in scene
        FlowerPrefabsInScene = GameObject.FindGameObjectsWithTag("Flower");
        // destroy them
        for (int i = 0; i < FlowerPrefabsInScene.Length; i++) {
            Destroy(FlowerPrefabsInScene[i]);
                }


        // get reference to spooky timer
        Debug.Log("change Spooky Time");

    }

}
