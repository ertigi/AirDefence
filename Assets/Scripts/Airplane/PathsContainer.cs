using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PathsContainer : MonoBehaviour {
    [SerializeField] private List<AirplanePath> _airplanePaths = new List<AirplanePath>();
     
    public void Init() {

    }

    public AirplanePath GetEmptyPath() {
        AirplanePath airplanePath = null;
        foreach (var item in _airplanePaths) {
            if (!item.IsHaveFollower) {
                airplanePath = item;
                break;
            }
        }
        return airplanePath;
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
