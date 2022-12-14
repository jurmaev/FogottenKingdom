using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScroll : Artifact
{
    [SerializeField] private GameObject magic;
    protected override void UpgradePlayer(PlayerController player)
    {
        player.LearnNewMagic(magic);
    }
}
