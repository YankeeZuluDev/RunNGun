using UnityEngine;
using TMPro;

/// <summary>
/// This class is used to update the text of UI, that displays player`s current amount of money
/// </summary>
public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmountText;

    public void UpdateMoneyUI(int moneyAmount)
    {
        moneyAmountText.text = moneyAmount.ToString();
    }
}
