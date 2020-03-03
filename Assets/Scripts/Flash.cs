using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public LayerMask ghost;
    public LayerMask wall;
    Transform flashCircle;
    public ValueHandler lightUI;
    public Toggle t;
    PlayerMovement pM;
    
    // Start is called before the first frame update
    void Start()
    {
        flashCircle = transform.GetChild(0);
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump")>0 && lightUI.value >=100)
        {
           

            //flashlight

            //send raycast in direction to find wall

            RaycastHit2D wallHits = Physics2D.Raycast(transform.position, Vector2.right*-1, 38, wall);

            float wallDistance;

            if (wallHits)
            {
                wallDistance = (wallHits.collider.transform.position - transform.position).magnitude;
            }
            else
            {
                wallDistance = 38;
            }
            

            //send raycast in direction to find ghost

        RaycastHit2D[] ghostHits = Physics2D.RaycastAll(transform.position, Vector2.right*-1, wallDistance, ghost);

            //render flashlight

            Debug.DrawLine(transform.position, transform.position + (Vector3.right * -1) * wallDistance, Color.yellow);

            //stun
            foreach (RaycastHit2D ghostHit in ghostHits)
            {

                ghostHit.collider.GetComponent<AI_Controller>().ChangeState(AI_Controller.AIStates.STUN);

            }
        }
    }
}