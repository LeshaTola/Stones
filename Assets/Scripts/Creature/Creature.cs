using UnityEngine;

[RequireComponent(typeof(Movement))]
public abstract class Creature : MonoBehaviour
{
	[Header("Creature params")]
	[SerializeField] protected float strength;
	[SerializeField] protected float agility;
	[SerializeField] protected float intelligence;
	[SerializeField] protected float protection;

	[Header("Attack params")]
	[SerializeField] protected float attackCooldown;

	protected float attackTimer;

	protected Movement movement;
	protected virtual void Init()
	{
		movement = GetComponent<Movement>();
		movement.SetStartProperties(transform.eulerAngles.y, Tools.GetVector2IntPosition(transform.position));
	}
}
