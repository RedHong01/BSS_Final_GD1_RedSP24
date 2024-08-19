using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionEffects : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite squareSprite;
    public Sprite triangleSprite;
    public float actionDuration = 1f; // Duration of the action effect
    private bool isPerformingAction = false;

    void Start()
    {
        spriteRenderer.sprite = squareSprite; // Assuming starting as a square
    }

    // Call this method to change sprite and start effects when an action is performed
    public void PerformAction()
    {
        spriteRenderer.sprite = triangleSprite; // Change to triangle
        StartCoroutine(ActionEffects());
    }

    private IEnumerator ActionEffects()
    {
        isPerformingAction = true;
        float timer = 0f;

        // Initial rotation
        transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);

        // Color change loop
        while (timer < actionDuration)
        {
            // Rotate Y axis
            transform.Rotate(0f, 360f * Time.deltaTime / actionDuration, 0f, Space.Self);

            // Color change from white to black and back
            float lerpFactor = Mathf.PingPong(timer, actionDuration / 2) / (actionDuration / 2);
            spriteRenderer.color = Color.Lerp(Color.white, Color.black, lerpFactor);

            timer += Time.deltaTime;
            yield return null;
        }

        // Reset properties
        spriteRenderer.sprite = squareSprite; // Change back to square
        spriteRenderer.color = Color.white; // Reset color
        isPerformingAction = false;
    }
}

