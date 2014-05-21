using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusType = Bombbrothers.Logic.Enums.BonusType;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    /// Klasa reprezentująca bonus 'Inkantacja'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    class Incantation : Bonus
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Incantation() : base(BonusType.Incantation)
        {

        }

        /// <summary>
        /// Metoda wykonująca akcję danego bonusu.
        /// </summary>
        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
