using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_fire_ball : MonoBehaviour
{

    public float atk;
    private void Awake()
    {

        atk = 3;

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
