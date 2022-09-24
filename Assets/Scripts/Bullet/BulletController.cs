using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BulletController {
    private BulletFactory _bulletFactory;
    private List<Bullet> _playerBullets;
    private List<Bullet> _enemyBullets;

    private Transform _target;
    private float _bulletSpeed, _hitSqrRadius;

    public BulletController (AssetContainer assetContainer, GameSettings gameSettings, Transform target) {
        _bulletFactory = new BulletFactory(assetContainer.Bullet, gameSettings);
        _target = target;

        _bulletSpeed = gameSettings.BulletSpeed;
        _hitSqrRadius = gameSettings.HitSqrRadius;

        _playerBullets = new List<Bullet>();
        _enemyBullets = new List<Bullet>();
    }

    public void SpawnPlayerBullet(Transform barrel) {
        _playerBullets.Add(_bulletFactory.SpawnBullet(barrel));
    }

    public void GameUpdate() {
        ReduceBulletsLifeTime();

        MoveBullets();

        if(_playerBullets.Count > 0)
            CheckHitOnEnemies();

        if(_enemyBullets.Count > 0)
            CheckHitOnPlayer();
    }

    private void ReduceBulletsLifeTime() {
        float deltaTime = Time.deltaTime;

        if (_playerBullets.Count > 0)
            for (int i = 0; i < _playerBullets.Count; i++) {
                if (_playerBullets[i].ReduceLifeTime(deltaTime)) {
                    UnityEngine.Object.Destroy(_playerBullets[i].gameObject);
                    _playerBullets.RemoveAt(i);
                }
            }

        if (_enemyBullets.Count > 0)
            for (int i = 0; i < _enemyBullets.Count; i++) {
                if (_enemyBullets[i].ReduceLifeTime(deltaTime)) {
                    UnityEngine.Object.Destroy(_enemyBullets[i].gameObject);
                    _enemyBullets.RemoveAt(i);
                }
            }
    }

    private void MoveBullets() {
        if (_playerBullets.Count > 0)
            foreach (var item in _playerBullets)
                Move(item);

        if (_enemyBullets.Count > 0)
            foreach (var item in _enemyBullets)
                Move(item);
    }

    private void Move(Bullet bullet) => 
        bullet.transform.position += bullet.MoveDirectionNormalized * (_bulletSpeed * Time.deltaTime);

    private void CheckHitOnEnemies() {
        for (int i = 0; i < _playerBullets.Count; i++) {
            if (Vector3.SqrMagnitude(_playerBullets[i].transform.position - _target.position) < _hitSqrRadius) {
                UnityEngine.Object.Destroy(_playerBullets[i].gameObject);
                _playerBullets.RemoveAt(i);
            }
        }
    }

    private void CheckHitOnPlayer() {

    }
}