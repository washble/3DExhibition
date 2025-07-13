using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletAttackTypeSO attackTypeSo;

    private bool isUsed = false;

    private void OnEnable()
    {
        isUsed = true;
        FireStart().Forget();

        if (attackTypeSo.useLifeTime)
        {
            LifeTime().Forget();
        }
    }

    private void OnDisable()
    {
        isUsed = false;
    }

    private void RestoreBullet()
    {
        isUsed = false;
        WeaponSpwaner.Instance.RestoreWeapon(attackTypeSo.weaponType, gameObject);
    }

    private async UniTaskVoid FireStart()
    {
        while (isUsed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * attackTypeSo.speed);
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: destroyCancellationToken);
        }
    }

    private async UniTaskVoid LifeTime()
    {
        float duration = 0;
        
        while (isUsed)
        {
            duration += Time.deltaTime;
            if (duration > attackTypeSo.lifeTime)
            {
                RestoreBullet();
            }

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: destroyCancellationToken);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case (int)GameObjectLayer.Map:
                RestoreBullet();
                break;
            case (int)GameObjectLayer.Player:
                RestoreBullet();
                break;
        }
    }
}
