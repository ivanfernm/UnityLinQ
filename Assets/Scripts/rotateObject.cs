using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [Header("Modifica el eje de rotación")]
    [SerializeField] Vector3 _rotate;
    void Update()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(_rotate);
    }
}
