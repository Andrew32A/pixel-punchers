using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{
    // The duration of the flash effect
    public float flashDuration = 0.1f;
    
    // The color to use when flashing
    public Color flashColor = Color.white;

    // The sprite renderer component for the enemy
    private SpriteRenderer spriteRenderer;

    // Whether the enemy is currently flashing
    private bool isFlashing = false;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when the enemy is hit
    public void Hit()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    // Coroutine to handle the flash effect
    IEnumerator FlashCoroutine()
    {
        isFlashing = true;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}
