using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> I am learning big time
[System.Serializable]
public class GraveEvents : UnityEngine.Events.UnityEvent { }
/// </summary>

public class IsPlayerOnGrave : MonoBehaviour
{
    public bool SpookyTimeAcivated = false;

    //modify how many times player needs to digg for grave to open
    [SerializeField]int GraveMaxHealth;
    public int GraveCurrentHealth;
    public float GraveHealthPercentage;

    bool isPlayerOnMe;
    [SerializeField] int GraveAward;


    // 1 = 100%
    public bool isThisTreasureSpooky = false;
    public int SpookyTreasureRandomNumber;
    [SerializeField] int PercentChanceForSpookyTreasure;


    //health bar visual
    [SerializeField] Image GraveHealthFill;
    // spooky tresure text visual
    [SerializeField] GameObject SpookyTreasureText;

    //check if the award is given
    bool Awarded = false;

    /// <summary> more learning
    public GraveEventsStrcut m_events;
    [System.Serializable]
    public struct GraveEventsStrcut
    {
        public GraveEvents playerDiggedGrave;
        public GraveEvents GraveBroken;
        public GraveEvents SpookyTreasure;
    }
    /// </summary>

    private ScoreManager m_scoreManager;
    private AI_Manager m_aiManager;
    private void Start()
    {
        m_aiManager = AI_Manager.Instance;
        m_scoreManager = ScoreManager.Instance;
        // array for awards
        var myCodes = new int[5];
        myCodes[0] = 150;
        myCodes[1] = 200;
        myCodes[2] = 250;
        myCodes[3] = 300;
        myCodes[4] = 350;
        var index = Random.Range(0, myCodes.Length);
        GraveAward = myCodes[index];

        // is this a spooky treasure?
        SpookyTreasureRandomNumber = Random.Range(0, 100);
        if(SpookyTreasureRandomNumber <= PercentChanceForSpookyTreasure) { isThisTreasureSpooky = true; } else { isThisTreasureSpooky = false; }


        // current health = max health at start
        GraveCurrentHealth = GraveMaxHealth;
        //disable spooky treasure text on start
        SpookyTreasureText.SetActive(false);
    }

    private void Update()
    {
        CheckRadius();
        


        // if the grave is broken
        if(GraveCurrentHealth == 0 && Awarded == false)
        {
            m_events.GraveBroken.Invoke();
        }

        // health bar
        GraveHealthPercentage = ((float)GraveCurrentHealth) / ((float)GraveMaxHealth);
        GraveHealthFill.fillAmount = GraveHealthPercentage;
    }    

    [Header("Detection")]
    public float m_radius;
    public LayerMask m_detectionLayer;
    public int m_gravePoints;
    [Header("Debugging")]
    public bool m_debugging;
    public Color m_gizmosColor1;
    private void CheckRadius()
    {
        isPlayerOnMe = Physics2D.OverlapCircle(transform.position, m_radius, m_detectionLayer) != null;
    }
    private void OnDrawGizmos()
    {
        if (!m_debugging) return;
        Gizmos.color = m_gizmosColor1;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    public void playerDiggedGrave()
    {
        m_events.playerDiggedGrave.Invoke();
        GraveCurrentHealth -= 1;
    }

    public void GraveBroken()
    {



        // check if this treasure is spooky or not
        if(isThisTreasureSpooky == true)
        {
            m_events.SpookyTreasure.Invoke();
        }

        /// add score
        if (SpookyTimeAcivated == false)
        {         
            m_scoreManager.ChangeScore(m_gravePoints);
        }

        m_aiManager.SpawnAI(transform.position);
        /// change sprites, (maybe destroy grave after a few seconds)
        Destroy(gameObject);
    }

   public void SpookyTreasure()
    {
        SpookyTreasureText.SetActive(true);
        TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld += 1;
    }



}
