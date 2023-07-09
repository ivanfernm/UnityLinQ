using System;
using UnityEngine.UI;
using UnityEngine;

public class AccText : MonoBehaviour
{
    public Text _Text;
    public LevelVars lv;

    public AccText instance;

    private void Awake()
    {
        _Text = gameObject.GetComponent<Text>();
        instance = this;
    }

    public  void UpdateAccText()
    {
        _Text.text = "" + lv.AccuarcyHit().Item2;
    }
}
