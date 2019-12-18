using Maze.Core;
using Maze.Core.Algorithms;
using System.Windows;
using System.Windows.Controls;

namespace Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private MazeController mazeController = MazeController.Instance; // The singleton instance of the Maze class
        private CellControl[,] cellControls; // A grid of cell controls
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the seed from the user input
        /// </summary>
        /// <remarks>
        /// If the input is an integer => seed
        /// If the input is a string => hash => seed
        /// If the input is empty, null or white space => random seed
        /// </remarks>
        /// <returns>The seed</returns>
        private int? GetSeed()
        {
            string seedString = SeedTextBox.Text;
            int seedIfInt = 0;
            int.TryParse(seedString, out seedIfInt);
            if (seedIfInt != 0)
            {
                return seedIfInt;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(seedString)) return null;
                return seedString.GetHashCode();
            }
        }

        /// <summary>
        /// Create a new grid
        /// </summary>
        public void NewControlGrid()
        {
            ResetMazeGrid();

            cellControls = new CellControl[mazeController.MazeGrid.Columns, mazeController.MazeGrid.Rows];
            for (int row = 0; row < mazeController.MazeGrid.Rows; row++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                MazeGrid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int col = 0; col < mazeController.MazeGrid.Columns; col++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                MazeGrid.RowDefinitions.Add(rowDefinition);
            }
            for (int row = 0; row < mazeController.MazeGrid.Rows; row++)
            {
                for (int col = 0; col < mazeController.MazeGrid.Columns; col++)
                {
                    CellControl cellControl = new CellControl(mazeController.MazeGrid[row, col]);
                    cellControl.Column = col;
                    cellControl.Row = row;
                    cellControl.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cellControl.VerticalAlignment = VerticalAlignment.Stretch;
                    MazeGrid.Children.Add(cellControl);
                    Grid.SetRow(cellControl, row);
                    Grid.SetColumn(cellControl, col);
                    cellControls[col, row] = cellControl; // WARNING: This is inverted !
                }
            }
            Update();
        }

        /// <summary>
        /// Clear the grid and create a new empty one
        /// </summary>
        private void ResetMazeGrid()
        {
            MainGrid.Children.Remove(MazeGrid);
            MazeGrid = new Grid();
            MainGrid.Children.Add(MazeGrid);
            Grid.SetColumn(MazeGrid, 1);
            Grid.SetRow(MazeGrid, 1);
        }

        /// <summary>
        /// Update the display
        /// </summary>
        public void Update()
        {
            for (int y = 0; y < mazeController.MazeGrid.Rows; y++)
            {
                for (int x = 0; x < mazeController.MazeGrid.Columns; x++)
                {
                    cellControls[x, y].Update();
                }
            }
        }

        /// <summary>
        /// On click on GenerateButton
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            int cols = ColumnsIntegerUpDown.Value ?? 50;
            int rows = RowsIntegerUpDown.Value ?? 50;
            int? seed = GetSeed();

            mazeController.MazeGrid = new MazeGrid(rows, cols, seed);
            mazeController.BuildMaze(Algorithm.TruePrim);
            NewControlGrid();
        }

        /// <summary>
        /// On solve button clicked
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">The arguments</param>
        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            if (mazeController.MazeGrid == null) return;
            mazeController.SolveMaze();
            Update();
        }
        #endregion
    }
}
