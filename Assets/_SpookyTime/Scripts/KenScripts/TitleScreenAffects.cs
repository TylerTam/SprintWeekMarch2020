using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenAffects : MonoBehaviour
{
    [SerializeField] GameObject Image1;
    [SerializeField] Transform Image1Goal;
    [SerializeField] GameObject Image2;
    [SerializeField] Transform Image2Goal;
    [SerializeField] GameObject Image3;
    [SerializeField] Transform Image3Goal;
    [SerializeField] GameObject CloudImage;
    [SerializeField] GameObject TitleText;
    [SerializeField] GameObject PressToStartText;
    [SerializeField] GameObject GroupNameText;
    [SerializeField] GameObject WhiteScreenImage;
    [SerializeField] float Speed;
    [SerializeField] float SpeedModifier1;
    [SerializeField] float SpeedModifier2;

    bool Flashed;
    bool TextFlash;
    int TextFlashState;

    void Start()
    {
        WhiteScreenImage.SetActive(false);
        TitleText.SetActive(false);
        PressToStartText.SetActive(false);
        GroupNameText.SetActive(false);

        Flashed = false;
        TextFlash = false;
        TextFlashState = 0;
    }

    void Update()
    {
        if (Image1.transform.position.y < Image1Goal.position.y) { Image1.transform.position += new Vector3(0, Speed); }
        if (Image2.transform.position.y < Image2Goal.position.y) { Image2.transform.position += new Vector3(0, Speed*SpeedModifier1); }
        if (Image3.transform.position.y < Image3Goal.position.y) { Image3.transform.position += new Vector3(0, Speed*SpeedModifier2); }

        if(Image1.transform.position.y >= Image1Goal.position.y && Image2.transform.position.y >= Image2Goal.position.y && Image3.transform.position.y >= Image3Goal.position.y
             && Flashed == false)
        { StartCoroutine(FlashAffect()); }

       if(PressToStartText.activeSelf == false && TextFlash == true)
        {
    //        StartCoroutine(TextFlashing());
        }
    }
    
    IEnumerator FlashAffect()
    {
        Flashed = true;
       yield return new WaitForSeconds(0.2f);
        WhiteScreenImage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        WhiteScreenImage.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        WhiteScreenImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        TitleText.SetActive(true);
        PressToStartText.SetActive(true);
        GroupNameText.SetActive(true);
        WhiteScreenImage.SetActive(false);
        TextFlash = true;
    }

   /* IEnumerator TextFlashing()
    {
        PressToStartText.SetActive(true);
        TextFlash = false;
        yield return new WaitForSeconds(0.3f);
        PressToStartText.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        TextFlash = true;
    } */


}
