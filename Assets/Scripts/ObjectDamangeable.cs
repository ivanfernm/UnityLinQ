using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDamangeable : MonoBehaviour, IDamangeable, IObservable
{
    [SerializeField] float _life = 1500;
    List<IObserver> _allObservers = new List<IObserver>();
    public void CauseDamange(int damange)
    {
        _life = _life - damange;
        NotifyToObservers(_life);
        if (_life <= 0)
        {
            OnDead();
        }
    }


    public void OnDead()
    {
        SceneManager.LoadScene("Lose");
        // UNDER CONSTRUCTION
    }

    #region IObservable
    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(float number)
    {
        for (int i = 0; i < _allObservers.Count; i++)
            _allObservers[i].PassData(number);
    }

    public void ExtraAction()
    {
        
    }
    #endregion

}
