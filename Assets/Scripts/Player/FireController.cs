using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullet;
using static FirePattern;

public class FireController : MonoBehaviour
{
    [SerializeField] Bullet _bullectPrefab;
    [SerializeField] Transform _firePosition;
    [SerializeField] Transform _fireUpPosition;
    [SerializeField] GameObject _bomb;
    [SerializeField] Transform _bombPosition;
    [SerializeField] int _bulletPoolInitialCount = 10;

    public int poolCount = 0;

    AudioSource _audioSource;
    ObjectPool<Bullet> _bulletPool;
    PlayerAnimation _playerAnimation;
    Transform _whereToFire;
    FirePattern _pattern;

    AbilitiesController _abilitiesController;
    private bool _canFire = true;

    public bool _isStanding { get; set; } = true;

    private Direction _direction = Direction.right;

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _abilitiesController = GetComponent<AbilitiesController>();
        _whereToFire = _firePosition;
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, _bullectPrefab, _firePosition, _bulletPoolInitialCount);
        _audioSource = GetComponent<AudioSource>();
    }

    //private void OnEnable()
    //{
    //    _bulletPool = new ObjectPool<Bullet>(CreateBullet, _bullectPrefab, _firePosition, _bulletPoolInitialCount);
    //}
    private void OnLevelWasLoaded()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, _bullectPrefab, _firePosition, _bulletPoolInitialCount);
    }


    private void OnDisable()
    {
        _bulletPool.ClearPool();
    }

    private void Update()
    {
        if (!_canFire) return;
        poolCount = _bulletPool.GetCount();
        DecideFirePosition();
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (_isStanding)
            {
                Shoot();
            }
            else if(_abilitiesController.canDropBomb)
            {
                Instantiate(_bomb, _bombPosition.position, _bombPosition.rotation);
            }
        }
    }

    private void Shoot()
    {
        _audioSource.Play();
        if (_pattern.isIntermittent)
        {
            Bullet bullet = _bulletPool.Dequeue();
            FirePatternDirection firePatternDirection = _pattern.GetNextDirection();
            Direction direction = ConvertFirePatternDirectionToBulletDirection(firePatternDirection);
            bullet.SetDirection(direction);
            bullet.transform.position = _whereToFire.position;
            if (bullet.GetObjectPool() == null)
            {
                bullet.SetObjectPool(_bulletPool);
            }
        }
        else
        {
            FirePatternDirection[] firePatternDirections = _pattern.directions;
            foreach(FirePatternDirection pattern in firePatternDirections)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.SetDirection(ConvertFirePatternDirectionToBulletDirection(pattern));
                bullet.transform.position = _whereToFire.position;
                if (bullet.GetObjectPool() == null)
                {
                    bullet.SetObjectPool(_bulletPool);
                }

            }

        }
        if (Input.GetKey(KeyCode.W))
        {
            _playerAnimation.ShootUp();
        }
        else
        {
            _playerAnimation.Shoot();
        }
    }

    private void DecideFirePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _whereToFire = _fireUpPosition;
        }
        else
        {
            _whereToFire = _firePosition;
        }
    }

    private Bullet CreateBullet(Bullet gameObject, Transform transform)
    {
        Bullet bullet = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
        bullet.gameObject.SetActive(false);
        bullet.SetObjectPool(_bulletPool);
        //DontDestroyOnLoad(bullet);
        return bullet;
    }

    public void DisableFire()
    {
        _canFire = false;

    }

    public void EnableFire()
    {
        _canFire = true;

    }

    public Direction ConvertFirePatternDirectionToBulletDirection(FirePatternDirection patternDirection)
    {
        switch (patternDirection)
        {
            case FirePatternDirection.back:
                if (_direction == Direction.left)
                    return Direction.right;
                else if (_direction == Direction.right)
                    return Direction.left;
                else
                    return Direction.up;
                break;
            case FirePatternDirection.front:
                return _direction;
                break;
            case FirePatternDirection.Up:
                return Direction.up;
                break;
            case FirePatternDirection.Down:
                return Direction.down;
                break;
        }
        return _direction;
    }

    public void SetFirePattern(FirePattern pattern)
    {
        _pattern = pattern;
    }
}
