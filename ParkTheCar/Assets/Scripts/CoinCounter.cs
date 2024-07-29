using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;
    public int CoinCount { get; private set; }
    public TextMeshProUGUI coinCountDisplay;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadCoinCount();
        UpdateCoinDisplay();
    }

    public void LoadCoinCount()
    {
        CoinCount = PlayerPrefs.GetInt("CoinCount", 0);
    }

    public void SaveCoinCount()
    {
        PlayerPrefs.SetInt("CoinCount", CoinCount);
        PlayerPrefs.Save();
    }

    public void CollectCoin()
    {
        CoinCount++;
        SaveCoinCount();
        UpdateCoinDisplay();
    }

    public void SpendCoins(int amount)
    {
        if (CoinCount >= amount)
        {
            CoinCount -= amount;
            SaveCoinCount();
            UpdateCoinDisplay();
        }
        else
        {
            Debug.LogError("Not enough coins to spend.");
        }
    }

    private void UpdateCoinDisplay()
    {
        if (coinCountDisplay != null)
        {
            coinCountDisplay.text = CoinCount.ToString();
        }
        else
        {
            Debug.LogError("coinCountDisplay is not assigned!");
        }
    }
}
