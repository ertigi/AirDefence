using Lofelt.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelController {
    private Turel _turel;
    private LevelData _levelData;
    private GameSettings _gameSettings;
    private Enteringpoint _enteringpoint;
    private InGamePanel _inGamePanel;

    private Transform _rotationTarget;
    private float _rotateSpeed;
    private int _currentHealth, _maxHealth;
    
    public TurelController(LevelData levelData, GameSettings gameSettings, UIRoot uIRoot, Enteringpoint enteringpoint) {
        _turel = levelData.Turel;
        _rotationTarget = levelData.ContainerForTargetPoint;
        _gameSettings = gameSettings;
        _enteringpoint = enteringpoint;
        _maxHealth = gameSettings.MaxHealth;
        _currentHealth = gameSettings.MaxHealth;
        _inGamePanel = uIRoot.GetPanel(UIPanelType.InGame) as InGamePanel;

        _rotateSpeed = _gameSettings.TurelRotateSpeed;
    }

    public void GameUpdate() {
        RotationTurel();
    }

    public void Hit() {
        --_currentHealth;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
        if (_currentHealth <= 0) {
            _inGamePanel.UpdateHealthBar(0);
            _enteringpoint.LoseGame();
        } else {
            _inGamePanel.UpdateHealthBar((float)_currentHealth / _maxHealth);
        }
    }

    private void RotationTurel() {
        Quaternion bodyRotate = Quaternion.Euler(0, _rotationTarget.eulerAngles.y, 0); 
        Quaternion barrelsRotate = Quaternion.Euler(_rotationTarget.eulerAngles.x, 0, 0);

        _turel.Body.localRotation = Quaternion.Lerp(_turel.Body.localRotation, bodyRotate, Time.deltaTime * _rotateSpeed);
        _turel.BarrelContainer.localRotation = Quaternion.Lerp(_turel.BarrelContainer.localRotation, barrelsRotate, Time.deltaTime * _rotateSpeed);
    }

}
