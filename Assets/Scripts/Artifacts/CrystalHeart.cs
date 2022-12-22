using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHeart : Artifact
{
    [SerializeField]private float amountOfHealth;
    protected override void UpgradePlayer(PlayerController player)
    {
        player.IncreaseMaximumHealth(amountOfHealth);
    }
}
