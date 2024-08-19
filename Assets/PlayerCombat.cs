using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;






public class PlayerCombat : MonoBehaviour
{
    public PlayerAttributes attributes; // ������ԣ���������ֵ��������

    private Rigidbody playerRigidbody; // ��ҵĸ������������������




   public bool isStunned = false; // �Ƿ���ѣ��״̬

    public GameObject cube;

    public FlashRedOnDamage frd;

    public shakecamera mshack_camera;
    public ScreenFlashRed sfr;

    public float movespeed;


    public GameObject hero_main;
    public GameObject hero_shanbi;



    public PlayerAttack pa;


    public float attackCooldown = 1.0f; // ������ȴʱ��

    public float dodgeDuration = 0.5f; // ���ܳ���ʱ��


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


        //���¿ո�,����׼���׶�
        //�������,�����ס�ո�������Ծ�ĸ�Զ������Ŀ�ĵأ�ִ�й���ָ��
        //���û�а���
        //����ģʽ��
        //ֻ����������,��������
        //���¿ո�.����������,


        //����A��ʼ����,a�ɿ�,  �����������1S,Ϊ�ع���,����Ϊ�ṥ��


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
                        attributes.pt = player_type.�ع���;

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

                        attributes.pt = player_type.�ṥ��;
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


                        attributes.pt = player_type.����;
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
    public float checkDistance = 0.1f; // ���߼�����
    public LayerMask obsLayer; // �ϰ����
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
