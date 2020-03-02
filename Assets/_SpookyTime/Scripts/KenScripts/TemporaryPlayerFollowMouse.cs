using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlayerFollowMouse : MonoBehaviour
{
    //remove this script
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
}
