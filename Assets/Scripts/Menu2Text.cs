using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu2Text : MonoBehaviour
{ 
    int num=0;
    public Text text;
    [SerializeField] string[] _texts;
    private void Awake()
    {
        num = Random.Range(1,_texts.Length);
    }
    void Start()
    {
        LangManager.instance.OnUpdate += ChangeLang;
        LangManager.instance.a();
    }
    void ChangeLang()
    {
        text.text = LangManager.instance.GetTranslate(_texts[num]);
    }
    private void OnDestroy()
    {
        LangManager.instance.OnUpdate -= ChangeLang;
    }
}
