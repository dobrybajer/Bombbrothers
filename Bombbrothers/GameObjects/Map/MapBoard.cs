using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Bombbrothers.Additional;

namespace Bombbrothers.GameObjects.Map
{
    /// <summary>
    ///     Klasa reprezentująca całą planszę - zbiór obiektów Tile wraz z metodami operującymi na tym zbiorze.
    /// </summary>
    public class MapBoard
    {
        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: prawo.
        /// </summary>
        private const byte Right = GameConst.Right;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: dół.
        /// </summary>
        private const byte Down = GameConst.Down;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: lewo.
        /// </summary>
        private const byte Left = GameConst.Left;

        /// <summary>
        ///     Wartość bajtowa odpowiadająca kierunkowi: góra.
        /// </summary>
        private const byte Up = GameConst.Up;

        /// <summary>
        ///     Liczba wierszy w danej planszy.
        /// </summary>
        private const int Rows = GameConst.MapRows;

        /// <summary>
        ///     Liczba kolumn w danej planszy.
        /// </summary>
        private const int Columns = GameConst.MapColumns;

        /// <summary>
        ///     Słownik Bitmap dla różnych typów Tile.
        /// </summary>
        private static Dictionary<int, BitmapImage> _bitmaps;

        /// <summary>
        ///     Ilość Tile typu 'ławka' na dany poziom.
        /// </summary>
        private const int BenchCount = GameConst.BenchCount;

        /// <summary>
        ///     Ilość Tile typu 'płyta gipsowa' na poziom
        /// </summary>
        private const int GypsumCount = GameConst.GypsumCount;

        /// <summary>
        ///     Konstruktor - tworzy kolekcję Tile[,] oraz generuje domyślny poziom pierwszy.
        /// </summary>
        public MapBoard()
        {
            CreateBitmaps();

            Tiles = new Tile[Rows, Columns];

            GenerateLevelOne();
        }

        /// <summary>
        ///     Kolekcja obiektów Tile. Tworzy całą planszę (bez obiektów dynamicznych typu gracz, wróg, bonus etc.)
        /// </summary>
        private Tile[,] Tiles { get; set; }

        /// <summary>
        ///     Pobiera element Tile z kolekcji elementów Tile[,] o danych współrzędnych
        /// </summary>
        /// <param name="i">Współrzędna 1.</param>
        /// <param name="j">Współrzędna 2.</param>
        /// <returns> Element Tile o danych współrzędnych. </returns>
        public Tile GetTile(int i, int j)
        {
            return Tiles[i, j];
        }

        /// <summary>
        ///     Zwraca stały rozmiar planszy (wiersze x kolumny)
        /// </summary>
        /// <returns>2-elementowa tablica zawierająca liczbę wierszy i kolumn w podanej kolejności.</returns>
        public int[] GetMapSize()
        {
            return new[] {Rows, Columns};
        }

        /// <summary>
        ///     Zmienia stan danego Tile.
        /// </summary>
        /// <param name="x">Współrzędna 1. danego Tile.</param>
        /// <param name="y">Współrzędna 2. danego Tile.</param>
        /// <param name="type">Type na jaki ma zostać zmieniony Tile</param>
        public void ChangeTypeOfTile(int x, int y, int type)
        {
            Tiles[x, y].Type = type;
            Tiles[x, y].IsEmpty = true;
        }

        /// <summary>
        ///     Zwraca byte jako iloczyn logiczny kierunków dla
        /// </summary>
        /// <param name="x">Lista pozycji x na jakich znajduje się obiekt.</param>
        /// <param name="y">Lista pozycji y na jakich znajduje się obiekt.</param>
        /// <returns>Byte jako iloczyn logiczny poprawnych kierunków.</returns>
        public byte GetValidMoves(List<int> x, List<int> y)
        {
            if (x == null || y == null || x.Count == 0 || y.Count == 0)
                return 0;

            var _y = y.First();
            var _x = x.First();

            byte moves = 0;

            if (x.Count == 1 && y.Count == 1)
            {
                switch (_x)
                {
                    case 0:
                        if (Tiles[_x + 1, _y].IsEmpty)
                            moves |= Down;
                        break;
                    case Rows - 1:
                        if (Tiles[_x - 1, _y].IsEmpty)
                            moves |= Up;
                        break;
                    default:
                        if (Tiles[_x - 1, _y].IsEmpty)
                            moves |= Up;
                        if (Tiles[_x + 1, _y].IsEmpty)
                            moves |= Down;
                        break;
                }

                switch (_y)
                {
                    case 0:
                        if (Tiles[_x, _y + 1].IsEmpty)
                            moves |= Right;
                        break;
                    case Columns - 1:
                        if (Tiles[_x, _y - 1].IsEmpty)
                            moves |= Left;
                        break;
                    default:
                        if (Tiles[_x, _y - 1].IsEmpty)
                            moves |= Left;
                        if (Tiles[_x, _y + 1].IsEmpty)
                            moves |= Right;
                        break;
                }
            }
            else if (x.Count == 1 && y.Count != 1)
            {
                moves |= Left;
                moves |= Right;
            }
            else if (x.Count != 1 && y.Count == 1)
            {
                moves |= Up;
                moves |= Down;
            }
            else
            {
                if (Tiles[_x, _y].IsEmpty)
                {
                    moves |= Left;
                    moves |= Up;
                }
                if (Tiles[_x + 1, _y].IsEmpty)
                {
                    moves |= Right;
                    moves |= Up;
                }
                if (Tiles[_x, _y + 1].IsEmpty)
                {
                    moves |= Left;
                    moves |= Down;
                }
                if (Tiles[_x + 1, _y + 1].IsEmpty)
                {
                    moves |= Right;
                    moves |= Down;
                }
            }

            return moves;
        }

