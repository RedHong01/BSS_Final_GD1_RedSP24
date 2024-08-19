using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{
    public Canvas canvas; // 引用 Canvas 组件
    public float baseWidth = 1920f; // 基础宽度（通常是设计时的分辨率宽度）
    public float baseHeight = 1080f; // 基础高度（通常是设计时的分辨率高度）
    public RectTransform uiElement; // 需要缩放的 UI 元素

    private void Start()
    {
        // 初始化缩放
        ScaleUIElement();
    }

    private void ScaleUIElement()
    {
        // 获取 Canvas 的 CanvasScaler 组件
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

        if (scaler == null)
        {
            Debug.LogError("CanvasScaler component is missing on the Canvas.");
            return;
        }

        // 获取当前屏幕分辨率
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 计算宽高比例
        float widthRatio = screenWidth / baseWidth;
        float heightRatio = screenHeight / baseHeight;
        float scaleRatio = Mathf.Min(widthRatio, heightRatio);

        // 应用缩放
        uiElement.sizeDelta = new Vector2(baseWidth * scaleRatio, baseHeight * scaleRatio);
    }
}
