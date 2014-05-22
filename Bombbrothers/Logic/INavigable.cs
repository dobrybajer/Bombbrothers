namespace Bombbrothers.Logic
{
    /// <summary>
    ///     Interfejs dla klas które mogą służyć jako strony zmieniane dynamicznie (zawartość okna)
    /// </summary>
    public interface INavigable
    {
        /// <summary>
        ///     Metoda wywoływana przez klasy implementujące interfejs.
        /// </summary>
        /// <param name="state"></param>
        void UtilizeState(object state);
    }
}