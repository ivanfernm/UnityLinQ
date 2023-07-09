using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinText : MonoBehaviour
{
    Text text;
    private void Start()
    {
        if(text == null)
        {
            text = gameObject.GetComponent<Text>();
        }
    }
    void Update()
    {
        text.text = storeManager.instance._coins.ToString();
    }
}
