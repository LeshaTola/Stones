using UnityEngine;

[RequireComponent(typeof(Movement))]
public abstract class Creature : MonoBehaviour
{
	[Header("Creature params")]
	[SerializeField] protected float strength;
	[SerializeField] protected float agility;
	[SerializeField] protected float intelligence;
	[SerializeField] protected float protection;

	public float Strength=>strength;
	public float Agility=>agility;
	public float Intelligence=>intelligence;
	public float Protection=>protection;

	protected Movement movement;
	protected virtual void Init()
	{
		movement = GetComponent<Movement>();
		movement.SetStartProperties(transform.eulerAngles.y, Tools.GetVector2IntPosition(transform.position));
	}
}
