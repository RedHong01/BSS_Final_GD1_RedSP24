using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRedOnDamage : MonoBehaviour
{






    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originalColor;

    private void Awake()
    {
      

   
        originalColor = spriteRenderer.color;
    }

    public void OnTakeDamage()
    {
      
        spriteRenderer.color = new Color(1, 0, 0, originalColor.a); 
        StartCoroutine(ResetColorAfterDelay(flashDuration)); 
    }

    IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = originalColor; 
    }







}
