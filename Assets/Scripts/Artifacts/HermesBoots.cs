using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesBoots : Artifact
{
    [SerializeField]private float amountOfSpeed;
    protected override void UpgradePlayer(PlayerController player)
    {   
        player.IncreaseSpeed(amountOfSpeed);
    }
}
