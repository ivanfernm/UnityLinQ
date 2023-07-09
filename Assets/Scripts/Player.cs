using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour,  IAttack, IObservable, IDamangeable
{
    #region old
    [Header("Player Stats")]
    [SerializeField] float _life = 100;
    [SerializeField] float _speed = 2f;
    [SerializeField] bool aturd = false;
    [SerializeField] List<AudioClip> _sfx;

    [Header("Debug Variables")]
    [SerializeField] private Controls virtualStick;
    [SerializeField] float _cooldown;

    Animator _animator;
    AudioSource audioSource;
    private Rigidbody myRigidbody;
    private Vector3 simulatedVector;

    List<IObserver> _allObservers = new List<IObserver>();
    #endregion

    Model _model;
    View _view;
    Controller _controller;

    public int atackCommand;
    public AccText _accText;

    #region old
    private void Awake()
    {
        if (virtualStick == null) { Debug.LogWarning("Player doesn have a stick"); }
        if (myRigidbody == null) { myRigidbody = GetComponent<Rigidbody>();}
        _animator = gameObject.GetComponent<Animator>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        _model = new Model(_view, transform, myRigidbody, _speed,virtualStick);
        _controller = new Controller(_model, simulatedVector, virtualStick);
        _view = new View(_animator);
        _view.OnAwake();
    }
    

    private void FixedUpdate()
    {
        _controller.FixedUpdate();
        animationsWalk();
    }
    private void Update()
    {
      
        Counter();
        if (_cooldown >= 1) { _cooldown = 1; }

        AttackPress();

        //mio
        _controller.OnUpdate();
        _view.OnUpdate();
        //mio
    }

    public virtual void animationsWalk()                            //tuvimos un problema al pasar este
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && aturd == false)
        {
             _view.OnWalk();
        }
        else if(aturd == true)
        {
             ExtraAction();
        }
        else
        {
            _view.OnAwake();
        }
    }
    void Counter()
    {
        if (_cooldown >= 1)
            _cooldown = 1;
        else
            _cooldown += Time.deltaTime;
    }
   
    #region AttackPress/Unpress
    public void AttackPress()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_cooldown == 1)
            {
                audioSource.clip = _sfx[1];
                PlaySFX();
                _view.OnAttack();
                atackCommand++;
                _accText.UpdateAccText();
            }
        }
    }

    public void AttackUnpress()
    {
       
        _view.OnUpdate();
    }

    #endregion
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
    #endregion
    #region IDamangeable
    public void CauseDamange(int damange)
    {
        audioSource.clip = _sfx[0];
        PlaySFX();
        NotifyToObservers(_life);
        _life = _life - damange;
        if(_life <= 0)
        {
            OnDead();
        }
    }

    public void OnDead()
    {
        if (_life <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }
    public void ExtraAction()
    {
        _view.OnAturd();
        aturd = true;
    }
    public void endAction()
    {
        aturd = false;
    }
    #endregion
    void PlaySFX()
    {
        audioSource.Play();
    }

    #endregion
}
