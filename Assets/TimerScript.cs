using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System; // 引用TextMeshPro命名空间

public class TimerScript : MonoBehaviour
{
    public float timeLeft = 60; // 秒
    public TextMeshProUGUI timerText; // 使用TextMeshProUGUI组件代替标准Unity UI Text
    public PlayerAttributes playerAttributes; // 引用PlayerAttributes脚本管理玩家血量

    void Start()
    {
    

    }

    void Update()
    {

        timeLeft -= Time.deltaTime;

        
        timerText.text = Convert.ToInt32(timeLeft).ToString() + "S";

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            SceneManager.LoadScene("GameWin"); // 倒计时结束，加载胜利场景
        }

        if (playerAttributes.currentHealth <= 0)
        {
            SceneManager.LoadScene("GameLose"); // 玩家血量归零，加载失败场景
        }
    }

   
}
