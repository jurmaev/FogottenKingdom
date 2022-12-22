using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Artifact : MonoBehaviour
{
    [field: SerializeField] public string ArtifactName { get; protected set; }
    [field: SerializeField] public string Description { get; protected set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            UpgradePlayer(col.gameObject.GetComponent<PlayerController>());
            EventManager.SendArtifactSelection(this);
            Destroy(gameObject);
        }
    }

    protected abstract void UpgradePlayer(PlayerController player);
}
