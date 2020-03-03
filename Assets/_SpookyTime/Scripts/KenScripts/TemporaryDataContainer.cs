using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryDataContainer : MonoBehaviour
{
    public static int TemporaryScoreInt;
    public static int TemporaryCurrentSpookyTreasureHeld;
    [SerializeField] int NumberOfSpookyTreasureToReachSpookyTime;
    public static int TemporaryNumberOfSpookyTreasureToReachSpookyTime;
    [SerializeField] float SpookyTimeDuration;
    public static float TemporarySpookyTimeDuration;
    public static bool TemporarySpookyTimeActivated;

    // get this out
    public static bool TemporaryIsPlayerAlive;

    private void Start()
    {
        TemporaryNumberOfSpookyTreasureToReachSpookyTime = NumberOfSpookyTreasureToReachSpookyTime;
        TemporarySpookyTimeDuration = SpookyTimeDuration;

        // get this out of here later
        TemporaryIsPlayerAlive = true;
    }

    private void Update()
    {
        if(TemporarySpookyTimeActivated == true)
        {
            StartCoroutine(WaitAndStopSpookyTime());
        }
    }

    // if spooky yime = true
    IEnumerator WaitAndStopSpookyTime()
    {
        yield return new WaitForSeconds(SpookyTimeDuration);
        TemporarySpookyTimeActivated = false;
    }
}
