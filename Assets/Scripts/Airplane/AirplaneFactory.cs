using UnityEngine;

public class AirplaneFactory {
    private Airplane _airplanePrefab;

    public AirplaneFactory(AssetContainer assetContainer) {
        _airplanePrefab = assetContainer.Airplane;
    }

    public Airplane SpawnAirplane() {
        Airplane newAirplane =  Object.Instantiate(_airplanePrefab);

        return newAirplane;
    }
}