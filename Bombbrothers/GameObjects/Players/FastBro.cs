using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    ///     Klasa reprezentująca typ gracza - szybki. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    internal class FastBro : Player
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public FastBro() : base(Enums.PlayerType.Fast)
        {
        }
    }
}