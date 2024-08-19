using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{
    public Canvas canvas; // ���� Canvas ���
    public float baseWidth = 1920f; // ������ȣ�ͨ�������ʱ�ķֱ��ʿ�ȣ�
    public float baseHeight = 1080f; // �����߶ȣ�ͨ�������ʱ�ķֱ��ʸ߶ȣ�
    public RectTransform uiElement; // ��Ҫ���ŵ� UI Ԫ��

    private void Start()
    {
        // ��ʼ������
        ScaleUIElement();
    }

    private void ScaleUIElement()
    {
        // ��ȡ Canvas �� CanvasScaler ���
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

        if (scaler == null)
        {
            Debug.LogError("CanvasScaler component is missing on the Canvas.");
            return;
        }

        // ��ȡ��ǰ��Ļ�ֱ���
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // �����߱���
        float widthRatio = screenWidth / baseWidth;
        float heightRatio = screenHeight / baseHeight;
        float scaleRatio = Mathf.Min(widthRatio, heightRatio);

        // Ӧ������
        uiElement.sizeDelta = new Vector2(baseWidth * scaleRatio, baseHeight * scaleRatio);
    }
}
