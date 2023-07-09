using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsCounter : MonoBehaviour
{
    public itemID item;
    Text _text;
    private void Start()
    {
        if (_text == null)
            _text = GetComponent<Text>();
    }
    private void Update()
    {
        _text.text = "x" + storeManager.instance.GetValue(item).ToString();
    }
}
