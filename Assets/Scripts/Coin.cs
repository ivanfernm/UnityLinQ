using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int _layerPlayer;
    [SerializeField]AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.layer == _layerPlayer)
        {
            audioSource.Play();
            storeManager.instance._coins +=1;
            CoinsPool.instance.pool.ReturnObject(this);
        }
        
        
    }
    public static void TurnOn(Coin e)
    {
        e.gameObject.SetActive(true);
    }
    public static void TurnOff(Coin e)
    {
        e.gameObject.SetActive(false);
    }
}
