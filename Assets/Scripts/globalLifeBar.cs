using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globalLifeBar : MonoBehaviour, IObserver
{   
    [Header("Objecto a observar:")]
    [SerializeField] GameObject _objectToObserve;
    IObservable _obtainInterface;

    [Header("Vida maxima del objeto:")]
    [SerializeField] private float _maxLife = 100;

    [Header("Imagen (automatico):")]
    [SerializeField] Image Bar;
    
    void Start()
    {
        
        if (_objectToObserve.GetComponent<Player>())
        {
            _obtainInterface = _objectToObserve.GetComponent<Player>();
            Debug.Log("Player set");
        }
        else if (_objectToObserve.GetComponent<ObjectDamangeable>())
        {
            _obtainInterface = _objectToObserve.GetComponent<ObjectDamangeable>();
            Debug.Log("ObjectDamangeable set");
        }
        else
            Debug.LogWarning(_objectToObserve.name + " doesn't contain IObserver interface (20)");

        if (Bar == null)
            Bar = GetComponent<Image>();
        if(_obtainInterface != null)
            _obtainInterface.Subscribe(this);
    }

    public void PassData(float number)
    {
        Bar.fillAmount = number / _maxLife;
    }
}
