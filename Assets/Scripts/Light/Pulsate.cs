using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Pulsate : MonoBehaviour
{
    [SerializeField][Tooltip("Продолжительность изменения радиуса")] private float lerpDuration;
    [SerializeField][Tooltip("Величина изменения радиуса")] private float radiusDifference;
    [SerializeField][Tooltip("Величина изменения интенсивности")] private float intensityDifference;
    private float minIntensity, maxIntensity;
    private Light2D light;
    private float minRadius, maxRadius;
    private float timeElapsed;

    private void Start()
    {
        light = GetComponent<Light2D>();
        minRadius = light.pointLightOuterRadius - radiusDifference;
        maxRadius = light.pointLightOuterRadius + radiusDifference;
        minIntensity = light.intensity - intensityDifference;
        maxIntensity = light.intensity + intensityDifference;
        InvokeRepeating(nameof(ChangeIntensity), 0,.1f);
    }

    private void Update()
    {
        
        if (timeElapsed < lerpDuration)
        {
            light.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else if (Math.Abs(light.pointLightOuterRadius - maxRadius) <= 0.1 ||
                 Math.Abs(light.pointLightOuterAngle - minRadius) <= 0.1)
        {
            (minRadius, maxRadius) = (maxRadius, minRadius);
            timeElapsed = 0;
        }
        else
            light.pointLightOuterRadius =
                Math.Abs(light.pointLightOuterRadius - minRadius) < Math.Abs(light.pointLightOuterRadius - maxRadius)
                    ? minRadius
                    : maxRadius;
    }

    private void ChangeIntensity()
    {
        light.intensity = Random.Range(minIntensity, maxIntensity);
    }
}