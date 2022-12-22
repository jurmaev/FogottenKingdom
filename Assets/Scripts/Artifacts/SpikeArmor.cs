using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeArmor : Artifact
{
    [SerializeField]private float amountOfDamage;
    protected override void UpgradePlayer(PlayerController player)
    {
        player.OnEnemyCollision += PrickEnemy;
    }

    private void PrickEnemy(Enemy enemy)
    {
        enemy.GetDamage(amountOfDamage);
    }
}
