using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyItem : MonoBehaviour
{
    public itemID itemBuy;
    AudioSource audioSource;
    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        storeManager.instance.addQuantity(itemBuy);
    }
    public void Buy()
    {
        if (storeManager.instance._coins >= itemBuy._itemValue)
        {
            storeManager.instance.addQuantity(itemBuy);
           audioSource.Play();
            storeManager.instance._coins -= itemBuy._itemValue;
        }
        else
            return;
    }
}
