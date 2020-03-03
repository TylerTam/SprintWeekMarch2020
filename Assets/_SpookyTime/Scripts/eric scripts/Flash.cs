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
    Entity_MovementController eMC;
    LineRenderer lr;
    [SerializeField] float flashTime;

    public float m_flashDistance;

    // Start is called before the first frame update
    void Start()
    {
        flashCircle = transform.GetChild(0);
        eMC = GetComponent<Entity_MovementController>();
        lr = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump")>0 && lightUI.value >=100)
        {

            lightUI.value = 0;

            //flashlight

            //send raycast in direction to find wall

            RaycastHit2D wallHits = Physics2D.Raycast(transform.position, eMC.GetCurrentForward() , m_flashDistance, wall);

            Vector3 hitPoint = new Vector3();

            if (wallHits)
            {
                hitPoint = wallHits.point;
            }
            else
            {
                hitPoint = transform.position + eMC.GetCurrentForward() * m_flashDistance;
            }
            
            //send raycast in direction to find ghost

        RaycastHit2D[] ghostHits = Physics2D.RaycastAll(transform.position, eMC.GetCurrentForward(), (Vector3.Distance((Vector2)hitPoint, (Vector2)transform.position)), ghost);

            //render flashlight
            
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position + Vector3.forward*-1);
            lr.SetPosition(1, hitPoint + Vector3.forward*-1);

            StartCoroutine(DerenderLine(flashTime));


            //stun
            foreach (RaycastHit2D ghostHit in ghostHits)
            {
                ghostHit.collider.GetComponent<AI_Controller>().ChangeState(AI_Controller.AIStates.STUN);
                
            }
        }
    }


    IEnumerator DerenderLine(float t)
    {

        yield return new WaitForSeconds(t);
        print("derender");
        lr.positionCount = 0;
    }
}