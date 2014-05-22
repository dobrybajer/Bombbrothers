using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Klasa reprezentująca bonus 'Kalkulator'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    internal class Calc : Bonus
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Calc() : base(Enums.BonusType.Calc)
        {
        }

        /// <summary>
        ///     Metoda wykonująca akcję danego bonusu.
        /// </summary>
        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}