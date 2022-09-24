using UnityEngine;

[CreateAssetMenu(menuName = "_Scriptable Objects/Game Settings", fileName = "Game Settings")]
public class GameSettings : ScriptableObject {
    [Header("CAMERA")]
    public float CameraMovementSpeed = 5;
    public float CameraRotationSpeed = 7;
    public float MaxHorizontalRotation = 40;
    public float MaxVerticalRotation = 20;
    public float SwipeScale = 30;
    [Header("Turel")]
    public float TurelRotateSpeed = 5;
    public float FireRate = 10;
    public float BulletSpeed = 10;
    public float BulletLifeTime = 2;
    public float HitSqrRadius = .5f;
}
