using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusType = Bombbrothers.Logic.Enums.BonusType;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    /// Abstrakcyjna klasa bonus. Dziedziczy po abstrakcyjnej klasie GObject. (Nie zaimplementowano)
    /// </summary>
    abstract class Bonus : GObject
    {
        /// <summary>
        /// Maksymalna ilość wystąpień danego bonusu na danym poziomie gry.
        /// </summary>
        protected int MaxNumberOfOccurrences;

        /// <summary>
        /// Szansa wystąpienia danego bonusu na danym poziomie gry.
        /// </summary>
        protected float ChanceOfOccurrence;

        /// <summary>
        /// Typ enum bonusu.
        /// </summary>
        protected BonusType Type;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Bonus(BonusType type) : base()
        {
            Type = type;
        }

        /// <summary>
        /// Abstrakcyjna metoda wykonująca akcję danego bonusu.
        /// </summary>
        public abstract void Action();

    }
}
