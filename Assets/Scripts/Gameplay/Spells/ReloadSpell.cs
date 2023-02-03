namespace Gameplay.Spells
{
    public class ReloadSpell : SpellBehaviour
    {
        public override void OnCast()
        {
            Player.Weapons.EndChargedCooldown();
        }
    }
}