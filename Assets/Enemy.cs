using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;


public enum state
{
    chase,
    idle,
    attack,


}

public class Enemy : MonoBehaviour
{
   
    public float health = 10f;
 
    public GameObject healthBarUI;

 
    public SpriteRenderer spriteRenderer;

    public Color originalColor;
  
    public float displayTime = 5f;

    public float fadeSpeed = 2f;

    public float atk;

    public FlashRedOnDamage frd;

    public shakecamera msc;
    public Rigidbody rigid;


    public state m_state;


    #region 击退


    public Transform player; 
    public float forceMagnitude = 1000.0f;
    public int numberOfJumps = 3; 
    public float pauseDuration = 1.0f; 


    private int currentJump = 0; 
    private float timer = 0.0f; 
    #endregion


    public audiomanager maudio;

     


    #region 追击逻辑

    public float chase_speed = 5f; 
    public float waitTime = 3f; 

    public float idle_time;
    public Vector3 targetpos;
    public Vector3 tar_dir;


    public MonsterHealthBar mhb;

    #endregion




    public void Awake()
    {

        chase_speed = 5;
        m_state = state.idle;

        forceMagnitude = 1;
        pauseDuration = 0.1f;


        atk = 3;



        msc = GameObject.Find("Main Camera").GetComponent<shakecamera>();
        player = GameObject.FindWithTag("Player").transform;
        maudio = GameObject.Find("audiomanager").GetComponent<audiomanager>();
    }



    private void Update()
    {






        switch (m_state)
        {
            case state.chase:

                    

                      


          
                if (targetpos != Vector3.zero)
                {

                    MoveTowardsTarget(targetpos,tar_dir);
                }
                else
                {
                 
                    tar_dir = (player.position- transform.position).normalized;
                    targetpos = transform.position + tar_dir *5;
                  


                }
           
        

                

              
                break;
            case state.idle:

                if (idle_time <= 0)
                {
             
                    m_state = state.chase;
                    targetpos = Vector3.zero;
                }
                else
                {
                    idle_time -= Time.deltaTime;
                }

                break;
            case state.attack:


                timer += Time.deltaTime;

            
                if (timer >= pauseDuration)
                {
                    PushBackwards();
                    timer = 0.0f;
                    currentJump++;

                   
                    if (currentJump >= numberOfJumps)
                    {
                        rigid.velocity = Vector3.zero;
                        m_state = state.idle;
                        targetpos = Vector3.zero;
                        idle_time = 1;
                        currentJump = 0;
                        timer = 0.0f;
                    }
                }

                break;

        }

    












    }




    #region 追击


    private void MoveTowardsTarget(Vector3 direction,Vector3 tar_dir)
    {
       
        transform.Translate(tar_dir * chase_speed * Time.deltaTime, Space.World);

        float dis = Vector3.Distance(transform.position, direction);
     
      
        if (dis< 1f)
        {
          
           
            m_state = state.idle;
            idle_time = 3;


        }

       



    }






    #endregion












    void PushBackwards()
    {
        maudio.play_audio();
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(-direction *3* Time.deltaTime, Space.World);
    }


    public void OnHitByPlayer()
    {
        m_state = state.attack;
   

        currentJump = 0;
        timer = pauseDuration;
    }











    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttributes p = other.GetComponent<PlayerAttributes>();
            if (p != null)
            {
                p.TakeDamage(atk);
            }
        }




    }



     







    public void TakeDamage(float amount)
    {
      
        health -= amount;



    

        healthBarUI.SetActive(true);


        mhb.TakeDamage(amount);

        frd.OnTakeDamage();

        msc.start_sc();

        OnHitByPlayer();



    
        if (health <= 0f)
        {
            Die(); 
        }
    }

    void Die()
    {
      
        Destroy(gameObject);
    }


 

   
 





}

