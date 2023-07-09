using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IA2;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IAnimator, IDamangeable, IDifficulty
{
    public event Action<Enemy> UpdateDelegate = delegate { };

    float _originalLife;
    enum EnemyInputs { MOVETO, ATTACK_PLAYER, ATTACK_CHEST, DIE }
    Vector3 _desired;
    EventFSM<EnemyInputs> _myFSM;
    public EnemyType _EnemyType;
    public int Hitcount; 
    enum AttackDesition { PLAYER, CHEST };
    AttackDesition _ToAttack;
    [Header("Enemy Stats")]
    public float _life;
    [SerializeField] float _speed;
    AudioSource _myAudioSource;
    Action FollowTo;
    Action AnimatorState;
    Animator _animator;
    Rigidbody myRigidBody;
    Vector3 desired;
    Vector3 _spawnPoint;
    Vector3 destiny;
    float _originalSpeed;
    Renderer _rd;

    [Header("Debug Variables")]
    [SerializeField] protected float _destinyRatious = 1.4f;
    [SerializeField] protected int _changeBehaviourLife = 30;

    [SerializeField] protected Transform _chest;
    [SerializeField] protected Transform _player;
    [SerializeField] protected EnemySpawner myEnemySpawner;

    [Header("Sound Effects")]
    [SerializeField] List<AudioClip> _sfx = new List<AudioClip>();
    int random;

    private void Awake()
    {
        AnimatorState = Walking;

        _originalSpeed = _speed;
        _animator = gameObject.GetComponent<Animator>();
        if (myRigidBody = null) { myRigidBody = GetComponent<Rigidbody>(); }
        if (_myAudioSource == null) _myAudioSource = GetComponent<AudioSource>();
        //IA2-P3
        #region IA2-P3
        var move = new State<EnemyInputs>("MOVE");
        var attackplayer = new State<EnemyInputs>("ATTACK_PLAYER");
        var attackchest = new State<EnemyInputs>("ATTACK_CHEST");
        var die = new State<EnemyInputs>("DIE");
        StateConfigurer.Create(move)
            .SetTransition(EnemyInputs.ATTACK_PLAYER, attackplayer)
            .SetTransition(EnemyInputs.ATTACK_CHEST, attackchest)
            .SetTransition(EnemyInputs.DIE, die)
            .Done();
        StateConfigurer.Create(attackplayer)
             .SetTransition(EnemyInputs.ATTACK_CHEST, attackchest)
             .SetTransition(EnemyInputs.MOVETO, move)
             .SetTransition(EnemyInputs.DIE, die)
             .Done();
        StateConfigurer.Create(attackchest)
            .SetTransition(EnemyInputs.ATTACK_PLAYER, attackplayer)
            .SetTransition(EnemyInputs.MOVETO, move)
            .SetTransition(EnemyInputs.DIE, die)
            .Done();
        StateConfigurer.Create(die)
            .SetTransition(EnemyInputs.MOVETO, move)
            .Done();

        move.OnEnter += x =>
        {
            random = UnityEngine.Random.Range(0, 101);
            if (random >= 70)
            {
                if (_chest != null) destiny = _chest.transform.position;
                _ToAttack = AttackDesition.CHEST;
            }
            else
            {
                if(_player != null) destiny = _player.transform.position;
                _ToAttack = AttackDesition.PLAYER;
            }
            Debug.Log(gameObject.name + "Move");
            _speed = _originalSpeed;

        };
        move.OnUpdate += () =>
        {
            if (_ToAttack == AttackDesition.CHEST)
            {
                if (_chest != null) destiny = _chest.transform.position;
            }
            else
            {
                if (_player != null) destiny = _player.transform.position;
            }
            if(_animator != null) _animator.SetInteger("AnimationState", 0);

            desired = destiny - transform.position;
            transform.forward = desired.normalized;

            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            transform.position += desired * _speed * Time.deltaTime;

            if (desired.magnitude <= _destinyRatious && _ToAttack == AttackDesition.PLAYER)
            {
                _myFSM.SendInput(EnemyInputs.ATTACK_PLAYER);
            }
            else if (desired.magnitude <= _destinyRatious && _ToAttack == AttackDesition.CHEST)
            {
                _myFSM.SendInput(EnemyInputs.ATTACK_CHEST);
            }
        };

        attackplayer.OnEnter += x =>
        {
            _speed = 0;
            Debug.Log(gameObject.name + " APlayer");
        };
        attackplayer.OnUpdate += () =>
        {
            if (_ToAttack == AttackDesition.CHEST)
            {
                destiny = _chest.transform.position;
            }
            else
            {
                destiny = _player.transform.position;
            }


            desired = destiny - transform.position;
            if (desired.magnitude >= _destinyRatious)
            {
                _myFSM.SendInput(EnemyInputs.MOVETO);
            }
            else if (desired.magnitude <= _destinyRatious)
            {
                _animator.SetInteger("AnimationState", 1);
            }
        };

        attackchest.OnEnter += x =>
        {
            _speed = 0;
            Debug.Log(gameObject.name + " AChest");
        };
        attackchest.OnUpdate += () =>
        {
            desired = destiny - transform.position;
            if (desired.magnitude >= _destinyRatious)
            {
                _myFSM.SendInput(EnemyInputs.MOVETO);
            }
            else if (desired.magnitude <= _destinyRatious)
            {
                _animator.SetInteger("AnimationState", 1);
            }
        };

        die.OnEnter += x =>
        {
            CoinsPool.instance.Set(this.transform.position);
            _myAudioSource.clip = _sfx[0];
            PlayAudio();
            ExplotionPool.instance.Set(this.transform.position);
            myEnemySpawner.pool.ReturnObject(this);
            _life = _originalLife;

            _myFSM.SendInput(EnemyInputs.MOVETO);

        };

        _myFSM = new EventFSM<EnemyInputs>(move);
#endregion
    }
    private void Start()
    {
        EventManager.Subscribe(EventManager.EventType.Easy, Easy);
        EventManager.Subscribe(EventManager.EventType.Normal, Normal);
        EventManager.Subscribe(EventManager.EventType.Hard, Hard);
        _rd = gameObject.GetComponentInChildren<Renderer>();
        Hitcount = 0;
        //SetColor(Random.Range(0,3));
    }

    //IA2-P3
    private void SendInputToFSM(EnemyInputs inp)
    {
        _myFSM.SendInput(inp);
    }
    private void Update()
    {

        Debug.Log("JointAngleLimits2D");
        UpdateDelegate(this);
        //IA2-P3
        _myFSM.Update();
        if (_life <= 0) _myFSM.SendInput(EnemyInputs.DIE);//IA2-P3
    }

    public static void TurnOn(Enemy e)
    {
        e.gameObject.SetActive(true);
        //Test();
    }
    public static void TurnOff(Enemy e)
    {
        e.gameObject.SetActive(false);
    }
    public void Asign(EnemySpawner spawner)
    {
        myEnemySpawner = spawner;

    }
    

    #region Animations
    public void Idle()
    {

    }

    public void Attack()
    {
        _animator.SetInteger("AnimationState", 1);
    }

    public void Walking()
    {
        _animator.SetInteger("AnimationState", 0);
    }

    public void Dead()
    {

    }

    public void ReceivesDamage()
    {

    }
    #endregion

    #region Damange
    public void CauseDamange(int damange)
    {
        _myAudioSource.clip = _sfx[1];
        PlayAudio();
        _life = _life - damange;
        Hitcount++;
    }

    public void OnDead()
    {
        CoinsPool.instance.Set(this.transform.position);
        _myAudioSource.clip = _sfx[0];
        PlayAudio();
        ExplotionPool.instance.Set(this.transform.position);
        Hitcount = 0;
        myEnemySpawner.pool.ReturnObject(this);
        _life = _originalLife;
        Crowm.SetActive(false);
    }
    void PlayAudio()
    {
        _myAudioSource.Play();
    }
    #endregion

    public void Easy(params object[] parameters)
    {
        _life = 30;
        _originalLife = _life;
        _speed = 0.3f;
        EventManager.UnSubscribe(EventManager.EventType.Easy, Easy);
    }

    public void Normal(params object[] parameters)
    {
        _life = 50;
        _originalLife = _life;
        _speed = 0.3f;
        EventManager.UnSubscribe(EventManager.EventType.Normal, Normal);
    }

    public void Hard(params object[] parameters)
    {
        _life = 90;
        _originalLife = _life;
        _speed = 0.3f;
        EventManager.UnSubscribe(EventManager.EventType.Hard, Hard);
    }

    public void ExtraAction()
    {
        throw new NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _destinyRatious);
    }

    public void AscendToLeader()
    {
        transform.localScale *= 2;
        _life *= 2;
    }

    public void SetColor(int a)
    {
        switch (a)
        {
            case 0:
                _rd.material.color = Color.blue;
                _EnemyType = EnemyType.blue;
                break;
            case 1:
                _rd.material.color = Color.green;
                _EnemyType = EnemyType.green;
                break;
            case 2:
                _rd.material.color = Color.red;
                _EnemyType = EnemyType.red;
                break;

        }

    }

    public GameObject Crowm;
    public void SetPositionCrown(int Position)
    {
        Crowm.SetActive(true);
        
        switch (Position)
        {
            case 1:
                Crowm.GetComponent<Renderer>().material.color = Color.grey;
                break;
            case 2:
                Crowm.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 0:
                Crowm.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }
    
    public enum EnemyType
    {
        red = 0,
        blue = 1,
        green = 2,
    }
}
