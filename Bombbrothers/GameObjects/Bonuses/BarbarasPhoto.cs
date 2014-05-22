using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Klasa reprezentująca bonus 'Zdjęcie Barbary'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    internal class BarbarasPhoto : Bonus
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public BarbarasPhoto() : base(Enums.BonusType.BarbarasPhoto)
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