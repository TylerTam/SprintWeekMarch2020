using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpookyTimeUI : MonoBehaviour
{
    [SerializeField] Image SpookyMeterImage;
    [SerializeField] Text SpookyMeterText;
    [SerializeField] GameObject SpookyTimeCountDownText;
    float SpookyMeterPercentage;

    // spooky time count down
    public float SpookyTimeDuration;
    bool SpookyTimeTrigger = false;
    /*

    void Update()
    {
        // disable countdown text 
        if(TemporaryDataContainer.TemporarySpookyTimeActivated == false)
        {
            SpookyTimeCountDownText.SetActive(false);
            SpookyTimeDuration = TemporaryDataContainer.TemporarySpookyTimeDuration;
        }



        //UI stuff
        SpookyMeterPercentage = ((float)TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld) / ((float)TemporaryDataContainer.TemporaryNumberOfSpookyTreasureToReachSpookyTime);
        SpookyMeterImage.fillAmount = SpookyMeterPercentage;
        SpookyMeterText.text = TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld + " / " +  TemporaryDataContainer.TemporaryNumberOfSpookyTreasureToReachSpookyTime;

        //Spooky time active
        if(TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld >= TemporaryDataContainer.TemporaryNumberOfSpookyTreasureToReachSpookyTime)
        {
            SpookyTimeTrigger = true;

        }
        if(SpookyTimeTrigger == true)
        {
            SpookyTimeStarted();
        }
    }

    void SpookyTimeStarted()
    {
        TemporaryDataContainer.TemporarySpookyTimeActivated = true;
        TemporaryDataContainer.TemporaryCurrentSpookyTreasureHeld = 0;

        // start the count down
        SpookyTimeDuration -= Time.deltaTime;
        // set count down text
        SpookyTimeCountDownText.SetActive(true);
        SpookyTimeCountDownText.GetComponent<Text>().text = "Time before Spooky Time Ends:" + SpookyTimeDuration;
        if(SpookyTimeDuration <= 0) { SpookyTimeTrigger = false; }
    }*/
}
