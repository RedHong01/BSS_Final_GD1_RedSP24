using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public enum player_type
{
    闪避,
    轻攻击,
    重攻击,
    none,

}

public class PlayerAttributes : MonoBehaviour
{
    // 定义角色的健康属性
    [Header("健康属性")]
    public Image healthBar; // 健康条的UI元素
    public float currentHealth; // 当前生命值
    public float maxHealth = 100f; // 最大生命值

    // 定义角色的耐力属性
    [Header("耐力属性")]
    public Image staminaBar; // 耐力条的UI元素
    public float currentStamina; // 当前耐力值
    public float maxStamina = 100f; // 最大耐力值
    public float staminaRecoveryRate = 5f; // 耐力恢复速率


    public int lightAttackDamage = 10; // 轻攻击造成的伤害
    public int heavyAttackDamage = 20; // 重攻击造成的伤害


    // 定义其他属性
    [Header("其他属性")]
    private bool isInvincible = false; // 记录无敌状态

    public player_type pt;



    public PlayerCombat pc;








    // 在游戏开始时初始化健康和耐力值，并更新UI
    private void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;

        UpdateHealthUI();
        UpdateStaminaUI();
    }

    // 更新函数，每帧调用一次
    private void Update()
    {
        RegenerateStamina(); // 每帧恢复耐力
    }

    // 耐力恢复函数
    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime; // 根据恢复速率增加耐力
            currentStamina = Mathf.Min(currentStamina, maxStamina); // 确保耐力不超过最大值
            UpdateStaminaUI(); // 更新耐力UI
        }
    }

    // 设置角色的无敌状态
    public void SetInvincibility(bool state)
    {
        isInvincible = state;
    }

    // 角色受到伤害
    public void TakeDamage(float amount)
    {
        if (!isInvincible) // 如果不是无敌状态
        {

            pc.frd.OnTakeDamage();
            pc.mshack_camera.start_sc();
            pc.sfr.TriggerFlash();

            currentHealth -= amount; // 减少生命值
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 确保生命值在合理范围内
            UpdateHealthUI(); // 更新健康UI
                              // 受伤时的额外逻辑，例如更新UI或播放受伤动画
        }
    }

    // 给角色增加生命值
    public void AddHealth(float amount)
    {
        currentHealth += amount; // 增加生命值
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 确保生命值在合理范围内
        UpdateHealthUI(); // 更新健康UI
    }

    // 给角色增加耐力值
    public void AddStamina(float amount)
    {
        currentStamina += amount; // 增加耐力值
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // 确保耐力值在合理范围内
        UpdateStaminaUI(); // 更新耐力UI
    }

    // 减少角色的耐力值
    public void ConsumeStamina(float amount)
    {
        currentStamina -= amount; // 减少耐力值
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // 确保耐力值在合理范围内
        UpdateStaminaUI(); // 更新耐力UI
    }

    // 更新健康UI
    private void UpdateHealthUI()
    {
        if (healthBar != null) // 检查健康条是否已分配
        {
            healthBar.fillAmount = currentHealth / maxHealth; // 更新健康条的填充比例
        }
    }

    // 更新耐力UI
    private void UpdateStaminaUI()
    {
        if (staminaBar != null) // 检查耐力条是否已分配
        {
            staminaBar.fillAmount = currentStamina / maxStamina; // 更新耐力条的填充比例
        }
    }
}