using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    ///     Klasa reprezentująca konkretny typ wroga - Human. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    internal class Human : Enemy
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Human() : base(Enums.EnemyType.Human)
        {
        }
    }
}