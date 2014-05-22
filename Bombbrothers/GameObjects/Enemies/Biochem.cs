using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    ///     Klasa reprezentująca konkretny typ wroga - Biochem. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    internal class Biochem : Enemy
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Biochem() : base(Enums.EnemyType.Biochem)
        {
        }
    }
}