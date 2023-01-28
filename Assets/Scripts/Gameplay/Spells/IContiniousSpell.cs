namespace Gameplay.Spells
{
    public interface IContiniousSpell
    {
        void OnCastStart();
        void OnInterruptCast();
    }
}