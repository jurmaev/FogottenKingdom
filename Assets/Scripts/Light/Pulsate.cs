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
    private Light2D pointLight;
    private float minRadius, maxRadius;
    private float timeElapsed;

    private void Start()
    {
        pointLight = GetComponent<Light2D>();
        minRadius = pointLight.pointLightOuterRadius - radiusDifference;
        maxRadius = pointLight.pointLightOuterRadius + radiusDifference;
        minIntensity = pointLight.intensity - intensityDifference;
        maxIntensity = pointLight.intensity + intensityDifference;
        InvokeRepeating(nameof(ChangeIntensity), 0,.1f);
    }

    private void Update()
    {
        
        if (timeElapsed < lerpDuration)
        {
            pointLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else if (Math.Abs(pointLight.pointLightOuterRadius - maxRadius) <= 0.1 ||
                 Math.Abs(pointLight.pointLightOuterAngle - minRadius) <= 0.1)
        {
            (minRadius, maxRadius) = (maxRadius, minRadius);
            timeElapsed = 0;
        }
        else
            pointLight.pointLightOuterRadius =
                Math.Abs(pointLight.pointLightOuterRadius - minRadius) < Math.Abs(pointLight.pointLightOuterRadius - maxRadius)
                    ? minRadius
                    : maxRadius;
    }

    private void ChangeIntensity()
    {
        pointLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}