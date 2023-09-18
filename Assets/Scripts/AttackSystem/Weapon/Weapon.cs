using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStatSO weaponStat;

    public WeaponStatSO WeaponStat => weaponStat;

    public virtual void AddSpecialEffect()
    {

    }



}
