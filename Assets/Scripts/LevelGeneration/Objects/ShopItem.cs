// using System;
// using TMPro;
// using UnityEngine;
//
// public abstract class ShopItem : MonoBehaviour
// {
//     [SerializeField] private int value;
//     private TextMeshProUGUI valueText;
//
//     public int Value => value;
//
//     private void Start()
//     {
//         valueText = GetComponentInChildren<TextMeshProUGUI>();
//         valueText.text = value.ToString();
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if(other.gameObject.TryGetComponent(out PlayerController player))
//             if (player.Coins >= value)
//             {
//                 EventManager.SendCoinAmountChanged(value);
//                 UpgradePlayer(other.gameObject.GetComponent<PlayerController>());
//                 EventManager.SendArtifactSelection(this);
//                 Destroy(gameObject);
//             }
//     }
//     
//     protected abstract void UpgradePlayer(PlayerController player);
//
// }