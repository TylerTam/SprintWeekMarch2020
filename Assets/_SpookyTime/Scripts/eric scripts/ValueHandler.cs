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
    
    public float value, valueMax, valueMin, valueWarn;



    public void Change()
    {
        vV.Display();
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
        ValueAdd(addPerSecond * Time.deltaTime);
    }
}
