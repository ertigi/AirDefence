using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController {
    public List<Airplane> Airplanes { get; private set; }

    private AirplaneFactory _airplaneFactory;
    private PathsContainer _pathsController;
    private BulletController _bulletController;
    private ParticleFactory _particleFactory;
    private Enteringpoint _enteringpoint;

    private Camera _camera;

    private int _airplansAmountInGame;
    private bool _isCanSpawnNewAirplanes;

    public EnemyController (AssetContainer assetContainer, GameSettings gameSettings, LevelData levelData, BulletController bulletController, ParticleFactory particleFactory, Enteringpoint enteringpoint) {
        _airplaneFactory = new AirplaneFactory(assetContainer);
        _airplansAmountInGame = gameSettings.AirplaneAmount;
        _pathsController = levelData.PathsContainer;
        _camera = levelData.Camera;

        _bulletController = bulletController;
        _particleFactory = particleFactory;
        _enteringpoint = enteringpoint;

        _bulletController.SetEnemyController(this);

        Airplanes = new List<Airplane>();
    }

    public void StartGame() {
        _isCanSpawnNewAirplanes = true;

        _enteringpoint.StartCoroutine(SpawnRoutine());
        _enteringpoint.StartCoroutine(ShootingRoutine());
    }

    public void DestroyAirplane(Airplane airplane, AirplanePath airplanePath) {
        _pathsController.ClearPathAction(airplanePath);
        _particleFactory.SpawnParticle(ParticleType.Explosion, airplane.transform.position);
        Airplanes.Remove(airplane);
        Object.Destroy(airplane.gameObject);
    }

    public void PathTriggerAction(Airplane airplane) {
        airplane.IsShooting = !airplane.IsShooting;
    }

    private IEnumerator SpawnRoutine() {
        while (_isCanSpawnNewAirplanes) {
            //try spawn airplane if have empty path
            Debug.Log("CheckForEmptyPaths");
            if (_pathsController.CheckForEmptyPaths()) {
                Debug.Log("spawn new airplane");
                AirplanePath airplanePath = _pathsController.GetRandomEmptyPath();
                Airplane airplane = _airplaneFactory.SpawnAirplane(airplanePath.SplineComputer.GetPoint(0).position);

                airplane.Init(this, airplanePath, _camera);
                airplanePath.Init(() => PathTriggerAction(airplane));

                Airplanes.Add(airplane);
                --_airplansAmountInGame;
                if (_airplansAmountInGame <= 0) {
                    _isCanSpawnNewAirplanes = false;
                    _enteringpoint.StartCoroutine(WaitEndGame());
                }
            }

            yield return new WaitForSeconds(2.5f);
        }
    }

    private IEnumerator WaitEndGame() {
        yield return new WaitWhile(() => Airplanes.Count > 0);
        _enteringpoint.WinGame();
    }

    private IEnumerator ShootingRoutine() {
        while (true) {
            if (Airplanes.Count > 0) {
                foreach (var item in Airplanes) {
                    if (item.IsShooting) {
                        _bulletController.EnemyShoot(item.Barrels);
                    }
                }
            }

            yield return new WaitForSeconds(.5f);
        }
    }
}
