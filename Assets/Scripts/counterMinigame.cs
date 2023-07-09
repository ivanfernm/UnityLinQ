using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class counterMinigame : MonoBehaviour
{
    [SerializeField] float _counter;
    [SerializeField] float _counter2 = 20;
    [SerializeField] Text text;
    private void Start()
    {
        if (text == null)
        {
            text = gameObject.GetComponent<Text>();
        }
    }
    void Update()
    {
        _counter += Time.deltaTime;
        text.text =_counter2.ToString();
        if (_counter >= 1.1f)
        {
            _counter = 0;
            _counter2--;
            _counter += Time.deltaTime;
        }
        if (_counter2 < 0)
        {
            //GameManager.instance.Save();
            SceneManager.LoadScene("menu2");
        }
    }
}
