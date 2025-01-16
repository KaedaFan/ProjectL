using System.Collections;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private float _moveSpeed;
    private int _damageAmount;
    private Transform _target;
    private EffectObjectPool _nameEffectInObjectPool;
    private float _maxMovetime = 10f;

    private Coroutine _movementCoroutine;

    /// <summary>
    /// Passing values to the projectile
    /// </summary>
    /// <param name="speed">Projectile speed</param>
    /// <param name="damage">Damage dealt by the projectile</param>
    /// <param name="target">The target to which the projectile is moving</param>
    /// <param name="nameInObjectPool">The name of the effect for the projectile in the object pool</param>
    public void Initialize(float speed, int damage, Transform target, EffectObjectPool nameInObjectPool)
    {
        _moveSpeed = speed;
        _damageAmount = damage;
        _target = target;
        _nameEffectInObjectPool = nameInObjectPool;

        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }
        _movementCoroutine = StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _maxMovetime)
        {
            if (_target == null)
            {
                ReturnToPool();
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _target.position) < 0.1f)
            {
                HitTarget();
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        ReturnToPool();
    }

    private void HitTarget()
    {
        if (_target != null)
        {
            var enemy = _target.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damageAmount);
            }
        }
        ReturnToPool();
    }

    private void ReturnToPool()
    {

        ObjectPoolForEffect.Instance.ReturnEffect(_nameEffectInObjectPool, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _target)
        {
            HitTarget();
        }
    }
}
