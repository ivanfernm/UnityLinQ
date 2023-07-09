using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinsGacha : MonoBehaviour
{
    public static skinsGacha instance;
    public Material[] _skins;
    //public Material[] _hatSkins;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);

    }
}
