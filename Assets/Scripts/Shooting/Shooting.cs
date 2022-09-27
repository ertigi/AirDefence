using System.Collections;
using UnityEngine;

public class Shooting {
    private BulletController _bulletController;
    private CameraController _cameraController;
    private MonoBehaviour _coroutineRunner;
    private Turel _turel;
    private Camera _camera;

    private Vector3 _targetPosition;

    private float _shootDelay;
    private int _barrelAmount, _currentBarrelIndex;
    private bool _isShooting;

    public Shooting(GameSettings gameSettings, LevelData levelData, BulletController bulletController, CameraController cameraController, MonoBehaviour monoBehaviour) {
        _turel = levelData.Turel;
        _camera = levelData.Camera;
        _bulletController = bulletController;
        _cameraController = cameraController;
        _coroutineRunner = monoBehaviour;

        _shootDelay = 1f / gameSettings.FireRate;
        _barrelAmount = _turel.Barrels.Count;

        _coroutineRunner.StartCoroutine(ShootingRoutine());
    }

    public void GameUpdate() {
        _isShooting = CheckAirplaneInAim();
        _cameraController.EnableZoom(_isShooting);
    }

    private bool CheckAirplaneInAim() {
        Ray ray = _camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if(Physics.SphereCast(ray, .1f, out hit)) {
            _targetPosition = hit.point;
            return true;
        } else {
            return false;
        }
    }

    private void Shoot() {
        Debug.Log("TrySoot");
        ++_currentBarrelIndex;
        _currentBarrelIndex = _currentBarrelIndex < _barrelAmount ? _currentBarrelIndex : 0;

        _bulletController.PlayerShoot(_turel.Barrels[_currentBarrelIndex], _targetPosition);
        _turel.ShootAnim(_currentBarrelIndex, _shootDelay);
    }

    private IEnumerator ShootingRoutine() {
        while (true) {
            yield return new WaitForSeconds(_shootDelay);
            if (_isShooting) {
                Shoot();
            }
        }
    }
}
