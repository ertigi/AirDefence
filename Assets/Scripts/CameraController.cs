using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController {
    private Camera _camera;
    private Transform _targetPoint, _container;
    private Tween _zoomTween;
    private Vector3 _startRotation;
    private Vector3 _defaultRotationEuler;
    private float _offsetScale, _rotateLerpSpeed, _moveLerpSpeed, _maxRotateX, _maxRotateY;
    private float _defaultFieldOfView;
    private bool _isZoom;

    public CameraController(GameSettings gameSettings, LevelData levelData, InputService inputService) {
        _container = levelData.ContainerForTargetPoint;
        _targetPoint = levelData.TargetPointForCamera;
        _camera = levelData.Camera;

        _maxRotateX = gameSettings.MaxVerticalRotation;
        _maxRotateY = gameSettings.MaxHorizontalRotation;
        _offsetScale = gameSettings.SwipeScale;
        _rotateLerpSpeed = gameSettings.CameraRotationSpeed;
        _moveLerpSpeed = gameSettings.CameraMovementSpeed;

        _defaultFieldOfView = _camera.fieldOfView;

        _defaultRotationEuler = _container.eulerAngles;
        _camera.transform.position = _targetPoint.position;
        _camera.transform.rotation = _targetPoint.rotation;

        inputService.OnSwipeStart += KeepStartRotate;
        inputService.OnSwipe += RotateContainer;
    }

    public void GameUpdate() {
        MoveCamera();
        RotateCamera();
    }

    public void EnableZoom(bool value) {
        if(_isZoom != value) {
            _isZoom = value;
            Zoom(value);
        }
    }

    private void Zoom(bool value) {
        if (_zoomTween != null)
            _zoomTween.Kill();

        float newFieldOfView = value ? _defaultFieldOfView / 1.5f : _defaultFieldOfView;
        _zoomTween = _camera.DOFieldOfView(newFieldOfView, .5f);
    }

    private void RotateContainer(Vector3 direction) {
        Vector3 rotationOffset = _startRotation + new Vector3(direction.y * -_offsetScale, direction.x * _offsetScale, 0);

        rotationOffset.x = Mathf.Clamp(rotationOffset.x, _defaultRotationEuler.x - _maxRotateX, _defaultRotationEuler.x + _maxRotateX);
        rotationOffset.y = Mathf.Clamp(rotationOffset.y, _defaultRotationEuler.y - _maxRotateY, _defaultRotationEuler.y + _maxRotateY);
        rotationOffset.z = 0;

        _container.rotation = Quaternion.Euler(rotationOffset);
    }

    private void KeepStartRotate() {
        _startRotation = _container.eulerAngles;

        _startRotation.x = _startRotation.x < 180 ? _startRotation.x : _startRotation.x - 360;
        _startRotation.y = _startRotation.y < 180 ? _startRotation.y : _startRotation.y - 360;
    }

    private void RotateCamera() {
        Quaternion targetRotation = Quaternion.Lerp(_camera.transform.rotation, _targetPoint.rotation, Time.deltaTime * _rotateLerpSpeed);

        _camera.transform.rotation = targetRotation;
    }

    private void MoveCamera() {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPoint.position, Time.deltaTime * _moveLerpSpeed);
    }
}
