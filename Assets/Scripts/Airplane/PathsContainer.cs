using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PathsContainer : MonoBehaviour {
    [SerializeField] private List<AirplanePath> _airplanePaths = new List<AirplanePath>();
     
    public AirplanePath GetRandomEmptyPath() {
        AirplanePath airplanePath = null;
        int rand;

        do {
            rand = UnityEngine.Random.Range(0, _airplanePaths.Count);
        } while (_airplanePaths[rand].IsHaveFollower);

        return _airplanePaths[rand];
    }

    public void ClearPathAction(AirplanePath airplanePath) {
        airplanePath.RemoveAction(); 
    }

    public bool CheckForEmptyPaths() {
        foreach (var item in _airplanePaths) {
            if (!item.IsHaveFollower) {
                return true;
            }
        }
        return false;
    }
}
