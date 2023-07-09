using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liquidDamange : MonoBehaviour
{
    float _counter;
    [SerializeField] float _cooldown;
    [SerializeField] int _layer;
    [SerializeField] int _damange = 1;
    private void Update()
    {
        _counter += Time.deltaTime;
        _cooldown += Time.deltaTime;
        if (_counter >= 30)
        {
            Destroy(gameObject);
        }
        if (_cooldown >= 1.3f)
        {
            _cooldown = 0;
            _cooldown += Time.deltaTime;
        }

    }
    
    private void OnTriggerStay(Collider other)
    {
        var hit = other.gameObject.GetComponent<Enemy>();
        if (hit != null)
        {
            if (_cooldown >= 1)
            {
                hit.gameObject.GetComponent<IDamangeable>().CauseDamange(_damange);
            }
            
        }
    }
}
