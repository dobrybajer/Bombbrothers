using System;
using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Klasa reprezentująca bonus 'Książka'. Dziedziczy po abstrakcyjnej klasie Bonus. (Nie zaimplementowano)
    /// </summary>
    internal class Book : Bonus
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public Book() : base(Enums.BonusType.Book)
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