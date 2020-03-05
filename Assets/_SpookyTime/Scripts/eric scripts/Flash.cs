using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public LayerMask ghost;
    public LayerMask wall;
    public ValueHandler lightUI;
    public Toggle t;
    Entity_MovementController eMC;
    LineRenderer lr;
    [SerializeField] float flashTime;
    [SerializeField] int stunAward = 50;
    [SerializeField] int spooktTimeStunAward = 50;
    private SpookyTimeManager m_spookyTimeManager;

    public float m_flashDistance;
    public KeyCode m_flashKeycode;

    // Start is called before the first frame update
    void Start()
    {
        eMC = GetComponent<Entity_MovementController>();
        lr = GetComponent<LineRenderer>();
        m_spookyTimeManager = SpookyTimeManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_flashKeycode) && lightUI.value >=100)
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

            ScoreManager sM = ScoreManager.Instance;

            //stun
            foreach (RaycastHit2D ghostHit in ghostHits)
            {
                ghostHit.collider.transform.parent.GetComponent<AI_Controller>().ChangeState(AI_Controller.AIStates.STUN);

                sM.ChangeScore( (m_spookyTimeManager.IsSpookyTimeActive()) ?  spooktTimeStunAward:stunAward);

            }
        }
    }


    IEnumerator DerenderLine(float t)
    {

        yield return new WaitForSeconds(t);
        lr.positionCount = 0;
    }
}