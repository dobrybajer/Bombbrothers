using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    ///     Klasa reprezentująca konkretny typ wroga - Woźny. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    internal class Janitor : Enemy
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Janitor() : base(Enums.EnemyType.Janitor)
        {
        }

        /// <summary>
        ///     Akcja typu Strzał do gracza. (nie zaimplementowano)
        /// </summary>
        public void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}