using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMageBoss : MonoBehaviour
{


    [Header("Boss 参数")]
    public float moveSpeed = 1f; 
    public float attackRange = 10f; 
    public float sightAngle = 90f; 
    public float fireballCooldown = 3f; 
    public float hellfireCooldown = 8f; 
    public float turretCooldown = 10f; 
    public float trackingDuration = 3f; 
    public float trackingDistance = 10f; 


    public float halfHealth = 50;
    public float health = 100;

    [Header("预制件引用")]



    public GameObject healthBarUI;

    private Transform target; 
    private float fireballTimer; 
    private float hellfireTimer; 
    private float turretTimer; 
    private float trackingTimer; 
    private bool canSeePlayer; 
    private bool isChasing; 
    private bool isHellfireReady;


    private bool isTurretSpawned; 


    private Animator animator; 

    public FlashRedOnDamage frd;

    public MonsterHealthBar mhb;
    public audiomanager maudio;
    public shakecamera msc;



    public float waitTime = 3f;



    private void Start()
    {
        // 初始化
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isChasing = false;
        isHellfireReady = true;

        msc = GameObject.Find("Main Camera").GetComponent<shakecamera>();
        maudio = GameObject.Find("audiomanager").GetComponent<audiomanager>();

        isTurretSpawned = false;


        canSeePlayer = true;


     
        InvokeRepeating("TeleportMonster", waitTime, waitTime);

    }






    void TeleportMonster()
    {
    
        float spawnX =UnityEngine. Random.Range(-5f, 5f);
        float spawnZ = UnityEngine. Random.Range(-5f, 5f);
        Vector3 spawnPosition = new Vector3(spawnX, 0.5f, spawnZ);
  
        transform.position = spawnPosition;
    }



    private void Update()
    {
        // 检测玩家是否在视野范围内
      //  CheckPlayerInRange();

        // 如果可以看到玩家且没有追逐
    //    if (canSeePlayer && !isChasing)
     //   {
           // StartCoroutine(ChasePlayer());
     //   }

   
        if (fireballTimer <= 0f && canSeePlayer)
        {
      
            ShootFireball();
            fireballTimer = fireballCooldown;
        }
        else
        {
            fireballTimer -= Time.deltaTime;
        }

       
        if (hellfireTimer <= 0f && canSeePlayer)
        {
        
            CastHellfire();
            hellfireTimer = hellfireCooldown;
        }
        else
        {
            hellfireTimer -= Time.deltaTime;
        }

     
        if (!isTurretSpawned && health <= halfHealth)
        {
         
            SpawnTurret();
            isTurretSpawned = true;
            turretTimer = turretCooldown;
        }
        else if (turretTimer > 0)
        {
            turretTimer -= Time.deltaTime;
        }
    }

    private void CheckPlayerInRange()
    {
     
        Vector3 dirToPlayer = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

      
        canSeePlayer = (dirToPlayer.magnitude <= attackRange) && (angle <= sightAngle / 2);
    }

    private IEnumerator ChasePlayer()
    {
        isChasing = true;

        while (canSeePlayer)
        {
            // 追逐玩家
            transform.LookAt(target);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

            yield return null;
        }

        isChasing = false;
    }

    private void ShootFireball()
    {
      
        GameObject fireball = Instantiate(Resources.Load<GameObject>("Fireball"), transform.position + transform.forward * 1.5f, Quaternion.identity);
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        fireballScript.Target = target;
        fireballScript.TrackingDuration = trackingDuration;
        fireballScript.TrackingDistance = trackingDistance;
    }

    private void CastHellfire()
    {
       


        
            GameObject hellfire = Instantiate(Resources.Load<GameObject>("HellfireAOE"), transform.position, Quaternion.identity);
            HellfireAOE hellfireScript = hellfire.GetComponent<HellfireAOE>();
            hellfireScript.Delay = 2f; 
            hellfireScript.DamageRadius = 5f; 
            hellfireScript.StunDuration = 1f; 




    }

    private void SpawnTurret()
    {
     
        GameObject turret = Instantiate(Resources.Load<GameObject>("Turret"), transform.position, Quaternion.identity);
        Turret turretScript = turret.GetComponent<Turret>();
        turretScript.Target = target;
    }

 

    void Die()
    {
       
        Destroy(gameObject);
    }
  









    public void TakeDamage(float amount)
    {
    
        health -= amount;
    


        healthBarUI.SetActive(true);

        mhb.TakeDamage(amount);


        msc.start_sc();

        frd.OnTakeDamage();
        maudio.play_audio();



   
        if (health <= 0f)
        {
            Die(); 
        }
    }









}
