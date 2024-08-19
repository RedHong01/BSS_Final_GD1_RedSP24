using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 在Inspector中指定的敌人预制体
    private float spawnY = 0.5f; // Y轴上的生成高度

    public float cur_time;
    public float max_time;


    


    private void Awake()
    {

        cur_time = 0;
        max_time = 5;

    }


     

    private void Update()
    {
       
        // 检查场景中的敌人数量并在需要时生成敌人
        if (cur_time<=0 )
        {
          
            GenerateEnemy();
            cur_time = max_time;
        }
        else
        {
      
            cur_time -= Time.deltaTime;


        }
    }

    void GenerateEnemy()
    {
        // 随机生成X和Z坐标
        float spawnX = Random.Range(-5f, 5f);
        float spawnZ = Random.Range(-5f, 5f);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // 生成敌人
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
