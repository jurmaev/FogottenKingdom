using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public void MakeBlink(GameObject target, float opacity, float blinkingTime, float timeBetweenBlinks)
    {
        StartCoroutine(Blink(target, opacity, blinkingTime, timeBetweenBlinks));
    }


    private IEnumerator Blink(GameObject target, float opacity, float blinkingTime, float timeBetweenBlinks)
    {
        if (target.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            var startTime = Time.time;
            while (Time.time <= startTime + blinkingTime)
            {
                spriteRenderer.color = new Color(255, 255, 255, opacity);
                yield return new WaitForSeconds(timeBetweenBlinks);
                spriteRenderer.color = new Color(255, 255, 255, 255);
            }
        }
    }
}