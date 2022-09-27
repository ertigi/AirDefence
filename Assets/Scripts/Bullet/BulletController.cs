using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class BulletController {
    private EnemyController _enemyController;
    private GameSettings _gameSettings;

    private BulletFactory _bulletFactory;
    private List<Bullet> _playerBullets;
    private List<Bullet> _enemyBullets;
    private Transform _target;
    private Transform _playerBody;
    private float _bulletSpeed, _hitSqrRadius;

    public BulletController(AssetContainer assetContainer, GameSettings gameSettings, LevelData levelData) {
        _bulletFactory = new BulletFactory(assetContainer.Bullet, gameSettings);
        _gameSettings = gameSettings;

        _playerBody = levelData.Turel.Body;


        _bulletSpeed = gameSettings.BulletSpeed;
        _hitSqrRadius = gameSettings.HitSqrRadius;

        _playerBullets = new List<Bullet>();
        _enemyBullets = new List<Bullet>();
    }

    public void SetEnemyController(EnemyController enemyController) {
        _enemyController = enemyController;
    }

    public void PlayerShoot(Transform barrel, Vector3 targetPosition) {
        Bullet bullet = _bulletFactory.SpawnBullet(barrel);
        bullet.SetMoveDirection((targetPosition - barrel.position).normalized);
        _playerBullets.Add(bullet);
    }

    public void EnemyShoot(List<Transform> barrels) {
        foreach (var item in barrels) {
            Bullet bullet = _bulletFactory.SpawnBullet(item);
            bullet.SetMoveDirection((_playerBody.position - item.transform.position).normalized);
            _enemyBullets.Add(bullet);
        }
    }

    public void GameUpdate() {
        ReduceBulletsLifeTime();

        MoveBullets();

        if (_playerBullets.Count > 0)
            CheckHitOnEnemies();

        if (_enemyBullets.Count > 0)
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
            for (int j = 0; j < _enemyController.Airplanes.Count; j++) {
                if (Vector3.SqrMagnitude(_playerBullets[i].transform.position - _enemyController.Airplanes[j].transform.position) < _hitSqrRadius) {
                    _enemyController.Airplanes[j].Hit();
                    UnityEngine.Object.Destroy(_playerBullets[i].gameObject);
                    _playerBullets.RemoveAt(i);
                }
            }
        }
    }

    private void CheckHitOnPlayer() {
        for (int i = 0; i < _enemyBullets.Count; i++) {
            if (Vector3.SqrMagnitude(_enemyBullets[i].transform.position - _playerBody.position) < _hitSqrRadius) {
                UnityEngine.Object.Destroy(_enemyBullets[i].gameObject);
                _enemyBullets.RemoveAt(i);
            }
        }
    }
}