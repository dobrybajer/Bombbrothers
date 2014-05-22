using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Bombbrothers.GameObjects.Bonuses;
using Bombbrothers.GameObjects.Enemies;
using Bombbrothers.GameObjects.Map;
using Bombbrothers.Logic;

namespace Bombbrothers.Additional
{
    /// <summary>
    ///     Statyczna klasa reprezentująca zbiór obiektów stworzonych na potrzeby aktualnej rozgrywki, bądź dla aktualnie
    ///     zalogowanego użytkownika.
    /// </summary>
    public static class GameParameters
    {
        #region Ustawiane w NewGame.xaml

        /// <summary>
        ///     Poziom trudności wybrany przez użytkownika, lub domyślny (łatwy).
        /// </summary>
        public static Enums.LevelDifficulty Difficulty = Enums.LevelDifficulty.Easy;

        /// <summary>
        ///     Rodzaj gracza wybrany przez użytkownika, lub domyślny (zręczny).
        /// </summary>
        public static Enums.PlayerType PlayerType = Enums.PlayerType.Agile;

        #endregion

        #region Ustawiane bezpośrednio w Game.xaml

        /// <summary>
        ///     Kontrolka, na której rysowane są wszystkie obiekty.
        /// </summary>
        public static Canvas ActualCanvas;

        /// <summary>
        ///     Numer wroga, zwiększany po utworzeniu nowego obiektu Enemy.
        /// </summary>
        public static int Id = 0;

        /// <summary>
        ///     Instancja klasy typu Player.
        /// </summary>
        public static object ActualPlayer;

        /// <summary>
        ///     Kolekcja stworzonych w danym poziomie obiektów Enemy.
        /// </summary>
        public static List<Enemy> Enemies = new List<Enemy>();

        /// <summary>
        ///     Instancja klasy typu MapBoard.
        /// </summary>
        public static MapBoard ActualBoard;

        #endregion

        #region Ustawiane w Game.xaml - za pomocą Board lub Player

        /// <summary>
        ///     Lista współrzędnych Tile, dla których typ wynosi 0 (Background).
        /// </summary>
        public static List<Tuple<int, int>> EmptyTiles = new List<Tuple<int, int>>();

        /// <summary>
        ///     Instancja klasy Bomb, tworzonej przez klasę Player.
        /// </summary>
        public static Bomb ActualBomb;

        #endregion

        #region Ustawiane w LogIn.xaml - za pomocą FileManager

        /// <summary>
        ///     Instancja klasy UsersUser przechowująca informację o aktualnie zalogowanym użytkowniku.
        /// </summary>
        public static UsersUser ActualUser = new UsersUser {IdSpecified = false, Name = "Anonim"};

        /// <summary>
        ///     Instancja klasy SettingsControl przechowująca informację o ustawieniach sterowania dla aktualnie zalogowanego
        ///     użytkownika.
        /// </summary>
        public static SettingsControl ActualSettings = new SettingsControl();

        #endregion
    }
}