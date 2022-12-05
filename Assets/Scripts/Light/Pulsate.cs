using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Pulsate : MonoBehaviour
{
    [SerializeField] private float minRadius, maxRadius;
    // [SerializeField] private float maxRadius;
    [SerializeField] private float lerpDuration;
    [SerializeField] private Light2D light;
    [SerializeField] private float minIntensity, maxIntensity;
    private float timeElapsed;

    private void Start()
    {
        light.pointLightOuterRadius = minRadius;
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