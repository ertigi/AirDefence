using UnityEngine;

public class AirplaneFactory {
    private Airplane _airplanePrefab;

    public AirplaneFactory(AssetContainer assetContainer) {
        _airplanePrefab = assetContainer.Airplane;
    }

    public Airplane SpawnAirplane(Vector3 spawnPosition) {
        Airplane newAirplane =  Object.Instantiate(_airplanePrefab, spawnPosition, Quaternion.identity);

        return newAirplane;
    }
}