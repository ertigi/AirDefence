using UnityEngine;

public class BulletFactory {
    private Bullet _bullet;
    private GameSettings _gameSettings;

    public BulletFactory(Bullet bullet, GameSettings gameSettings) {
        _bullet = bullet;
        _gameSettings = gameSettings;
    }

    public Bullet SpawnBullet(Transform barrel) {
        Bullet bullet = Object.Instantiate(_bullet, barrel.position, barrel.rotation);
        bullet.Init(_gameSettings.BulletLifeTime);
        return bullet;
    }
}