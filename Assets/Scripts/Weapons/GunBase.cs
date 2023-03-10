using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public Define.WeaponType weaponType = Define.WeaponType.None;

    protected GameObject _bulletRoot;
    protected GameObject _bulletOrigin;
    protected Transform _firePos;

    protected float _power;
    protected int _maxAmmo;
    protected int _fullLoadAmmo;
    float reloadingTime;

    [SerializeField] protected int _curAmmo;
    [SerializeField] protected int _curLoadAmmo;

    void Start()
    {
        Init();
    }

    public abstract void LoadBulletResource();
    public abstract void GenerateBullets();
    public void Fire()
    {
        if (_curLoadAmmo == 0)
            return;

        _curLoadAmmo--;
        GenerateBullets();
    }

    public int GetCurLoadedAmmo()
    {
        return _curLoadAmmo;
    }

    public float GetReloadingTime()
    {
        return 1f;
    }

    public virtual void Reload()
    {
        if (_curLoadAmmo == _fullLoadAmmo)
            return;

        int maxReloadAmmoCount = _fullLoadAmmo - _curLoadAmmo;
        int reloadAmmoCount = Mathf.Min(_curAmmo, maxReloadAmmoCount);

        _curAmmo -= reloadAmmoCount;
        _curLoadAmmo += reloadAmmoCount;
    }

    public virtual void Init()
    {
        Data.Weapon weaponInfo = Managers.Data.weaponDict[weaponType.ToString()];

        _power = weaponInfo.power;
        _maxAmmo = weaponInfo.maxAmmo;
        _fullLoadAmmo = weaponInfo.fullLoadAmmo;
        _curAmmo = _maxAmmo;
        _curLoadAmmo = _fullLoadAmmo;

        _bulletRoot = GetBulletRoot();
        _firePos = transform.Find("FirePos");
        LoadBulletResource();
    }

    GameObject GetBulletRoot()
    {
        GameObject _bulletRoot = GameObject.Find("BulletControll");
        if (_bulletRoot == null)
        {
            _bulletRoot = new GameObject("BulletControll");
        }

        return _bulletRoot;
    }
}
