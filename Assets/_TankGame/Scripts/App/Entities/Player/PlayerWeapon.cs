using System;
using System.Collections;
using TankGame.App.Entities.Player.Data;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Projectiles;
using TankGame.Core.Services.Input;
using TankGame.Core.Utils.Enums;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.App.Entities.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public event Action OnPlayerShot;
        public event Action OnApProjectileTypeChosen;
        public event Action OnHexProjectileTypeChosen;

        [SerializeField] private PlayerWeaponData _weaponData;
        [SerializeField] private Transform _bulletSpawn;

        private IInputService _inputService;
        private IPoolsService _poolsService;
        private ProjectileTypeId _selectedProjectile;
        private Projectile _projectile;

        private bool _canShoot = true;

        public PlayerWeaponData WeaponData => _weaponData;

        public void Init(IInputService inputService, IPoolsService poolsService)
        {
            _inputService = inputService;
            _poolsService = poolsService;

            _inputService.OnLeftMouseButtonClicked += Shoot;
            _inputService.OnFirstProjectileTypeSelected += ChooseFirstProjectileType;
            _inputService.OnSecondProjectileTypeSelected += ChooseSecondProjectileType;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
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
            _projectile = _poolsService.GetProjectile(_selectedProjectile);
            _projectile.gameObject.layer = (int)Layers.PlayerProjectile;
            _projectile.transform.position = _bulletSpawn.position;
            _projectile.transform.rotation = _bulletSpawn.rotation;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_weaponData.ReloadTime);
            _canShoot = true;
        }

        private void ChooseFirstProjectileType()
        {
            _selectedProjectile = ProjectileTypeId.AP;
            OnApProjectileTypeChosen?.Invoke();
        }

        private void ChooseSecondProjectileType()
        {
            _selectedProjectile = ProjectileTypeId.HEX;
            OnHexProjectileTypeChosen?.Invoke();
        }
    }
}