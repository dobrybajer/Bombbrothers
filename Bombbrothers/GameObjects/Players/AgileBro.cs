using Bombbrothers.Logic;

namespace Bombbrothers.GameObjects.Players
{
    /// <summary>
    ///     Klasa reprezentująca typ gracza - zręczny. Dziedziczy po abstrakcyjnej klasie Player.
    /// </summary>
    internal class AgileBro : Player
    {
        /// <summary>
        ///     Konstruktor. Wywołuje konstruktor bazowy.
        /// </summary>
        public AgileBro() : base(Enums.PlayerType.Agile)
        {
        }
    }
}