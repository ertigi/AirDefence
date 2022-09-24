using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 MoveDirectionNormalized { get; private set; }

    private float LifeTime;

    public void Init(Vector3 direction, float lifeTime) {
        MoveDirectionNormalized = direction;
        LifeTime = lifeTime;
    }

    public bool ReduceLifeTime(float time) {
        LifeTime -= time;
        return LifeTime <= 0;
    }
}