using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private ArmorPiercingProjectilePool _armorPiercingProjectilePool;
    [SerializeField] private HighExplosiveProjectilePool _highExplosiveProjectilePool;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private PlayerInputHolder _playerInputHolder;

    private Projectile _projectile;
    private BaseProjectilePool _activePool;

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
        _projectile = _activePool.Pool.TakeFromPool();
        _projectile.gameObject.layer = Constants.PLAYER_PROJECTILE_LAYER;
        _projectile.transform.position = _bulletSpawn.position;
        _projectile.transform.rotation = _bulletSpawn.rotation;
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
