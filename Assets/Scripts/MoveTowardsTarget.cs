using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{

    [SerializeField] Transform target;
    public float moveSpeed;
    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDir = target.position - transform.position;
        transform.position += (Vector3)moveDir.normalized * moveSpeed * Time.deltaTime;
    }
}
