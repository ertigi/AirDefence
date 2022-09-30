using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService {
    public event Action OnSwipeStart;
    public event Action OnSwipeBreak;
    public event Action<Vector3> OnSwipe;

    private Camera _camera;

    private Vector3 _startTouchPosition;
    private Vector3 _swipeDirection;
    private bool _isMobile;

    public InputService(Camera camera) {
        _isMobile = Application.isMobilePlatform;
        _camera = camera;
    }

    public void GameUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            StartTouch();
        } else if (Input.GetMouseButton(0)) {
            Touch();
        } else if (Input.GetMouseButtonUp(0)) {
            BreakTouch();
        }
    }
    public void BreakTouch() {
        _startTouchPosition = Vector3.zero;
        OnSwipeBreak?.Invoke();
    }

    private void Touch() {
        _swipeDirection = _camera.ScreenToViewportPoint(Input.mousePosition) - _startTouchPosition;
        OnSwipe?.Invoke(_swipeDirection);
    }

    private void StartTouch() {
        _startTouchPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        OnSwipeStart?.Invoke();
    }

}
