using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public abstract class Creature : MonoBehaviour
{
	public event Action OnCreatureStuned;

	[Header("Creature params")]
	[SerializeField] protected float strength;
	[SerializeField] protected float agility;
	[SerializeField] protected float intelligence;
	[SerializeField] protected float protection;

	private bool isStuned;

	public float Strength => strength;
	public float Agility => agility;
	public float Intelligence => intelligence;
	public float Protection => protection;
	public bool IsStuned
	{
		get => isStuned;
		set
		{
			isStuned = value;
			if (isStuned == true)
			{

			}
		}
	}

	protected Movement movement;
	public WorldController World { get; private set; }

	protected virtual void Init()
	{
		movement = GetComponent<Movement>();
		World = FindObjectOfType<WorldController>();

		if (World == null)
		{
			throw new Exception($"{gameObject.name} can not find world controller");
		}

		movement.SetStartProperties(transform.eulerAngles.y, Tools.GetVector2IntPosition(transform.position));
	}

	public virtual void ApplyDamage(Creature damager, float damage)
	{
		if (TryGetComponent(out Health enemyHealth))
		{
			enemyHealth.ApplyDamage(damage);
		}
	}
}
