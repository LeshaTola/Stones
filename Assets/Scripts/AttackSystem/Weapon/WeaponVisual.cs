using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    private const string ATTACK_TRIGGER = "Attack";

    [SerializeField] private AttackSystem attackSystem;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attackSystem.OnAttack += OnAttack;
    }

    private void OnDisable()
    {
        attackSystem.OnAttack -= OnAttack;
    }
    private void OnAttack()
    {
        animator.SetTrigger(ATTACK_TRIGGER);
    }
}
