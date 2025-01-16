using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private EffectObjectPool _nameEffectInObjectPool;

    public void Shoot()
    {

    }

    private GameObject GetShootProjectile()
    {
        return ObjectPoolForEffect.Instance.GetEffect(_nameEffectInObjectPool);
    }

    public void SetNewProjectileForshoot(EffectObjectPool newProjectile)
    {
        _nameEffectInObjectPool = newProjectile;
    }
}


