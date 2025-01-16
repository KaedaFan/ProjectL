using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;

    private EffectObjectPool _nameProjectileInObjectPool = EffectObjectPool.Bullet;

    private float _throwForce = 10f;
    private int _damageAmount = 10;
    private float _rangeAttack = 20f;

    private Transform _target;

    private void Awake()
    {
        if (_stats != null)
        {
            _nameProjectileInObjectPool = _stats.NameProjectile;
            _throwForce = _stats.ThrowForce;
            _damageAmount = _stats.DamageAmount;
            _rangeAttack = _stats.RangeAttack;
        }
    }

    public void Shoot()
    {
        _target = FindNearTarget();

        if (_target == null)
        {
            Debug.Log("No enemies nearby to shoot at");
            return;
        }

        GameObject projectile = GetShootProjectile();

        Debug.Log(_nameProjectileInObjectPool);
        if (projectile == null)
        {
            Debug.LogError("Projectile for shoot = null. Check ObjectPool's settings");
            return;
        }

        projectile.transform.position = transform.position;

        Vector3 direction = _target.position - transform.position; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle); 


        ProjectileMove projectileMove = projectile.GetComponent<ProjectileMove>();
        if (projectileMove == null)
        {
            Debug.LogWarning("Projectile doesn't have ProjectileMove script");
            projectile.AddComponent<ProjectileMove>();
        }

        projectileMove.Initialize(_throwForce, _damageAmount, _target, _nameProjectileInObjectPool);
    }

    // Method for finding the near "enemy" target
    private Transform FindNearTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= _rangeAttack) 
            {
                shortestDistance = distanceToEnemy;
                nearestTarget = enemy.transform;
            }
        }

        return nearestTarget;
    }

    public void SetNewProjectileForshoot(EffectObjectPool newProjectile)
    {
        _nameProjectileInObjectPool = newProjectile;
    }

    private GameObject GetShootProjectile()
    {
        return ObjectPoolForEffect.Instance.GetEffect(_nameProjectileInObjectPool);
    }
}


