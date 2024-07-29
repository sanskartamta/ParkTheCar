using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource CoinSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinCounter coinCounter = FindObjectOfType<CoinCounter>(); // Find CoinCounter in the scene
            if (coinCounter != null)
            {
                coinCounter.CollectCoin(); // Call the CollectCoin method
            }
            CoinSFX.Play();
            gameObject.SetActive(false); // Deactivate the coin
        }
    }
}
