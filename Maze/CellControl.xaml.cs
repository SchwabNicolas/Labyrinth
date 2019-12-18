using Maze.Core;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maze
{
    /// <summary>
    /// Interaction logic for CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        #region Properties
        /// <summary>
        /// The cell that is linked to this control
        /// </summary>
        public Cell Cell { get; set; }
        /// <summary>
        /// The x coordinate of the cell
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// The y coordinate of the cell
        /// </summary>
        public int Row { get; set; }
        #endregion

        #region Variables
        private readonly MazeController mazeController = MazeController.Instance; // The singleton instance of the MazeController class
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public CellControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="cell"></param>
        public CellControl(Cell cell)
        {
            Cell = cell;
            InitializeComponent();
        }

        /// <summary>
        /// Update the control
        /// </summary>
        public void Update()
        {
            Cell = mazeController.MazeGrid[Row, Column];

            var opacity = (
                Top: Cell.Borders.Top ? 1 : 0,
                Right: Cell.Borders.Right ? 1 : 0,
                Bottom: Cell.Borders.Bottom ? 1 : 0,
                Left: Cell.Borders.Left ? 1 : 0
                );

            TopBorder.Opacity = opacity.Top;
            RightBorder.Opacity = opacity.Right;
            BottomBorder.Opacity = opacity.Bottom;
            LeftBorder.Opacity = opacity.Left;

            if (Cell.IsRightPath) Background = new SolidColorBrush(Colors.GreenYellow);
            if (Cell.IsStart) Background = new SolidColorBrush(Colors.BlueViolet);
            if (Cell.IsEnd) Background = new SolidColorBrush(Colors.CornflowerBlue);
        }
    }
}
