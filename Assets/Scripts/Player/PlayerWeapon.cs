using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWeaponData _weaponData;
    [SerializeField] private ArmorPiercingProjectilePool _armorPiercingProjectilePool;
    [SerializeField] private HighExplosiveProjectilePool _highExplosiveProjectilePool;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private PlayerInputHolder _playerInputHolder;

    private Projectile _projectile;
    private BaseProjectilePool _activePool;

    private bool _canShoot = true;

    private void Start()
    {
        _activePool = _armorPiercingProjectilePool;
    }

    private void OnEnable()
    {
        _playerInputHolder.OnLeftMouseButtonClicked += Shoot;
        _playerInputHolder.OnFirstProjectileTypeSelected += ChooseFirstProjectileType;
        _playerInputHolder.OnSecondProjectileTypeSelected += ChooseSecondProjectileType;
    }

    private void OnDisable()
    {
        _playerInputHolder.OnLeftMouseButtonClicked -= Shoot;
        _playerInputHolder.OnFirstProjectileTypeSelected -= ChooseFirstProjectileType;
        _playerInputHolder.OnSecondProjectileTypeSelected -= ChooseSecondProjectileType;
    }

    private void Shoot()
    {
        if (!_canShoot)
            return;

        _projectile = _activePool.Pool.TakeFromPool();
        _projectile.gameObject.layer = Constants.PLAYER_PROJECTILE_LAYER;
        _projectile.transform.position = _bulletSpawn.position;
        _projectile.transform.rotation = _bulletSpawn.rotation;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_weaponData.ReloadTime);
        _canShoot = true;
    }

    private void ChooseFirstProjectileType()
    {
        _activePool = _armorPiercingProjectilePool;
    }

    private void ChooseSecondProjectileType()
    {
        _activePool = _highExplosiveProjectilePool;
    }
}
