using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    ///     Klasa reprezentująca konkretny typ wroga - Matfiz. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    internal class Matphys : Enemy
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Matphys() : base(Enums.EnemyType.Matphys)
        {
        }
    }
}