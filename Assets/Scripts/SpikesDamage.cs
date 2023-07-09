using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField]
    int _layerPlayer = 9;
    [SerializeField]
    int _damange = 5;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _layerPlayer)
        {
            other.GetComponent<IDamangeable>().CauseDamange(_damange);
        }
    }
}
