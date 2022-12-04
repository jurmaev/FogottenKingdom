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
            if (value)
                frameImage.GetComponent<Image>().color = new Color(1, 0.9549635f, 0.4188679f);
            else
                frameImage.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    private bool isActive;
    [SerializeField] private GameObject skillImagePrefab;
    [SerializeField] private GameObject frameImage;


    public void SetSkillImage(SpriteRenderer skillSprite)
    {
        GameObject skillImage = Instantiate(skillImagePrefab, gameObject.transform, true);
        skillImage.GetComponent<Image>().sprite = skillSprite.sprite;
        RectTransform skillRectTransform = skillImage.GetComponent<RectTransform>();
        skillRectTransform.anchoredPosition = new Vector2(0, 0);
        skillRectTransform.Rotate(0, 0, -45);
    }
}