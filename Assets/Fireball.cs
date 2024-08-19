using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform Target;
    public float TrackingDuration;
    public float TrackingDistance;
    private float timer;
    public Vector3 lastDirection;
    public float atk;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < TrackingDuration)
        {
      
            Vector3 direction = Target.position+new Vector3(0,0.5f,0) - transform.position;
            float distance = Vector3.Distance(Target.position + new Vector3(0, 0.5f, 0), transform.position);

            if (distance <= TrackingDistance)
            {
           
                transform.Translate(direction.normalized * Time.deltaTime * 2f, Space.World);

              
                lastDirection = direction.normalized;
            }
            else
            {
       
            }
        }
        else
        {
           
            transform.Translate(lastDirection * Time.deltaTime * 2f, Space.World);
        }
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerAttributes p = other.GetComponent<PlayerAttributes>();
            if (p != null)
            {
                Destroy(gameObject);
                p.TakeDamage(atk);
            }
        }



    }


}
