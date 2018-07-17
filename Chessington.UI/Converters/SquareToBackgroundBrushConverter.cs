using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Chessington.UI.ViewModels;

namespace Chessington.UI.Converters
{
    /// <summary>
    /// Given a ChessSquare object, returns a Brush which can be used to colour that square's border.
    /// </summary>
    public class SquareToBackgroundBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(randomColor());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static Random rnd = new Random();
        private Color randomColor()
        {
            Byte[] b = new Byte[3];
            rnd.NextBytes(b);
            var squareColor = new Color();
            squareColor = Color.FromRgb(b[0], b[1], b[2]);
            return squareColor;
        }
    }
}