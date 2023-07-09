using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    float _counter;
    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= 2)
        {
            ExplotionPool.instance.pool.ReturnObject(this);
        }
    }
    public static void TurnOn(Smoke e)
    {
        e.gameObject.SetActive(true);
    }
    public static void TurnOff(Smoke e)
    {
        e.gameObject.SetActive(false);
    }
}
