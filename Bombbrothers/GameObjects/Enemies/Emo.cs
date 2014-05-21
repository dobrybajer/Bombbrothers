using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Enemies
{
    /// <summary>
    /// Klasa reprezentująca konkretny typ wroga - Emo. Dziedziczy po abstrakcyjnej klasie Enemy.
    /// </summary>
    class Emo : Enemy
    {
        /// <summary>
        /// Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public Emo() : base(Enums.EnemyType.Emo) { }

        /// <summary>
        /// Metoda poruszania wroga specyficzna dla danego typu. (nie zaimplementowano)
        /// </summary>
        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
