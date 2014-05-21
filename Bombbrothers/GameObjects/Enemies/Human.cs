using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    /// Klasa reprezentująca konkretny typ wroga - Human. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    class Human : Enemy
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Human() : base(Enums.EnemyType.Human) { }
    }
}
