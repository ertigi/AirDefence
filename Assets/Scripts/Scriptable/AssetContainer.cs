using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "_Scriptable Objects/Asset Container", fileName = "Asset Container")]
public class AssetContainer : ScriptableObject {
    public Bullet Bullet;
    public Airplane Airplane;
    public ParticleSystem Impact;
    public ParticleSystem Explosion;
}