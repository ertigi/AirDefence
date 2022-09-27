using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController {
    public List<Airplane> Airplanes { get; private set; }

    private AirplaneFactory _airplaneFactory;
    private PathsContainer _pathsController;
    private BulletController _bulletController;
    private MonoBehaviour _coroutineRunner;
    private Camera _camera;

    public EnemyController (AssetContainer assetContainer, LevelData levelData, BulletController bulletController,MonoBehaviour monoBehaviour) {
        _airplaneFactory = new AirplaneFactory(assetContainer);
        _pathsController = levelData.PathsContainer;
        _camera = levelData.Camera;

        _bulletController = bulletController;
        _coroutineRunner = monoBehaviour;

        _bulletController.SetEnemyController(this);

        Airplanes = new List<Airplane>();

        _coroutineRunner.StartCoroutine(SpawnRoutine());
        _coroutineRunner.StartCoroutine(ShootingRoutine());
    }

    public void GameUpdate() {
        for (int i = 0; i < Airplanes.Count; i++) {
            Airplanes[i].GameUpdate();
        }
    }

    public void DestroyAirplane(Airplane airplane, AirplanePath airplanePath) {
        _pathsController.ClearPathAction(airplanePath);
        Airplanes.Remove(airplane);
        Object.Destroy(airplane.gameObject);
    }

    public void PathTriggerAction(Airplane airplane) {
        airplane.IsShooting = !airplane.IsShooting;
    }

    private IEnumerator SpawnRoutine() {
        while (true) {
            //try spawn airplane if have empty path
            Debug.Log("CheckForEmptyPaths");
            if (_pathsController.CheckForEmptyPaths()) {
                Debug.Log("spawn new airplane");
                Airplane airplane = _airplaneFactory.SpawnAirplane();
                AirplanePath airplanePath = _pathsController.GetEmptyPath();

                airplane.Init(this, airplanePath, _camera);
                airplanePath.Init(() => PathTriggerAction(airplane));

                Airplanes.Add(airplane);
            }

            yield return new WaitForSeconds(1f);
        }
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
