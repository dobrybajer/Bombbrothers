using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Bonuses
{
    /// <summary>
    ///     Abstrakcyjna klasa bonus. Dziedziczy po abstrakcyjnej klasie GObject. (Nie zaimplementowano)
    /// </summary>
    internal abstract class Bonus : GObject
    {
        /// <summary>
        ///     Szansa wystąpienia danego bonusu na danym poziomie gry.
        /// </summary>
        protected float ChanceOfOccurrence;

        /// <summary>
        ///     Maksymalna ilość wystąpień danego bonusu na danym poziomie gry.
        /// </summary>
        protected int MaxNumberOfOccurrences;

        /// <summary>
        ///     Typ enum bonusu.
        /// </summary>
        protected Enums.BonusType Type;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        protected Bonus(Enums.BonusType type)
        {
            Type = type;
        }

        /// <summary>
        ///     Abstrakcyjna metoda wykonująca akcję danego bonusu.
        /// </summary>
        public abstract void Action();
    }
}