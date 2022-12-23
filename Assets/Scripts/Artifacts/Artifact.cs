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
    [SerializeField]private TextMeshPro priceText;

    private void Start()
    {
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
                player.TryChangeCoins(Price);
                UpgradePlayer(col.gameObject.GetComponent<PlayerController>());
                EventManager.SendArtifactSelection(this);
                Destroy(gameObject);
            }
            
        }
    }

    protected abstract void UpgradePlayer(PlayerController player);
}
