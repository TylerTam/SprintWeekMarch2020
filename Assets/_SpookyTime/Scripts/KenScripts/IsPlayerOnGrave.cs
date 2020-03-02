using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsPlayerOnGrave : MonoBehaviour
{
    //temporary
    public bool Haunt = false;

    //modify how many times player needs to digg for grave to open
    [SerializeField]int GraveMaxHealth;
    public int GraveCurrentHealth;
    public float GraveHealthPercentage;

    bool isPlayerOnMe;
    [SerializeField] int GraveAward;
    [SerializeField] float HauntScoreMultiplier;

    [SerializeField] Sprite GraveSprite;
    [SerializeField] Sprite GraveSpriteWhenPlayerIsOn;
    [SerializeField] Sprite BrokenGraveSprite;

    //health bar visual
    [SerializeField] Image GraveHealthFill;

    //check if the award is given
    bool Awarded = false;

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

        // current health = max health at start
        GraveCurrentHealth = GraveMaxHealth;
    }

    private void Update()
    {


        if (isPlayerOnMe == true) { GetComponent<SpriteRenderer>().sprite = GraveSprite; }
        if (isPlayerOnMe == false) { GetComponent<SpriteRenderer>().sprite = GraveSpriteWhenPlayerIsOn; }
        if(Awarded == true) { GetComponent<SpriteRenderer>().sprite = BrokenGraveSprite; }

        if (isPlayerOnMe == true)
        {
            if (GraveCurrentHealth != 0)
            {
                // change this to actual controller key
                if (Input.GetKeyDown("space"))
                {
                    playerDiggedGrave();
                }
            }
        }

        // if the grave is broken
        if(GraveCurrentHealth == 0 && Awarded == false)
        {
            GraveBroken();
        }

        // health bar
        GraveHealthPercentage = ((float)GraveCurrentHealth) / ((float)GraveMaxHealth);
        GraveHealthFill.fillAmount = GraveHealthPercentage;
    }    

    void OnTriggerEnter2D(Collider2D other)
    {
        // change this to find the real player
        if (other.gameObject.name == "TemporaryPlayer")
        {
            isPlayerOnMe = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // change this to find the real player
        if (other.gameObject.name == "TemporaryPlayer")
        {
            isPlayerOnMe = false;
        }
    }

    void playerDiggedGrave()
    {
        GraveCurrentHealth -= 1;
    }

    void GraveBroken()
    {
        Awarded = true;

        /// add score
        if (Haunt == false)
        {         
            TemporaryDataContainer.TemporaryScoreInt += GraveAward;
        }
        if(Haunt == true) { TemporaryDataContainer.TemporaryScoreInt += Mathf.RoundToInt(GraveAward * HauntScoreMultiplier); }


        /// change sprites, (maybe destroy grave after a few seconds)
        StartCoroutine(WaitAndDestroyGrave());
    }

    // remove this if want sprite to stay
    IEnumerator WaitAndDestroyGrave()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
