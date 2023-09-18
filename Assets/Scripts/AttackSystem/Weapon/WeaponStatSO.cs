using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStat")]
public class WeaponStatSO : ScriptableObject
{
    [SerializeField] private AttackTypeEnum attackType;
    [SerializeField] private int agility;
    [SerializeField] private int strength;
    [SerializeField] private int intelligence;
    [SerializeField] private float damage;
    [SerializeField] private int rangeAttack;
    [SerializeField] private TypeOfWeaponEnum typeOfWeapon;
    [SerializeField] private RarityEnum rarity;
    [SerializeField] private ICastable effect;
    [Header("Visual")]
    [SerializeField] private Sprite icon;
    [SerializeField] private Weapon weapon;


    public AttackTypeEnum AttackType => attackType;
    public int Agility => agility;
    public int Strength => strength;
    public int Intelligence  =>intelligence;
    public float Damage => damage;
    public int RangeAttack => rangeAttack;
    public TypeOfWeaponEnum TypeOfWeapon => typeOfWeapon;
    public RarityEnum Rarity => rarity;
    public ICastable Effect => effect;

}
