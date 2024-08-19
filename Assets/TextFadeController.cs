using System.Collections;
using UnityEngine;
using TMPro;

public class TextFadeController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 引用 TextMeshProUGUI 组件
    public float fadeDuration = 15f; // 控制渐变持续时间

    private void Start()
    {
        // 启动协程来控制文本的渐变效果
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // 获取当前文本的颜色
        Color textColor = textMeshPro.color;
        float startAlpha = textColor.a; // 获取当前的 alpha 值

        float elapsedTime = 0f;

        // 循环直到时间超过渐变持续时间
        while (elapsedTime < fadeDuration)
        {
            // 计算当前的 alpha 值
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);

            // 更新文本颜色
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, newAlpha);

            // 更新已过时间
            elapsedTime += Time.deltaTime;

            // 等待下一帧
            yield return null;
        }

        // 确保最终 alpha 为 0
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
    }
}
