using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("[Weapon]")] 
    [SerializeField] private WeaponBase weaponBase;
    internal WeaponBase WeaponBase => weaponBase;

    [Header("[State]")]
    [SerializeField] public float shield = 5;
    [SerializeField] public float damage = 5;
    [SerializeField] public float attackDistance = 10;
}
