using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static int CoinCount;
    public GameObject coinCountDisplay;

    void Update()
    {
        
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = "" + CoinCount;
        
    }
}