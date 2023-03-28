using UnityEngine;

/// <summary>
/// This is a class, that is responsible to store money
/// </summary>
public class MoneyStorage : MonoBehaviour
{
    private MoneyUI moneyUI;
    private int levelStartMoneyAmount;
    private int moneyAmount;

    public int MoneyAmount => moneyAmount;

    public void InitializeMoneyStorage(MoneyUI moneyUI) //переделать
    {
        this.moneyUI = moneyUI;
        levelStartMoneyAmount = GetInitialMoneyAmount();
        AddMoney(levelStartMoneyAmount);
    }

    public void AddMoney(int amount)
    {
        moneyAmount += amount;

        moneyUI.UpdateMoneyUI(moneyAmount);
    }

    public void RemoveMoney(int amount)
    {
        moneyAmount -= amount;

        moneyUI.UpdateMoneyUI(moneyAmount);
    }

    private int GetInitialMoneyAmount()
    {
        return PlayerPrefs.GetInt("Money", 0);
    }

    public void SaveMoneyAmount()
    {
        levelStartMoneyAmount = moneyAmount;
        PlayerPrefs.SetInt("Money", moneyAmount);
    }

    public void ResetMoneyAmount()
    {
        moneyAmount = levelStartMoneyAmount;

        moneyUI.UpdateMoneyUI(moneyAmount);
    }
}
