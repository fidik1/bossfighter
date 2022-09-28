using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ResetDash()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        StopCoroutine(Dash());
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
