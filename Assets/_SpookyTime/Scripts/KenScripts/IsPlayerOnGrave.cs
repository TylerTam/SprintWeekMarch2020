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
    [SerializeField] float HauntScoreMultiplier;

    // 1 = 100%
    public bool isThisTreasureSpooky = false;
    public int SpookyTreasureRandomNumber;
    [SerializeField] int PercentChanceForSpookyTreasure;

    [SerializeField] Sprite GraveSprite;
    [SerializeField] Sprite GraveSpriteWhenPlayerIsOn;
    [SerializeField] Sprite BrokenGraveSprite;

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

    private void Start()
    {
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
        if (isPlayerOnMe == true) { GetComponent<SpriteRenderer>().sprite = GraveSprite; }
        if (isPlayerOnMe == false) { GetComponent<SpriteRenderer>().sprite = GraveSpriteWhenPlayerIsOn; }
        if(Awarded == true) { GetComponent<SpriteRenderer>().sprite = BrokenGraveSprite; }

        if (isPlayerOnMe == true)
        {
            if (GraveCurrentHealth != 0)
            {
                // change this to actual controller key
                if (Input.GetKeyDown(m_digButton))
                {
                    m_events.playerDiggedGrave.Invoke();
                }
            }
        }

        // if the grave is broken
        if(GraveCurrentHealth == 0 && Awarded == false)
        {
            m_events.GraveBroken.Invoke();
        }

        // health bar
        GraveHealthPercentage = ((float)GraveCurrentHealth) / ((float)GraveMaxHealth);
        GraveHealthFill.fillAmount = GraveHealthPercentage;
    }    

    public float m_radius;
    public LayerMask m_detectionLayer;
    public KeyCode m_digButton;
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
        GraveCurrentHealth -= 1;
    }

    public void GraveBroken()
    {
        Awarded = true;

        // check if this treasure is spooky or not
        if(isThisTreasureSpooky == true)
        {
            m_events.SpookyTreasure.Invoke();
        }

        /// add score
        if (SpookyTimeAcivated == false)
        {         
            TemporaryDataContainer.TemporaryScoreInt += GraveAward;
        }
        if(SpookyTimeAcivated == true) { TemporaryDataContainer.TemporaryScoreInt += Mathf.RoundToInt(GraveAward * HauntScoreMultiplier); }

        /// change sprites, (maybe destroy grave after a few seconds)
        StartCoroutine(WaitAndDestroyGrave());
    }

   public void SpookyTreasure()
    {
        SpookyTreasureText.SetActive(true);
        TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld += 1;
    }

    // remove this if want sprite to stay
    IEnumerator WaitAndDestroyGrave()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
