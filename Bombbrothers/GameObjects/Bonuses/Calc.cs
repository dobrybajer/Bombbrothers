using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusType = Bombbrothers.Logic.Enums.BonusType;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    /// Klasa reprezentująca bonus 'Kalkulator'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    class Calc : Bonus
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Calc() : base(BonusType.Calc)
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
