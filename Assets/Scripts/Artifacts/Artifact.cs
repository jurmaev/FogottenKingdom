using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Artifact : MonoBehaviour
{
    [field: SerializeField] public string ArtifactName { get; protected set; }
    [field: SerializeField] public string Description { get; protected set; }
    [field: SerializeField] public int Price { get; protected set; }
    private TextMeshProUGUI priceText;

    private void Start()
    {
        priceText = GetComponentInChildren<TextMeshProUGUI>();
        if (Price == 0)
            priceText.text = "";
        else
            priceText.text = Price.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerController player))
        {
            if (player.Coins >= Price)
            {
                // EventManager.SendCoinAmountChanged(Price);
                UpgradePlayer(col.gameObject.GetComponent<PlayerController>());
                EventManager.SendArtifactSelection(this);
                Destroy(gameObject);
            }
            
        }
    }

    protected abstract void UpgradePlayer(PlayerController player);
}
