using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource CoinSFX;
    private void OnTriggerEnter(Collider other)
    {
       this.gameObject.SetActive(false);
    }
}