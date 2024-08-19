using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakecamera : MonoBehaviour
{


    public float shakeLevel = 3f;
    public float setShakeTime = 0.5f;  
    public float shakeFps = 45f;   

    private bool isshakeCamera = false;
    private float fps;
    private float shakeTime = 0.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    private Camera selfCamera;

    void OnEnable()
    {
       
    }

     
    public void start_sc()
    {
        isshakeCamera = true;
        selfCamera = gameObject.GetComponent<Camera>();
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
    }





    void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    selfCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        selfCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value), shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }



}





