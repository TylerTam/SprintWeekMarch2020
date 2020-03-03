using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawnedOnMe : MonoBehaviour
{
    [SerializeField] GameObject GraveSpawnPoints;
    Transform MyGrave;
    bool isSomethingOnMe;


    void Update()
    {

        MyGrave = gameObject.transform.Find("GravePrefab(Clone)");
        if (MyGrave.gameObject.name == "GravePrefab(Clone)")
        {
            isSomethingOnMe = true;
        }



    }
}
