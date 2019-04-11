using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace pazel_Game
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game 
    {
        Grid playGrid;
        Pazel puzzle;
        readonly int width = 52;
        readonly int SIZE;
        Difficulty difficulty;
        public Game(int size, Difficulty difficulty)
        {
            SIZE = size;
            if (SIZE > 4)
                width += 8;
            if (SIZE > 9)
                width += 18;
            InitializeComponent();

            #region Initialize 
            this.Width = width * (SIZE) + 5;
            this.Height = width * (SIZE) + 5 + 75;

            playGrid = new Grid();
            playGrid.Name = "playGrid";
            playGrid.Height = playGrid.Width = width * SIZE;
            playGrid.Margin = new Thickness(5, 5, 5, 5);
            playGrid.HorizontalAlignment = HorizontalAlignment.Center;
            playGrid.VerticalAlignment = VerticalAlignment.Top;
            playGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            for (int i = 0; i < SIZE; i++)
            {
                playGrid.ColumnDefinitions.Add(new ColumnDefinition());
                playGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < SIZE * SIZE; i++)
            {
                Label label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(2),
                    //label.Foreground = new SolidColorBrush(Colors.Azure); //Color.FromRgb((byte)255, (byte)255, (byte)255 );
                    FontFamily = new FontFamily("You're Gone"),
                    FontSize = 30,
                    Name = string.Format("lable{0}{1}", i / SIZE, i % SIZE)
                };
                Grid.SetRow(label, i / SIZE);
                Grid.SetColumn(label, i % SIZE);
                playGrid.Children.Add(label);

            }
            Border border = new Border
            {
                Background = new SolidColorBrush(Colors.LightSteelBlue),
                BorderThickness = new Thickness(2),
                Child = playGrid
            };
            this.Content = border;

            #endregion Initialize

            puzzle = new Pazel(SIZE, difficulty);
            this.KeyDown += Game_KeyDown;
            Refresh();

        }

        private void Game_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    puzzle.LeftKey(sender, e);
                    Refresh();
                    break;
                case Key.Right:
                    puzzle.RightKey(sender, e);
                    Refresh();
                    break;
                case Key.Up:
                    puzzle.UpKey(sender, e);
                    Refresh();
                    break;
                case Key.Down:
                    puzzle.DownKey(sender, e);
                    Refresh();
                    break;
            }
            //todo win chacking
            if (puzzle.Win())
                MessageBox.Show("You Win!!!");
        }
        void Refresh()
        {
            int index = 0;
            foreach (Label lable in (from Control item in playGrid.Children where item is Label && index <= SIZE * SIZE select item))
            {
                int value = puzzle.Matrix[index / SIZE, index++ % SIZE];
                //if (value == 0)
                //{

                //}
                lable.Content = (value == 0) ? "" : value.ToString();
                lable.Background = new SolidColorBrush((value == 0) ? Colors.BurlyWood : Colors.Bisque);
            }
        }

    }
}
