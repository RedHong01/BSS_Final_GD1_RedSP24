using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform Target;
    private float fireRate = 1f;
    private float timer = 0f;



    private void Awake()
    {
        


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
        
            GameObject smallFireball = Instantiate(Resources.Load<GameObject>("Turret_Fireball"), transform.position + transform.forward * 1.5f, Quaternion.identity);
            smallFireball.GetComponent<Rigidbody>().velocity = (Target.position - transform.position).normalized * 5f;

            timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {

    
            TakeDamage(50);
        }
    }

    private void TakeDamage(int damage)
    {
   
        int currentHealth = 100;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
