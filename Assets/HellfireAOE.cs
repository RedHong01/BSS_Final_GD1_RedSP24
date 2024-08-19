using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireAOE : MonoBehaviour
{
    public float Delay;
    public float DamageRadius;
    public float StunDuration;

    public float time;
    public bool open_atk;

    private void Start()
    {
        open_atk = false;
  
  
        StartCoroutine(DelayedExplosion());
    }

    private void Update()
    {



        if (open_atk)
        {
            if (time <= 0)
            {

                Destroy(gameObject);
            }
            else
            {

                time -= Time.deltaTime;

            }
            Collider[] hitColliders = Physics.OverlapSphere(transform.position,1);

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {

             
               
                    collider.GetComponent<PlayerAttributes>().TakeDamage(0.05f); // Ê¾ÀýÉËº¦Öµ
                                
        
                    collider.GetComponent<PlayerCombat>().StunPlayer(0.1f);

         

                }
      
            }

        }

       

    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(Delay);



        open_atk = true;
        time = 1;
    }
}
