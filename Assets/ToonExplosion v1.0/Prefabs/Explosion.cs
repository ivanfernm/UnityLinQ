using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float _counter;
    void Update()
    {
        _counter += Time.deltaTime;
        if(_counter >= 2)
        {
            Destroy(gameObject);
        }
    }
}
