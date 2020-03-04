using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCanvasMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField]Transform YLineToStopMoving;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < YLineToStopMoving.position.y) { gameObject.transform.position += new Vector3(0, moveSpeed); }

    }
}
