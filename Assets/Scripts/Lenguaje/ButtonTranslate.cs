using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonTranslate: MonoBehaviour
{
    public string ID;
    public Text myText;

    void Start()
    {
        LangManager.instance.OnUpdate += ChangeLang;
        LangManager.instance.a();
    }

    void ChangeLang()
    {
        myText.text = LangManager.instance.GetTranslate(ID);
    }
    private void OnDestroy()
    {
        LangManager.instance.OnUpdate -= ChangeLang;
    }
}
