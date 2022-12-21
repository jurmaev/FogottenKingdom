using TMPro;
using UnityEngine;

public class CoinAmount : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    private void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        EventManager.OnCoinPicked.AddListener(ChangeAmount);
    }

    private void ChangeAmount(int newValue)
    {
        coinText.text = newValue.ToString();
    }
}
