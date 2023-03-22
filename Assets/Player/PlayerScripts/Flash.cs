using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{
    // duration of the flash effect
    public float flashDuration = 0.1f;
    
    // color to use when flashing
    public Color flashColor = Color.white;

    // sprite renderer component for the enemy
    private SpriteRenderer spriteRenderer;

    // whether the enemy is currently flashing
    private bool isFlashing = false;

    // use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // called when the enemy is hit
    public void Hit()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    // coroutine to handle the flash effect
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
