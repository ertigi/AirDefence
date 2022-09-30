using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory {
    private ParticleSystem _impactParticlePrefab;
    private ParticleSystem _explosionParticlePrefab;
     
    public ParticleFactory(AssetContainer assetContainer) {
        _impactParticlePrefab = assetContainer.Impact;
        _explosionParticlePrefab = assetContainer.Explosion;
    }

    public void SpawnParticle(ParticleType type, Vector3 worldPosition) {
        if (type == ParticleType.Impact)
            Instantiate(_impactParticlePrefab, worldPosition);
        else if (type == ParticleType.Explosion)
            Instantiate(_explosionParticlePrefab, worldPosition);
    }

    private void Instantiate(ParticleSystem particle, Vector3 position) {
        Object.Instantiate(particle, position, Quaternion.identity);
    }
}

public enum ParticleType {
    Impact, Explosion
}