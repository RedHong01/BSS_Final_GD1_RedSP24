using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lose_ui : MonoBehaviour
{



    public Button btn;




    // Start is called before the first frame update
    void Start()
    {

        btn.onClick.AddListener(btnm);
        
    }

    private void btnm()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
