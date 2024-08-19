using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public enum player_type
{
    ����,
    �ṥ��,
    �ع���,
    none,

}

public class PlayerAttributes : MonoBehaviour
{
    // �����ɫ�Ľ�������
    [Header("��������")]
    public Image healthBar; // ��������UIԪ��
    public float currentHealth; // ��ǰ����ֵ
    public float maxHealth = 100f; // �������ֵ

    // �����ɫ����������
    [Header("��������")]
    public Image staminaBar; // ��������UIԪ��
    public float currentStamina; // ��ǰ����ֵ
    public float maxStamina = 100f; // �������ֵ
    public float staminaRecoveryRate = 5f; // �����ָ�����


    public int lightAttackDamage = 10; // �ṥ����ɵ��˺�
    public int heavyAttackDamage = 20; // �ع�����ɵ��˺�


    // ������������
    [Header("��������")]
    private bool isInvincible = false; // ��¼�޵�״̬

    public player_type pt;



    public PlayerCombat pc;








    // ����Ϸ��ʼʱ��ʼ������������ֵ��������UI
    private void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;

        UpdateHealthUI();
        UpdateStaminaUI();
    }

    // ���º�����ÿ֡����һ��
    private void Update()
    {
        RegenerateStamina(); // ÿ֡�ָ�����
    }

    // �����ָ�����
    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime; // ���ݻָ�������������
            currentStamina = Mathf.Min(currentStamina, maxStamina); // ȷ���������������ֵ
            UpdateStaminaUI(); // ��������UI
        }
    }

    // ���ý�ɫ���޵�״̬
    public void SetInvincibility(bool state)
    {
        isInvincible = state;
    }

    // ��ɫ�ܵ��˺�
    public void TakeDamage(float amount)
    {
        if (!isInvincible) // ��������޵�״̬
        {

            pc.frd.OnTakeDamage();
            pc.mshack_camera.start_sc();
            pc.sfr.TriggerFlash();

            currentHealth -= amount; // ��������ֵ
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ȷ������ֵ�ں���Χ��
            UpdateHealthUI(); // ���½���UI
                              // ����ʱ�Ķ����߼����������UI�򲥷����˶���
        }
    }

    // ����ɫ��������ֵ
    public void AddHealth(float amount)
    {
        currentHealth += amount; // ��������ֵ
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ȷ������ֵ�ں���Χ��
        UpdateHealthUI(); // ���½���UI
    }

    // ����ɫ��������ֵ
    public void AddStamina(float amount)
    {
        currentStamina += amount; // ��������ֵ
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // ȷ������ֵ�ں���Χ��
        UpdateStaminaUI(); // ��������UI
    }

    // ���ٽ�ɫ������ֵ
    public void ConsumeStamina(float amount)
    {
        currentStamina -= amount; // ��������ֵ
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // ȷ������ֵ�ں���Χ��
        UpdateStaminaUI(); // ��������UI
    }

    // ���½���UI
    private void UpdateHealthUI()
    {
        if (healthBar != null) // ��齡�����Ƿ��ѷ���
        {
            healthBar.fillAmount = currentHealth / maxHealth; // ���½�������������
        }
    }

    // ��������UI
    private void UpdateStaminaUI()
    {
        if (staminaBar != null) // ����������Ƿ��ѷ���
        {
            staminaBar.fillAmount = currentStamina / maxStamina; // ������������������
        }
    }
}