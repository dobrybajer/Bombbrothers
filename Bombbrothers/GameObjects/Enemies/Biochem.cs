using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    /// Klasa reprezentująca konkretny typ wroga - Biochem. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    class Biochem : Enemy
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Biochem() : base(Enums.EnemyType.Biochem) { }
    }
}
