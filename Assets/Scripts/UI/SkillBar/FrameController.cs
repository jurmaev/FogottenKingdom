using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    public bool IsActive
    {
        get => isActive;
        set
        {
            if(value)
                frameImage.GetComponent<Image>().color = new Color(1, 0.9549635f, 0.4188679f);
            else
                frameImage.GetComponent<Image>().color = new Color(1, 0.9549635f, 0.4188679f);
        }
    }

    private bool isActive;
    [SerializeField] private GameObject skillImagePrefab;
    [SerializeField] private GameObject frameImage;
    
    
    public void SetSkillImage(SpriteRenderer skillSprite)
    {
        GameObject skillImage = Instantiate(skillImagePrefab, gameObject.transform, true);
        skillImage.GetComponent<RectTransform>().position = new Vector3(0,0);
        skillImage.GetComponent<Image>().sprite = skillSprite.sprite;
    }
}
