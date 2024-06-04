using System.Collections;
using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    public event Action OnPlayerShot;
    public event Action OnApProjectileTypeChosen;
    public event Action OnHexProjectileTypeChosen;

    [SerializeField] private PlayerWeaponData _weaponData;
    [SerializeField] private ArmorPiercingProjectilePool _armorPiercingProjectilePool;
    [SerializeField] private HighExplosiveProjectilePool _highExplosiveProjectilePool;
    [SerializeField] private Transform _bulletSpawn;
    
    private IInputService _inputService;
    private Projectile _projectile;
    private BaseProjectilePool _activePool;

    private bool _canShoot = true;

    public PlayerWeaponData WeaponData => _weaponData;

    public void Init(IInputService inputService)
    {
        _activePool = _armorPiercingProjectilePool;
        _inputService = inputService;
    }

    private void OnEnable()
    {
        _inputService.OnLeftMouseButtonClicked += Shoot;
        _inputService.OnFirstProjectileTypeSelected += ChooseFirstProjectileType;
        _inputService.OnSecondProjectileTypeSelected += ChooseSecondProjectileType;
    }

    private void OnDisable()
    {
        _inputService.OnLeftMouseButtonClicked -= Shoot;
        _inputService.OnFirstProjectileTypeSelected -= ChooseFirstProjectileType;
        _inputService.OnSecondProjectileTypeSelected -= ChooseSecondProjectileType;
    }

    private void Shoot()
    {
        if (!_canShoot)
            return;

        OnPlayerShot?.Invoke();
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
        OnApProjectileTypeChosen?.Invoke();
    }

    private void ChooseSecondProjectileType()
    {
        _activePool = _highExplosiveProjectilePool;
        OnHexProjectileTypeChosen?.Invoke();
    }
}
