using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCanvasController : MonoBehaviour
{
    [SerializeField] GameObject ScoreBoard;
    void Start()
    {
        ScoreBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(TemporaryDataContainer.TemporaryIsPlayerAlive == false)
        {
            ScoreBoard.SetActive(true);
        }
        if (TemporaryDataContainer.TemporaryIsPlayerAlive == true)
        {
            ScoreBoard.SetActive(false);
        }
    }
}
