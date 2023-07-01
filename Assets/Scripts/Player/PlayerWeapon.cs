using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum ActivePorjectileType
    {
        AP = 1,
        HEX = 2,
    }

    public ActivePorjectileType ActiveProjectile { get; private set; } = ActivePorjectileType.AP;

    [SerializeField] private ArmorPiercingProjectilePool _armorPiercingProjectilePool;
    [SerializeField] private HighExplosiveProjectilePool _highExplosiveProjectilePool;
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
        _projectile.Explode();
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
