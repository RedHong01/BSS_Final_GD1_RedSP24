using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScreenFlashRed : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.5f;
    public float fadeInTime = 0.1f; 
    public float fadeOutTime = 0.4f; 

    private Coroutine flashRoutine;


    private void Awake()
    {



    }

   
    public void TriggerFlash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashScreen());
    }

    private IEnumerator FlashScreen()
    {
     
        Color targetColor = flashImage.color;
        targetColor.a = 1f;
        float startTime = Time.time;
        while (Time.time < startTime + fadeInTime)
        {
            float t = (Time.time - startTime) / fadeInTime;
            flashImage.color = Color.Lerp(new Color(1f, 0f, 0f, 0f), targetColor, t);
            yield return null;
        }

     
        yield return new WaitForSeconds(flashDuration - fadeInTime - fadeOutTime);

       
        startTime = Time.time;
        while (Time.time < startTime + fadeOutTime)
        {
            float t = (Time.time - startTime) / fadeOutTime;
            flashImage.color = Color.Lerp(targetColor, new Color(1f, 0f, 0f, 0f), t);
            yield return null;
        }

   
        flashImage.color = new Color(1f, 0f, 0f, 0f);
        flashRoutine = null;
    }
}
