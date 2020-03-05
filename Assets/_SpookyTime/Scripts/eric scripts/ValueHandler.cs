using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandler : MonoBehaviour
{
    public bool clampMaxAmount;
    public bool warnAtFirstEmpty;
    public float addPerSecond;
    bool warned = false;
    public ValueVisualizer vV;
    ShakeObject sO;
    
    public float value, valueMax, valueMin, valueWarn;

    private void Start()
    {
        if (GetComponent<ShakeObject>()) sO = GetComponent<ShakeObject>(); 
    }

    public void Change()
    {
        vV.Display();
        if (sO)
        {
            sO.StartShaking();

        }
    }

    public void ValueAdd(float f)
    {
        if (clampMaxAmount) {
            value = Mathf.Clamp(value + f, valueMin, valueMax);
        }
        else
        {
            value = value + f;
            
        }
        
        Change();
    }


    public void ValueSubtract(float f)
    {
        if (!(warnAtFirstEmpty && value - f <= valueMin && warned == false))
        {
            if (clampMaxAmount) { value = Mathf.Clamp(value - f, valueMin, valueMax); }
            else
            {
                value = Mathf.Clamp(value - f, valueMin, Mathf.Infinity);
            }
        }
        else
        {
            value = valueWarn;
            warned = true;
        }
        Change();
    }

    public void ValueSet(float f)
    {
        if (clampMaxAmount)
        {
            value = Mathf.Clamp(f, valueMin, valueMax);
        }
        else
        {
            value = Mathf.Clamp(f, valueMin, Mathf.Infinity);
        }

        Change();
    }


    public void ValueMultiply(float f)
    {
        if (clampMaxAmount)
        {
            value = Mathf.Clamp(value * f, valueMin, valueMax);
        }
        else
        {
            value = Mathf.Clamp(value * f, valueMin, Mathf.Infinity);
        }
        Change();
    }

    public void ValueDivide(float f)
    {//not sure how this interacts with negative valueMin
        value = valueMin + ((value - valueMin) / f);
        Change();
    }


    private void Update()
    {
        if(addPerSecond !=0)ValueAdd(addPerSecond * Time.deltaTime);
    }
}