        /// <summary>
        ///     Tworzy słownik o kluczach jako typ (int) pola i odpowiadających im bitmapach.
        /// </summary>
        private static void CreateBitmaps()
        {
            _bitmaps = new Dictionary<int, BitmapImage>
            {
                {0, new BitmapImage(new Uri(GameConst.MapBackground, UriKind.RelativeOrAbsolute))},
                {1, new BitmapImage(new Uri(GameConst.MapBlock, UriKind.RelativeOrAbsolute))},
                {2, new BitmapImage(new Uri(GameConst.MapBench, UriKind.RelativeOrAbsolute))},
                {3, new BitmapImage(new Uri(GameConst.MapGypsum, UriKind.RelativeOrAbsolute))}
            };
        }

        /// <summary>
        ///     Tworzy losową mapę tj. różne typy Tile w losowych miejscach.
        /// </summary>
        private void GenerateRandomMap()
        {
            var r = new Random();

            int iRand, jRand, count = 0;
            for (var i = 0; i < Rows; ++i)
                for (var j = 0; j < Columns; ++j)
                    if (i%2 == 1 && j%2 == 1)
                    {
                        Tiles[i, j] = new Tile
                        {
                            Row = i,
                            Column = j,
                            Type = GameConst.TypeBlock,
                            IsConstElement = true,
                            IsEmpty = false
                        };

                        var binding = new Binding("Type")
                        {
                            Converter = new TypeConverter(),
                            Source = Tiles[i, j]
                        };
                        Tiles[i, j].SetBinding(Image.SourceProperty, binding);
                    }
                    else
                    {
                        Tiles[i, j] = new Tile
                        {
                            Row = i,
                            Column = j,
                            Type = GameConst.TypeBackground,
                            IsConstElement = false,
                            IsEmpty = true
                        };

                        var binding = new Binding("Type")
                        {
                            Converter = new TypeConverter(),
                            Source = Tiles[i, j]
                        };
                        Tiles[i, j].SetBinding(Image.SourceProperty, binding);
                    }

            while (count < BenchCount)
            {
                iRand = r.Next()%Rows;
                jRand = r.Next()%Columns;

                if (!Tiles[iRand, jRand].IsEmpty) continue;
                Tiles[iRand, jRand].Type = GameConst.TypeBench;
                Tiles[iRand, jRand].IsConstElement = true;
                Tiles[iRand, jRand].IsEmpty = false;
                count++;
            }

            count = 0;
            while (count < GypsumCount)
            {
                iRand = r.Next()%Rows;
                jRand = r.Next()%Columns;

                if (!Tiles[iRand, jRand].IsEmpty) continue;
                Tiles[iRand, jRand].Type = GameConst.TypeGypsum;
                Tiles[iRand, jRand].IsConstElement = true;
                Tiles[iRand, jRand].IsEmpty = false;
                count++;
            }

            for (var i = 1; i < Rows; ++i)
                for (var j = 1; j < Columns; ++j)
                    if (Tiles[i, j].IsEmpty)
                        GameParameters.EmptyTiles.Add(new Tuple<int, int>(i, j));
        }

