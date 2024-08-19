using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;






public class PlayerCombat : MonoBehaviour
{
    public PlayerAttributes attributes; // 玩家属性，例如生命值、耐力等

    private Rigidbody playerRigidbody; // 玩家的刚体组件，用于物理交互




   public bool isStunned = false; // 是否处于眩晕状态

    public GameObject cube;

    public FlashRedOnDamage frd;

    public shakecamera mshack_camera;
    public ScreenFlashRed sfr;

    public float movespeed;


    public GameObject hero_main;
    public GameObject hero_shanbi;



    public PlayerAttack pa;


    public float attackCooldown = 1.0f; // 攻击冷却时间

    public float dodgeDuration = 0.5f; // 闪避持续时间


    public float _attackTimer;




    private void Start()
    {
        _attackTimer = 0.0f;



        playerRigidbody = GetComponent<Rigidbody>(); 


        movefinish = true;

    }



    public Vector3 finalTargetPos;
    private void Update()
    {


        //按下空格,进入准备阶段
        //按下鼠标,如果按住空格，蓄力飞跃的更远，到达目的地，执行攻击指令
        //如果没有按下
        //三种模式：
        //只按下鼠标左键,进行闪避
        //按下空格.按照鼠标左键,


        //按下A开始蓄力,a松开,  如果蓄力超过1S,为重攻击,否则为轻攻击


        if (isStunned)
        {
            return;
        }




        if (attributes.pt== player_type.none&&attributes.currentStamina>=10)
        {
            if (Input.GetKey(KeyCode.Space))
            {
            
                _attackTimer += Time.deltaTime;


            }

            if (Input.GetMouseButtonDown(0))
            {


                if (_attackTimer > 1.0f)
                {

                  
               
                    if (movefinish)
                    {
                
                        movefinish = false;


                
                        movespeed = 15;


                        Plane playerPlane = new Plane(Vector3.up, transform.position); 
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                        float hitDist = 0.0f;

                        if (playerPlane.Raycast(ray, out hitDist)) 
                        {
                            Vector3 targetPos = ray.GetPoint(hitDist); 

                     
                            Vector3 direction = (targetPos - transform.position).normalized;
                            float distance = _attackTimer * 2; 

                     
                            finalTargetPos = transform.position + direction * distance;


                            pa.dir_atk(finalTargetPos);
                            
                            hero_main.SetActive(true);
                            hero_shanbi.SetActive(false);
                       
                            cube.transform.position = finalTargetPos;
                        }

                        _attackTimer = 0;

                        attributes.ConsumeStamina(50);                             
                        attributes.pt = player_type.重攻击;

                    }



                }
                else if (_attackTimer > 0.0f && _attackTimer < 1.0f)
                {
                  
                    if (movefinish)
                    {





                
                        movefinish = false;
                        movespeed = 10;

                        Plane playerPlane = new Plane(Vector3.up, transform.position); 
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                        float hitDist = 0.0f;

                        if (playerPlane.Raycast(ray, out hitDist)) 
                        {
                            Vector3 targetPos = ray.GetPoint(hitDist); 

                   
                            Vector3 direction = (targetPos - transform.position).normalized;
                            float distance = 2f; 

                         
                            finalTargetPos = transform.position + direction * distance;
                            pa.dir_atk(finalTargetPos);
                         
                            cube.transform.position = finalTargetPos;





                            hero_main.SetActive(true);
                            hero_shanbi.SetActive(false);

                            attributes.ConsumeStamina(10);
                        }

                        attributes.pt = player_type.轻攻击;
                        _attackTimer = 0;

                    }
                }
                else
                {
                  
                    if (movefinish)
                    {
                     
                        movefinish = false;

                        movespeed = 5;

                        Plane playerPlane = new Plane(Vector3.up, transform.position); 
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                        float hitDist = 0.0f;

                        if (playerPlane.Raycast(ray, out hitDist)) 
                        {
                            Vector3 targetPos = ray.GetPoint(hitDist);

                      
                            Vector3 direction = (targetPos - transform.position).normalized;
                            float distance = 3; 

                          
                            finalTargetPos = transform.position + direction * distance;
                            pa.dir_atk(finalTargetPos);
                         
                            cube.transform.position = finalTargetPos;

                            hero_main.SetActive(false);
                            hero_shanbi.SetActive(true);

                        }


                        attributes.pt = player_type.闪避;
                        attributes.SetInvincibility(true);
                        _attackTimer = 0;

                    }



                }

            }




        }







     





        if (!movefinish)
        {
            MoveTowards(finalTargetPos);
        }







    }

 











    public bool movefinish;
    public float checkDistance = 0.1f; // 射线检查距离
    public LayerMask obsLayer; // 障碍物层
    void MoveTowards(Vector3 targetPosition)
    {
        
        Vector3 direction = (targetPosition - transform.position).normalized;


        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), direction * 3, Color.green);





 
        RaycastHit hit;
        if (Physics.Raycast(transform.position+new Vector3(0,0.5f,0), direction, out hit, checkDistance, 1 <<LayerMask.NameToLayer("obs")))
        {



         

            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
            
                playerRigidbody.velocity = Vector3.zero;


             

             
                attributes.pt = player_type.none;

                attributes.SetInvincibility(false);

                hero_main.SetActive(true);
                hero_shanbi.SetActive(false);

             
                movefinish = true;
                return; 
            }
        }




      
        playerRigidbody.MovePosition(transform.position + direction * movespeed * Time.fixedDeltaTime);

  
        if (Vector3.Distance(transform.position, targetPosition) <= 1.0f)
        {
            playerRigidbody.velocity = Vector3.zero;
     
            movefinish = true;

            hero_main.SetActive(true);
            hero_shanbi.SetActive(false);
          
            attributes.pt = player_type.none;

           
            attributes.SetInvincibility(false);

        }
    }







  












    public void StunPlayer(float duration) 
    {
        isStunned = true;
        StartCoroutine(StunCoroutine(duration)); 
    }

    private IEnumerator StunCoroutine(float duration) 
    {
        yield return new WaitForSeconds(duration); 
        isStunned = false;


        playerRigidbody.velocity = Vector3.zero;


       


        attributes.pt = player_type.none;

        attributes.SetInvincibility(false);

        hero_main.SetActive(true);
        hero_shanbi.SetActive(false);


        movefinish = true;

    }







}
