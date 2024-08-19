using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerAttributes playerAttributes;




    private void Start()
    {
        // 假设PlayerAttributes组件位于父对象上
        playerAttributes = GetComponentInParent<PlayerAttributes>();
    }







    public void dir_atk(Vector3 pos)
    {

        transform.LookAt(pos);

    }



    private void OnTriggerEnter(Collider other)
    {


        switch (playerAttributes.pt)
        {
            case player_type.重攻击:
                if (other.gameObject.CompareTag("Enemy"))
                {

              
                    Enemy enemy = other.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                         enemy.TakeDamage(playerAttributes.lightAttackDamage);
                    }
                    else
                    {
                        if (other.GetComponent<BlackMageBoss>() != null)
                        {
                            other.GetComponent<BlackMageBoss>().TakeDamage(playerAttributes.lightAttackDamage);
                        }
                         

                    }

              
                    playerAttributes.pc.movefinish = false;
             
                    playerAttributes.pt = player_type.none;

                }
                break;
            case player_type.轻攻击:

                if (other.gameObject.CompareTag("Enemy"))
                {

                  
                    Enemy enemy = other.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(playerAttributes.heavyAttackDamage);
                    }
                    else
                    {
                        if (other.GetComponent<BlackMageBoss>() != null)
                        {
                            other.GetComponent<BlackMageBoss>().TakeDamage(playerAttributes.heavyAttackDamage);
                        }

                    }
                  
                    playerAttributes.pc.movefinish = false;
                  
                    playerAttributes.pt = player_type.none;

                }
                break;

        }

       
    }
}
