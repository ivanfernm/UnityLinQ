using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBonus : MonoBehaviour
{ 
    void Start()
    {
        storeManager.instance._coins += 50;
        storeManager.instance.SaveCoins();
    }

}