        /// <summary>
        ///     Tworzy poziom pierwszy o konkretnie ustawionych Tile i ich typach
        /// </summary>
        private void GenerateLevelOne()
        {
            var benches = new List<Tuple<int, int>>();
            var gypsums = new List<Tuple<int, int>>();

            for (var i = 0; i < Rows; ++i)
                for (var j = 0; j < Columns; ++j)
                    if (i%2 == 1 && j%2 == 1)
                    {
                        Tiles[i, j] = new Tile
                        {
                            Row = i,
                            Column = j,
                            Type = GameConst.TypeBlock,
                            IsConstElement = true,
                            IsEmpty = false
                        };

                        var binding = new Binding("Type")
                        {
                            Converter = new TypeConverter(),
                            Source = Tiles[i, j]
                        };
                        Tiles[i, j].SetBinding(Image.SourceProperty, binding);
                    }
                    else
                    {
                        Tiles[i, j] = new Tile
                        {
                            Row = i,
                            Column = j,
                            Type = GameConst.TypeBackground,
                            IsConstElement = false,
                            IsEmpty = true
                        };

                        var binding = new Binding("Type")
                        {
                            Converter = new TypeConverter(),
                            Source = Tiles[i, j]
                        };
                        Tiles[i, j].SetBinding(Image.SourceProperty, binding);
                    }

            benches.Add(new Tuple<int, int>(8, 0));
            benches.Add(new Tuple<int, int>(8, 2));
            benches.Add(new Tuple<int, int>(8, 4));
            benches.Add(new Tuple<int, int>(8, 6));
            benches.Add(new Tuple<int, int>(8, 8));
            benches.Add(new Tuple<int, int>(8, 10));
            benches.Add(new Tuple<int, int>(8, 12));
            benches.Add(new Tuple<int, int>(0, 6));
            benches.Add(new Tuple<int, int>(4, 6));
            benches.Add(new Tuple<int, int>(12, 6));
            benches.Add(new Tuple<int, int>(16, 6));

            gypsums.Add(new Tuple<int, int>(5, 0));
            gypsums.Add(new Tuple<int, int>(6, 0));
            gypsums.Add(new Tuple<int, int>(7, 0));
            gypsums.Add(new Tuple<int, int>(9, 0));
            gypsums.Add(new Tuple<int, int>(10, 0));
            gypsums.Add(new Tuple<int, int>(11, 0));
            gypsums.Add(new Tuple<int, int>(5, 6));
            gypsums.Add(new Tuple<int, int>(6, 6));
            gypsums.Add(new Tuple<int, int>(7, 6));
            gypsums.Add(new Tuple<int, int>(9, 6));
            gypsums.Add(new Tuple<int, int>(10, 6));
            gypsums.Add(new Tuple<int, int>(11, 6));
            gypsums.Add(new Tuple<int, int>(5, 12));
            gypsums.Add(new Tuple<int, int>(6, 12));
            gypsums.Add(new Tuple<int, int>(7, 12));
            gypsums.Add(new Tuple<int, int>(9, 12));
            gypsums.Add(new Tuple<int, int>(10, 12));
            gypsums.Add(new Tuple<int, int>(11, 12));
            gypsums.Add(new Tuple<int, int>(0, 3));
            gypsums.Add(new Tuple<int, int>(0, 4));
            gypsums.Add(new Tuple<int, int>(0, 5));
            gypsums.Add(new Tuple<int, int>(0, 7));
            gypsums.Add(new Tuple<int, int>(0, 8));
            gypsums.Add(new Tuple<int, int>(0, 9));
            gypsums.Add(new Tuple<int, int>(16, 3));
            gypsums.Add(new Tuple<int, int>(16, 4));
            gypsums.Add(new Tuple<int, int>(16, 5));
            gypsums.Add(new Tuple<int, int>(16, 7));
            gypsums.Add(new Tuple<int, int>(16, 8));
            gypsums.Add(new Tuple<int, int>(16, 9));
            gypsums.Add(new Tuple<int, int>(8, 1));
            gypsums.Add(new Tuple<int, int>(8, 3));
            gypsums.Add(new Tuple<int, int>(8, 5));
            gypsums.Add(new Tuple<int, int>(8, 7));
            gypsums.Add(new Tuple<int, int>(8, 9));
            gypsums.Add(new Tuple<int, int>(8, 11));
            gypsums.Add(new Tuple<int, int>(1, 6));
            gypsums.Add(new Tuple<int, int>(2, 6));
            gypsums.Add(new Tuple<int, int>(3, 6));
            gypsums.Add(new Tuple<int, int>(13, 6));
            gypsums.Add(new Tuple<int, int>(14, 6));
            gypsums.Add(new Tuple<int, int>(15, 6));

            foreach (var t in benches)
            {
                Tiles[t.Item2, t.Item1].Type = GameConst.TypeBench;
                Tiles[t.Item2, t.Item1].IsConstElement = true;
                Tiles[t.Item2, t.Item1].IsEmpty = false;
            }

            foreach (var t in gypsums)
            {
                Tiles[t.Item2, t.Item1].Type = GameConst.TypeGypsum;
                Tiles[t.Item2, t.Item1].IsConstElement = true;
                Tiles[t.Item2, t.Item1].IsEmpty = false;
            }

            for (var i = 0; i < Rows; ++i)
                for (var j = 0; j < Columns; ++j)
                    if (Tiles[i, j].IsEmpty)
                        GameParameters.EmptyTiles.Add(new Tuple<int, int>(i, j));
        }

        /// <summary>
        ///     Konwerter Tile to/from BitmapImage. Używany w bindingu.
        /// </summary>
        [ValueConversion(typeof (Tile), typeof (BitmapImage))]
        private class TypeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var type = (int) value;
                return _bitmaps[type];
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var ib = value as BitmapImage;
                return _bitmaps.First(x => x.Value == ib).Key;
            }
        }
    }
}