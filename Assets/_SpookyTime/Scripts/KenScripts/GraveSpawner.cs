using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawner : MonoBehaviour
{
    [SerializeField] Transform GravePrefab;
    [SerializeField] int MaxNumberOfGravesOnMap;
    public int CurrentNumberOfGravesOnMap;
    int RandomNumber;
    private GameObject[] GraveCount;

    public List<Transform> m_spawnPoints;
    public LayerMask m_graveLayer;

    public Transform m_gravesParent;


    void Start()
    {
        StartCoroutine(LoadNewGraves());
    }

    void Update()
    {
        GraveCount = GameObject.FindGameObjectsWithTag("Grave");
        CurrentNumberOfGravesOnMap = GraveCount.Length;
    }

    private IEnumerator LoadNewGraves()
    {
        while(CurrentNumberOfGravesOnMap < MaxNumberOfGravesOnMap)
        {
            SpawnNewGrave();
            yield return null;
        }
    }


    public void SpawnNewGrave()
    {
        List<Transform> possibleSpawns = new List<Transform>();
        foreach(Transform newPoint in m_spawnPoints)
        {
            if(!Physics2D.Raycast(newPoint.position, Vector3.forward, 100, m_graveLayer))
            {
                possibleSpawns.Add(newPoint);
            }
        }

        int randomSpawn = Random.Range(0, possibleSpawns.Count);
        GameObject newPoints = ObjectPooler.instance.NewObject(GravePrefab.gameObject, possibleSpawns[randomSpawn].position, Quaternion.identity);

        newPoints.GetComponent<IsPlayerOnGrave>().m_spawner = this;


    }


}
