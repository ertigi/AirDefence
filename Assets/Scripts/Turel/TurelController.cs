using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelController {
    private Turel _turel;
    private LevelData _levelData;
    private GameSettings _gameSettings;

    private Transform _rotationTarget;
    private float _rotateSpeed;

    public TurelController(LevelData levelData, GameSettings gameSettings) {
        _turel = levelData.Turel;
        _rotationTarget = levelData.ContainerForTargetPoint;
        _gameSettings = gameSettings;

        _rotateSpeed = _gameSettings.TurelRotateSpeed;
    }

    public void GameUpdate() {
        RotationTurel();
    }

    private void RotationTurel() {
        Quaternion bodyRotate = Quaternion.Euler(0, _rotationTarget.eulerAngles.y, 0); 
        Quaternion barrelsRotate = Quaternion.Euler(_rotationTarget.eulerAngles.x, 0, 0);

        _turel.Body.localRotation = Quaternion.Lerp(_turel.Body.localRotation, bodyRotate, Time.deltaTime * _rotateSpeed);
        _turel.BarrelContainer.localRotation = Quaternion.Lerp(_turel.BarrelContainer.localRotation, barrelsRotate, Time.deltaTime * _rotateSpeed);
    }
}
