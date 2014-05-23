using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Bombbrothers.Additional;
using Bombbrothers.Resources.XMLModel;

namespace Bombbrothers.Logic
{
    /// <summary>
    ///     Statyczna klasa zarządzająca plikami w programie.
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        ///     Ścieżka względna do folderu z plikami używanymi w aplikacji.
        /// </summary>
        private const string Dir = @"Data";

        /// <summary>
        ///     Nazwa pliku z listą użytkowników.
        /// </summary>
        private const string Users = "Users.dat";

        /// <summary>
        ///     Nazwa pliku z listą zapisanych gier.
        /// </summary>
        private const string SavedGames = "SavedGames.dat";

        /// <summary>
        ///     Nazwa pliku z listą zapisanych najlepszych wyników.
        /// </summary>
        private const string HighScores = "HighScores.dat";

        /// <summary>
        ///     Nazwa pliku z listą zapisanych ustawień.
        /// </summary>
        private const string Settings = "Settings.dat";

        /// <summary>
        ///     Przygotowanie plików i folderów. Jeśli czegoś brakuje, ta metoda tworzy to.
        /// </summary>
        public static void Prepare()
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);

            var files = Directory.GetFiles(Dir);

            var Files = new List<string>(files);

            for (var i = 0; i < Files.Count; ++i)
                Files[i] = Files[i].Split('\\')[1];

            if (!Files.Contains(Users))
            {
                var users = new Users {Users1 = new List<UsersUser>()};

                File.WriteAllText(Path.Combine(Dir, Users), users.Serialize());
            }

            if (!Files.Contains(SavedGames))
            {
                // TODO: create model to SavedGames
                File.WriteAllText(Path.Combine(Dir, SavedGames), String.Empty);
            }

            if (!Files.Contains(HighScores))
            {
                var scores = new Scores {Scores1 = new List<ScoresScore>()};

                File.WriteAllText(Path.Combine(Dir, HighScores), scores.Serialize());
            }

            if (!Files.Contains(Settings))
            {
                var settings = new Settings {Controls = new List<SettingsControl>()};

                File.WriteAllText(Path.Combine(Dir, Settings), settings.Serialize());
            }
        }

        /// <summary>
        ///     Tworzy ustawienia dla użytkownika o podanym numerze indentyfikacyjnym.
        /// </summary>
        /// <param name="id">Numer identyfikacyjny użytkownika.</param>
        private static void CreateSettings(ulong id)
        {
            try
            {
                var settings = Resources.XMLModel.Settings.Deserialize(ReadFile(Path.Combine(Dir, Settings)));
                var sc = new SettingsControl {UserId = id, UserIdSpecified = true};
                settings.Controls.Add(sc);

                settings.SaveToFile(Path.Combine(Dir, Settings));

                GameParameters.ActualSettings = sc;
            }
            catch
            {
                MessageBox.Show("Couldn't create settings");
            }
        }

        /// <summary>
        ///     Zmienia ustawienia dla aktualnie zalogowanego użytkownika na te podane w parametrze.
        /// </summary>
        /// <param name="sc">Nowe ustawienia dla gracza.</param>
        public static void SetSettings(SettingsControl sc)
        {
            try
            {
                var settings = Resources.XMLModel.Settings.Deserialize(ReadFile(Path.Combine(Dir, Settings)));
                var ind = settings.Controls.FindIndex(x => x.UserId == sc.UserId);
                settings.Controls[ind] = sc;

                settings.SaveToFile(Path.Combine(Dir, Settings));

                GameParameters.ActualSettings = sc;

                MessageBox.Show("Settings changed succesfully.");
            }
            catch (Exception)
            {
                MessageBox.Show("Settings couldn't be changed.");
            }
        }

        /// <summary>
        ///     Pobiera ustawienia użytkownika o podanym numerze indentyfikacyjnym.
        /// </summary>
        /// <param name="id">Numer identyfikacyjny użytkownika.</param>
        private static void GetSettings(ulong id)
        {
            try
            {
                var settings = Resources.XMLModel.Settings.Deserialize(ReadFile(Path.Combine(Dir, Settings)));
                GameParameters.ActualSettings = settings.Controls.Find(x => x.UserId == id);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error.");
            }
        }

        /// <summary>
        ///     Tworzy nowego użytkownika o podanym loginie i haśle.
        /// </summary>
        /// <param name="name">Login.</param>
        /// <param name="password">Hasło.</param>
        public static void CreateUser(string name, string password)
        {
            try
            {
                var users = Resources.XMLModel.Users.Deserialize(ReadFile(Path.Combine(Dir, Users)));

                var user = new UsersUser
                {
                    Id = (ulong) users.Users1.Count,
                    IdSpecified = true,
                    Name = name,
                    Password = GetBytes(password)
                };

                users.Users1.Add(user);

                users.SaveToFile(Path.Combine(Dir, Users));

                CreateSettings(user.Id);

                GameParameters.ActualUser = user;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't create user.");
            }
        }

        /// <summary>
        ///     Waliduje login i hasło użytkownika.
        /// </summary>
        /// <param name="name">Login.</param>
        /// <param name="password">Hasło.</param>
        /// <returns>True jeśli dany użytkownik istniał i hasło jest poprawne.</returns>
        public static bool GetUser(string name, string password)
        {
            try
            {
                var users = Resources.XMLModel.Users.Deserialize(ReadFile(Path.Combine(Dir, Users)));
                var user = users.Users1.Find(x => x.Name == name);
                if (user == null)
                {
                    var dialogResult =
                        MessageBox.Show("No player with name: " + name + "\n Want to create new user ?",
                            "Player don't exist", MessageBoxButton.YesNo);
                    if (dialogResult != MessageBoxResult.Yes) return false;
                    CreateUser(name, password);
                    return true;
                }


                if (GetString(user.Password) != password)
                {
                    MessageBox.Show("Wrong password for player: " + name);
                    return false;
                }

                GameParameters.ActualUser = user;
                GetSettings(user.Id);
                MessageBox.Show("Logged successfully.");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error.");
                return false;
            }
        }

        /// <summary>
        ///     Pobiera listę najlepszych wyników.
        /// </summary>
        /// <returns>Lista najlepszych wyników.</returns>
        public static Scores GetHighScores()
        {
            try
            {
                return Scores.Deserialize(ReadFile(Path.Combine(Dir, HighScores)));
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error.");
                return null;
            }
        }

        /// <summary>
        ///     Odczy pliku z danej ścieżki.
        /// </summary>
        /// <param name="name">Ścieżka do pliku.</param>
        /// <returns>Zawartość pliku jako tekst.</returns>
        private static string ReadFile(string name)
        {
            var streamReader = new StreamReader(name);
            var problemString = streamReader.ReadToEnd();
            streamReader.Close();

            return GetString(GetBytes(problemString));
        }

        /// <summary>
        ///     Konwersja string -> byte[]
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>byte[]</returns>
        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        ///     Konwersja byte[] -> string
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>string</returns>
        private static string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}