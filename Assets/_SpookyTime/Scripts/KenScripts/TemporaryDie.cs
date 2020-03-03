using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryDie : MonoBehaviour
{
    public void TemporaryDeathOnClick()
    {
        TemporaryDataContainer.TemporaryIsPlayerAlive = false;
    }
    public void TemporaryAliveOnClick()
    {
        TemporaryDataContainer.TemporaryIsPlayerAlive = true;
    }
}
