using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 MoveDirectionNormalized { get; private set; }

    private float LifeTime;

    public void Init(float lifeTime) {
        LifeTime = lifeTime;
    }

    public void SetMoveDirection(Vector3 direction) {
        MoveDirectionNormalized = direction;
    }

    public bool ReduceLifeTime(float time) {
        LifeTime -= time;
        return LifeTime <= 0;
    }
}