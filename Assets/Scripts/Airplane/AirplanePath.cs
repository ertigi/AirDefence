using UnityEngine;
using Dreamteck.Splines;
using System;

public class AirplanePath : MonoBehaviour{
    public SplineComputer SplineComputer;
    public bool IsHaveFollower { get; private set; }

    private Action _triggerAction;

    public void Init(Action action) {
        _triggerAction = action;
        IsHaveFollower = true;
    }

    public void RemoveAction() {
        _triggerAction = null;
        IsHaveFollower = false;
    }

    public void TriggerAction() {
        Debug.Log("airplane shoot");
        _triggerAction.Invoke();
    }
}