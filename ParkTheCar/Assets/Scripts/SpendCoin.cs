using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendCoins : MonoBehaviour
{
    public GameObject targetObject; // The object to deactivate
    public int cost = 50; // Cost in coins to deactivate the object

    public void TrySpendCoins()
    {
        if (CoinCounter.Instance != null && CoinCounter.Instance.CoinCount >= cost)
        {
            // Spend coins and deactivate the object
            CoinCounter.Instance.SpendCoins(cost);
            targetObject.SetActive(false);
            Debug.Log($"Spent {cost} coins. {targetObject.name} deactivated.");
        }
        else
        {
            Debug.Log("Not enough coins to spend.");
        }
    }
}
