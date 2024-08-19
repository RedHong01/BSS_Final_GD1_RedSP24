using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterHealthBar : MonoBehaviour
{
    public Image redHealthBar; 
    public Image whiteHealthBar; 
    public float maxHealth = 100.0f; 
    private float currentHealth; 
    public float flashDuration = 1.0f; 
    public float flashInterval = 0.1f; 
    public Color whiteColor = Color.white; 
    public float smoothDuration = 0.5f; 

    private float flashTimer;

    private bool isSmoothing = false;
    private float smoothStartTime;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {




        if (Input.GetKeyDown(KeyCode.Q))
        {

            TakeDamage(10);
        }



        if (isSmoothing)
        {
            
            if (smoothStartTime < 0)
            {
                smoothStartTime = 0.5f;
          
                float t = Mathf.Clamp01(smoothStartTime / smoothDuration);
                whiteHealthBar.fillAmount = Mathf.Lerp(whiteHealthBar.fillAmount, redHealthBar.fillAmount, t);
            }
            else
            {
                smoothStartTime -= Time.deltaTime;
              
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

    
        redHealthBar.fillAmount = currentHealth / maxHealth;


        isSmoothing = true;
        smoothStartTime = 0.5f;

    }

    private void UpdateHealthBar()
    {
     
        redHealthBar.fillAmount = currentHealth / maxHealth;
        whiteHealthBar.fillAmount = currentHealth / maxHealth;
        whiteHealthBar.color = redHealthBar.color;
    }
}

