using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUIController : MonoBehaviour
{
    private Image cooldownImage;
    private bool isCooldown;
    private float cooldownTime;
    
    void Start()
    {
        cooldownImage = gameObject.GetComponent<Image>();
        cooldownImage.fillAmount = 0;
    }
    
    void Update()
    {
        if (isCooldown)
        {
            if(Math.Abs(cooldownImage.fillAmount - 1f) > 4.94065645841247E-324)
                cooldownImage.fillAmount = 1;
            cooldownImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;
            if (cooldownImage.fillAmount <= 0)
            {
                isCooldown = false;
                cooldownImage.fillAmount = 0;
            }
        }
    }

    public void Activate(float cooldownTime)
    {
        isCooldown = true;
        this.cooldownTime = cooldownTime;
    }
}
