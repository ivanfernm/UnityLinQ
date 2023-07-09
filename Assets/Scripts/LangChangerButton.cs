using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LangChangerButton : MonoBehaviour
{
    [SerializeField]int counter;
    public Sprite[] _images;
    [SerializeField]SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void Change()
    {
        if (counter < _images.Length - 1)
        {
            counter++;
            gameObject.GetComponent<Image>().sprite = _images[counter];
        }
        else
        {
            counter = 0;
            gameObject.GetComponent<Image>().sprite = _images[counter];
        }
        LangManager.instance.chngButton();
    }    
}
