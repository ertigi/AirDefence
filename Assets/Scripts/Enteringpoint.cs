using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enteringpoint : MonoBehaviour {
    [SerializeField] private UIRoot _uIRoot;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AssetContainer _assetContainer;

    private InputService _inputService;
    private CameraController _cameraController;
    private TurelController _turelController;
    private BulletController _bulletController;
    private Shooting _shooting;
    private EnemyController _enemyController;
    private ParticleFactory _particleFactory;
    private SceneLoad _sceneLoad;

    private bool _isGame = false;

    public void StartGame() {
        _enemyController.StartGame();
        _shooting.StartGame();
        _isGame = true;
    }

    public void LoseGame() {
        _uIRoot.EnablePanel(UIPanelType.Lose);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
        StopGame();
    }

    public void WinGame() {
        _uIRoot.EnablePanel(UIPanelType.Win);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
        StopGame();
    }

    private void StopGame() {
        _uIRoot.EnablePanel(UIPanelType.Win);
        _isGame = false;
        StopAllCoroutines();
    }

    private void Start() {
        _sceneLoad = new SceneLoad();

        _levelData.Turel.Init();
        _uIRoot.Init(this, _sceneLoad);

        _particleFactory = new ParticleFactory(_assetContainer);

        _inputService = new InputService(_levelData.Camera);
        _cameraController = new CameraController(_gameSettings, _levelData, _inputService);
        _turelController = new TurelController(_levelData, _gameSettings, _uIRoot, this);
        _bulletController = new BulletController(_assetContainer, _gameSettings, _levelData, _particleFactory, _turelController, _uIRoot); ;
        _shooting = new Shooting(_gameSettings, _levelData, _bulletController, _cameraController, this);
        _enemyController = new EnemyController(_assetContainer, _gameSettings, _levelData, _bulletController, _particleFactory, this);
    }

    private void Update() {
        if(_isGame) {
            _inputService.GameUpdate();
            _uIRoot.GameUpdate();
            _cameraController.GameUpdate();
            _turelController.GameUpdate();
            _bulletController.GameUpdate();
            _shooting.GameUpdate();
        }
    }
}
