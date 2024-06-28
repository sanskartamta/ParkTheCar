using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource CoinSFX;
    private void OnTriggerEnter(Collider other)
    {
        CoinCounter.CoinCount += 1;
        this.gameObject.SetActive(false);
        CoinSFX.Play();
    }
}