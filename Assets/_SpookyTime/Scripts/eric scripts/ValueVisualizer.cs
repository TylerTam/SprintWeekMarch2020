using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueVisualizer : MonoBehaviour
{
    #region Variable Declaration


    [SerializeField] bool testMode;

    [SerializeField] ValueHandler vH;
    [SerializeField] Image positiveImage, negativeImage;


    Vector2 psSize;
    Vector2 nsSize;
    [SerializeField] bool useColors = false;
    [SerializeField] Gradient colors;

    
    [SerializeField] Text text;

    public enum TextType
    {
        value,
        valueOverMax,
        word
    }

    public TextType textType;

    public enum DisplayType
    {
        simple,
        tiledHorizontal,
        tiledVertical,
        //repeated,
        size,
        fill,
        //animation,
        //shader
    }

    public DisplayType displayType;

    [Header("Tiled")]
    [SerializeField] bool fillCentre;

    [Header("Repeat")]
    [SerializeField] Vector2 repeatGap;

    [Header("Size")]
    [SerializeField] Vector2 sizeMaxScale;
    [SerializeField] Vector2 sizeMinScale;

    [Header("Fill")]
    [SerializeField] public Image.FillMethod fillMethod;
    [SerializeField] int fillOrigin;
    [SerializeField] bool fillClockwise;
    
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
        Display();

        negativeImage.rectTransform.pivot = new Vector2(0, 0);
        positiveImage.rectTransform.pivot = new Vector2(0, 0);

    }

    private void Update()
    {
        if(testMode)Display();


    }
    private void OnDrawGizmos()
    {
        if(testMode)
        Invoke("Display", 0.1f);
    }

    [ContextMenu("Update Display")]
    public void Display()
    {
        
        //Gather Sprite Stats
        psSize = positiveImage.sprite.bounds.size;
        if (negativeImage)
        {
            nsSize = negativeImage.sprite.bounds.size;//size is found in units, not pixels
        }


        if (displayType == DisplayType.simple)
        {
            positiveImage.type = Image.Type.Simple;
            positiveImage.rectTransform.sizeDelta = new Vector2(100 * nsSize.x, 100 * nsSize.y);
            negativeImage.type = Image.Type.Simple;
            negativeImage.rectTransform.sizeDelta = new Vector2(100 * nsSize.x, 100 * nsSize.y);
        }

        if (displayType == DisplayType.tiledHorizontal)
        {
            //change image type
            positiveImage.type = Image.Type.Tiled;
            //change rect width/height according to values
            positiveImage.rectTransform.sizeDelta = new Vector2(vH.value * 100 * psSize.x, 100 * psSize.y);
            positiveImage.fillCenter = fillCentre;

            if (negativeImage)
            {
                negativeImage.type = Image.Type.Tiled;
                negativeImage.rectTransform.sizeDelta = new Vector2(vH.valueMax * 100 * nsSize.x, 100 * nsSize.y);
            }
        }

        if (displayType == DisplayType.tiledVertical)
        {
            positiveImage.type = Image.Type.Tiled;
            positiveImage.rectTransform.sizeDelta = new Vector2(100 * psSize.x, vH.value * 100 * psSize.y);

            positiveImage.fillCenter = fillCentre;

            negativeImage.type = Image.Type.Tiled;
            negativeImage.rectTransform.sizeDelta = new Vector2(100 * nsSize.x, vH.valueMax * 100 * nsSize.y);
        }

        if(displayType == DisplayType.size)
        {
            positiveImage.type = Image.Type.Simple;
            positiveImage.rectTransform.sizeDelta = Vector2.Lerp(sizeMinScale, sizeMaxScale, vH.value / vH.valueMax) * 100;
            negativeImage.rectTransform.sizeDelta = sizeMaxScale * 100f;
        }

        if(displayType == DisplayType.fill)
        {
            positiveImage.rectTransform.sizeDelta = new Vector2(100 * psSize.x, 100 * psSize.y);
            negativeImage.rectTransform.sizeDelta = new Vector2(100 * nsSize.x, 100 * nsSize.y);

            //change image type
            positiveImage.type = Image.Type.Filled;
            //change rect width/height according to values
            positiveImage.fillMethod = fillMethod;
            positiveImage.fillOrigin = fillOrigin;
            positiveImage.fillClockwise = fillClockwise;
            positiveImage.fillAmount = vH.value / vH.valueMax;

            if (negativeImage)
            {
                negativeImage.type = Image.Type.Filled;
                negativeImage.fillAmount = 1;
            }
        }


        if (text)
        {
            if (textType == TextType.value)
            {
                text.text = vH.value.ToString(/*"F1"*/);
            }
            if (textType == TextType.valueOverMax)
            {
                text.text = vH.value.ToString(/*"F1"*/) + " / " + vH.valueMax;
            }
            if (textType == TextType.word)
            {
                text.text = gameObject.name;
            }
            
        }

        if (useColors)
        {
            positiveImage.color = colors.Evaluate(vH.value / vH.valueMax);
        }

    }

}
