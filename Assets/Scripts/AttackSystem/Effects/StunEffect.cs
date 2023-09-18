public class StunEffect : ICastable
{
	public override void Cast(Creature creature)
	{
		creature.IsStuned = true;
	}
}
