using System;
using R3;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.Projectiles;
using TankGame.Runtime.StaticData.Player;
using TankGame.Runtime.Utils.Enums;
using TankGame.Runtime.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.Runtime.Entities.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponData _weaponData;
        [SerializeField] private Transform _bulletSpawn;

        private IInputService _inputService;
        private IPoolsService _poolsService;
        private Projectile _projectile;

        private bool _canShoot = true;
        private float _reloadingTimer;
        private Player _player;

        private readonly ReactiveProperty<float> _reloadProgress = new();
        private readonly ReactiveProperty<ProjectileTypeId> _selectedProjectile = new();
        
        public Observable<float> ReloadProgress => _reloadProgress;
        public Observable<ProjectileTypeId> SelectedProjectile => _selectedProjectile;

        public void Init(Player player, IInputService inputService, IPoolsService poolsService)
        {
            _player = player;
            _inputService = inputService;
            _poolsService = poolsService;

            _inputService.OnAttackPressed += Shoot;
            _inputService.OnFirstProjectileTypeSelected += ChooseFirstProjectileType;
            _inputService.OnSecondProjectileTypeSelected += ChooseSecondProjectileType;
        }

        public void LogicUpdate()
        {
            _reloadingTimer += Time.deltaTime;

            if (_reloadingTimer >= _weaponData.ReloadTime)
            {
                _reloadingTimer = _weaponData.ReloadTime;
                _canShoot = true;
            }
            
            _reloadProgress.Value = _reloadingTimer / _weaponData.ReloadTime;
        }

        private void OnDestroy()
        {
            _inputService.OnAttackPressed -= Shoot;
            _inputService.OnFirstProjectileTypeSelected -= ChooseFirstProjectileType;
            _inputService.OnSecondProjectileTypeSelected -= ChooseSecondProjectileType;
        }

        private void Shoot()
        {
            if (!_canShoot || _player.IsPaused)
                return;

            _projectile = _poolsService.GetProjectile(_selectedProjectile.Value);
            _projectile.gameObject.layer = (int)Layers.PlayerProjectile;
            _projectile.transform.position = _bulletSpawn.position;
            _projectile.transform.rotation = _bulletSpawn.rotation;
            _reloadingTimer = 0;
            _canShoot = false;
        }

        private void ChooseFirstProjectileType()
        {
            if (_player.IsPaused)
                return;
            
            _selectedProjectile.Value = ProjectileTypeId.AP;
        }

        private void ChooseSecondProjectileType()
        {
            if (_player.IsPaused)
                return;
            
            _selectedProjectile.Value = ProjectileTypeId.HEX;
        }
    }
}