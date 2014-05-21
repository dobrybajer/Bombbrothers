using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    /// Klasa reprezentująca konkretny typ wroga - Matfiz. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    class Matphys : Enemy
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Matphys() : base(Enums.EnemyType.Matphys) { }
    }
}
