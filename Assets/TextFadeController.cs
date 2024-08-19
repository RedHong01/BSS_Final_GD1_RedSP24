using System.Collections;
using UnityEngine;
using TMPro;

public class TextFadeController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // ���� TextMeshProUGUI ���
    public float fadeDuration = 15f; // ���ƽ������ʱ��

    private void Start()
    {
        // ����Э���������ı��Ľ���Ч��
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // ��ȡ��ǰ�ı�����ɫ
        Color textColor = textMeshPro.color;
        float startAlpha = textColor.a; // ��ȡ��ǰ�� alpha ֵ

        float elapsedTime = 0f;

        // ѭ��ֱ��ʱ�䳬���������ʱ��
        while (elapsedTime < fadeDuration)
        {
            // ���㵱ǰ�� alpha ֵ
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);

            // �����ı���ɫ
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, newAlpha);

            // �����ѹ�ʱ��
            elapsedTime += Time.deltaTime;

            // �ȴ���һ֡
            yield return null;
        }

        // ȷ������ alpha Ϊ 0
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
    }
}
