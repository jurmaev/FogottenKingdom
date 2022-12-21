using System;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int value;
    private TextMeshProUGUI valueText;

    public int Value => value;

    private void Start()
    {
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        valueText.text = value.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            if(other.gameObject.GetComponent<PlayerController>().Coins >= value)
                Destroy(gameObject);
    }
}