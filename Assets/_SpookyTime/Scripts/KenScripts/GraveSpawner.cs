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

    [SerializeField] Transform GraveSpawn1;
    [SerializeField] Transform GraveSpawn2;
    [SerializeField] Transform GraveSpawn3;
    [SerializeField] Transform GraveSpawn4;
    [SerializeField] Transform GraveSpawn5;
    [SerializeField] Transform GraveSpawn6;
    [SerializeField] Transform GraveSpawn7;
    [SerializeField] Transform GraveSpawn8;
    [SerializeField] Transform GraveSpawn9;
    [SerializeField] Transform GraveSpawn10;
    [SerializeField] Transform GraveSpawn11;
    [SerializeField] Transform GraveSpawn12;
    [SerializeField] Transform GraveSpawn13;

    public bool Grave1IsUp;
    public bool Grave2IsUp;
    public bool Grave3IsUp;
    public bool Grave4IsUp;
    public bool Grave5IsUp;
    public bool Grave6IsUp;
    public bool Grave7IsUp;
    public bool Grave8IsUp;
    public bool Grave9IsUp;
    public bool Grave10IsUp;
    public bool Grave11IsUp;
    public bool Grave12IsUp;
    public bool Grave13IsUp;

    public Transform m_gravesParent;

    void Start()
    {
        Grave1IsUp = false;
         Grave2IsUp = false;
         Grave3IsUp = false;
         Grave4IsUp = false;
         Grave5IsUp = false;
         Grave6IsUp = false;
         Grave7IsUp =false;
         Grave8IsUp = false;
         Grave9IsUp = false;
         Grave10IsUp = false;
         Grave11IsUp = false;
         Grave12IsUp = false;
         Grave13IsUp = false;
    }

    void Update()
    {
        GraveCount = GameObject.FindGameObjectsWithTag("Grave");
        CurrentNumberOfGravesOnMap = GraveCount.Length;


        if (CurrentNumberOfGravesOnMap < MaxNumberOfGravesOnMap)
        {
            SpawnNewGrave();
        }


    }

    private void LateUpdate()
    {
        if (GraveSpawn1.childCount == 0) { Grave1IsUp = false; }
        if (GraveSpawn2.childCount == 0) { Grave2IsUp = false; }
        if (GraveSpawn3.childCount == 0) { Grave3IsUp = false; }
        if (GraveSpawn4.childCount == 0) { Grave4IsUp = false; }
        if (GraveSpawn5.childCount == 0) { Grave5IsUp = false; }
        if (GraveSpawn6.childCount == 0) { Grave6IsUp = false; }
        if (GraveSpawn7.childCount == 0) { Grave7IsUp = false; }
        if (GraveSpawn8.childCount == 0) { Grave8IsUp = false; }
        if (GraveSpawn9.childCount == 0) { Grave9IsUp = false; }
        if (GraveSpawn10.childCount == 0) { Grave10IsUp = false; }
        if (GraveSpawn11.childCount == 0) { Grave11IsUp = false; }
        if (GraveSpawn12.childCount == 0) { Grave12IsUp = false; }
        if (GraveSpawn13.childCount == 0) { Grave13IsUp = false; }
    }

    void SpawnNewGrave()
    {
        RandomNumber = Random.Range(1, 13);
        if (RandomNumber == 1 & Grave1IsUp == false) { Instantiate(GravePrefab, GraveSpawn1); Grave1IsUp = true; }
        if (RandomNumber == 2 & Grave2IsUp == false) { Instantiate(GravePrefab, GraveSpawn2); Grave2IsUp = true; }
        if (RandomNumber == 3 & Grave3IsUp == false) { Instantiate(GravePrefab, GraveSpawn3); Grave3IsUp = true; }
        if (RandomNumber == 4 & Grave4IsUp == false) { Instantiate(GravePrefab, GraveSpawn4); Grave4IsUp = true; }
        if (RandomNumber == 5 & Grave5IsUp == false) { Instantiate(GravePrefab, GraveSpawn5); Grave5IsUp = true; }
        if (RandomNumber == 6 & Grave6IsUp == false) { Instantiate(GravePrefab, GraveSpawn6); Grave6IsUp = true; }
        if (RandomNumber == 7 & Grave7IsUp == false) { Instantiate(GravePrefab, GraveSpawn7); Grave7IsUp = true; }
        if (RandomNumber == 8 & Grave8IsUp == false) { Instantiate(GravePrefab, GraveSpawn8); Grave8IsUp = true; }
        if (RandomNumber == 9 & Grave9IsUp == false) { Instantiate(GravePrefab, GraveSpawn9); Grave9IsUp = true; }
        if (RandomNumber == 10 & Grave10IsUp == false) { Instantiate(GravePrefab, GraveSpawn10); Grave10IsUp = true; }
        if (RandomNumber == 11 & Grave11IsUp == false) { Instantiate(GravePrefab, GraveSpawn11); Grave11IsUp = true; }
        if (RandomNumber == 12 & Grave12IsUp == false) { Instantiate(GravePrefab, GraveSpawn12); Grave12IsUp = true; }
        if (RandomNumber == 13 & Grave13IsUp == false) { Instantiate(GravePrefab, GraveSpawn13); Grave13IsUp = true; }
    }


}
