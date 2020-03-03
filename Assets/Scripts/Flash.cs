using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public LayerMask hitlayer;
    Transform flashCircle;
    public ValueHandler light;
    public Toggle t;
    
    // Start is called before the first frame update
    void Start()
    {
        flashCircle = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump")>0 && light.value >=100)
        {

            light.value = 0;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius,  hitlayer);
            foreach(Collider2D col in colliders)
            {


                SpriteRenderer csr = flashCircle.GetComponent<SpriteRenderer>();

                col.GetComponent<MoveTowardsTarget>().moveSpeed = 0;

                if (t.isOn)
                {
                    StartCoroutine(Delay(col.GetComponent<MoveTowardsTarget>()));
                }
                else
                {
                    StartCoroutine(Delay2(col.GetComponent<MoveTowardsTarget>()));
                }
                

                
            }
        }



    }

    IEnumerator Delay(MoveTowardsTarget mtt)
    {
        yield return new WaitForSeconds(1);


        mtt.moveSpeed = 2;
    }
    IEnumerator Delay2(MoveTowardsTarget mtt)
    {
        yield return new WaitForSeconds(1);


        mtt.moveSpeed = 5;
    }

}
