using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class Airplane : MonoBehaviour {
    public SplineFollower Follower;
    public List<Transform> Barrels;
    public bool IsShooting;

    [SerializeField] private List<GameObject> _trails = new List<GameObject>();

    private int _health = 5;

    private EnemyController _enemyController;
    private AirplanePath _airplanePath;
    private Camera _camera;

    public void Init(EnemyController enemyController, AirplanePath airplanePath, Camera camera) {
        _enemyController = enemyController;
        _airplanePath = airplanePath;
        _camera = camera;

        Follower.spline = airplanePath.SplineComputer;
        Follower.follow = true;

        StartCoroutine(TrailsEnable());
    }

    public void Hit() {
        --_health;
        if(_health <= 0) {
            _enemyController.DestroyAirplane(this, _airplanePath);
        }
    }

    private IEnumerator TrailsEnable() {
        yield return null;
        _trails.ForEach((item) => item.SetActive(true));
    }
}
