using UnityEngine;

[CreateAssetMenu(fileName = "Spell")]
public class SpellSO : ScriptableObject
{
    [SerializeField] private int damagePercent;
    [SerializeField] private float cooldown;
    [SerializeField] private float manaCost;
    [SerializeField] private int attackNumber;
    [SerializeField] private ICastable effect;

   public int DamagePercent=>damagePercent;
   public float Cooldown => cooldown;
   public float ManaCost => manaCost;
   public int AttackNumber => attackNumber;
   public ICastable Effect => effect;

}
