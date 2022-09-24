using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enteringpoint : MonoBehaviour {
    [SerializeField] private LevelData _levelData;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AssetContainer _assetContainer;
    [SerializeField] private Transform _testEnemy;

    private InputService _inputService;
    private CameraController _cameraController;
    private TurelController _turelController;
    private BulletController _bulletController;
    private Shooting _shooting;

    private void Start() {
        _levelData.Turel.Init();

        _inputService = new InputService(_levelData.Camera);
        _cameraController = new CameraController(_gameSettings, _levelData, _inputService);
        _turelController = new TurelController(_levelData, _gameSettings);
        _bulletController = new BulletController(_assetContainer, _gameSettings, _testEnemy);
        _shooting = new Shooting(_gameSettings, _levelData, _bulletController, this);
    }

    private void Update() {
        _inputService.GameUpdate();
        _cameraController.GameUpdate();
        _turelController.GameUpdate();
        _bulletController.GameUpdate();
        _shooting.GameUpdate();
    }
}
