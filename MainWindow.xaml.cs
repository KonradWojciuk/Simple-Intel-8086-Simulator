using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Procesor8086
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResetInput();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ResetInput();
        }

        private void ResetInput()
        {
            Input.Text = string.Empty;
            PrintAx.Text = string.Empty;
            PrintBx.Text = string.Empty;
            PrintCx.Text = string.Empty;
            PrintDx.Text = string.Empty;
        }

        private void Switch(char inputButtonName, string data)
        {
            switch (inputButtonName)
            {
                case 'A':
                    PrintAx.Text = data;
                    Input.Text = string.Empty;
                    break;
                case 'B':
                    PrintBx.Text = data;
                    Input.Text = string.Empty;
                    break;
                case 'C':
                    PrintCx.Text = data;
                    Input.Text = string.Empty;
                    break;
                case 'D':
                    PrintDx.Text = data;
                    Input.Text = string.Empty;
                    break;
                default:
                    return;
            }
        }

        private void InputClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var data = Input.Text;
            var buttonName = button?.Name;
            var inputButtonName = char.Parse(buttonName?.Substring(5, 1) ?? string.Empty);

            Switch(inputButtonName, data);
        }

        private void RandomInput(object sender, RoutedEventArgs e)
        {
            // Random HEX
            Random random = new Random();
            var bytes = new byte[2];
            random.NextBytes(bytes);
            var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
            var hexString = string.Concat(hexArray);
            
            var button = sender as Button;
            var buttonName = button?.Name;
            var inputButtonName = char.Parse(buttonName?.Substring(11, 1) ?? string.Empty);

            Switch(inputButtonName, hexString);
        }

        private void RandomAll(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            
            var byte1 = new byte[2];
            var byte2 = new byte[2];
            var byte3 = new byte[2];
            var byte4 = new byte[2];
            
            random.NextBytes(byte1);
            random.NextBytes(byte2);
            random.NextBytes(byte3);
            random.NextBytes(byte4);

            var hexArray1 = Array.ConvertAll(byte1, x => x.ToString("X2"));
            var hexArray2 = Array.ConvertAll(byte2, x => x.ToString("X2"));
            var hexArray3 = Array.ConvertAll(byte3, x => x.ToString("X2"));
            var hexArray4 = Array.ConvertAll(byte4, x => x.ToString("X2"));

            PrintAx.Text = string.Concat(hexArray1);
            PrintBx.Text = string.Concat(hexArray2);
            PrintCx.Text = string.Concat(hexArray3);
            PrintDx.Text = string.Concat(hexArray4);
        }


        private void Move(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonName = button?.Name;
            var destinationButtonName = char.Parse(buttonName?.Substring(3, 1) ?? string.Empty);
            var sourceButtonName = char.Parse(buttonName?.Substring(5, 1) ?? string.Empty);

            var source = sourceButtonName switch
            {
                'A' => PrintAx.Text,
                'B' => PrintBx.Text,
                'C' => PrintCx.Text,
                'D' => PrintDx.Text,
                _ => ""
            };

            Switch(destinationButtonName, source);
        }


        private void XCHG(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonName = button?.Name;
            var name = char.Parse(buttonName?.Substring(6, 1) ?? string.Empty);
            var holder = "";
            
            if (buttonName != null && buttonName.Contains("AX"))
            {
                switch (name)
                {
                    case 'B':
                        holder = PrintAx.Text;
                        PrintAx.Text = PrintBx.Text;
                        PrintBx.Text = holder;
                        break;
                    case 'C':
                        holder = PrintAx.Text;
                        PrintAx.Text = PrintCx.Text;
                        PrintCx.Text = holder;
                        break;
                    case 'D':
                        holder = PrintAx.Text;
                        PrintAx.Text = PrintDx.Text;
                        PrintDx.Text = holder;
                        break;
                }
            }
            if (buttonName != null && buttonName.Contains("BX"))
            {
                switch (name)
                {
                    case 'C':
                        holder = PrintBx.Text;
                        PrintBx.Text = PrintCx.Text;
                        PrintCx.Text = holder;
                        break;
                    case 'D':
                        holder = PrintBx.Text;
                        PrintBx.Text = PrintDx.Text;
                        PrintDx.Text = holder;
                        break;
                }
            }
            if (buttonName != null && buttonName.Contains("CX"))
            {
                holder = PrintCx.Text;
                PrintCx.Text = PrintDx.Text;
                PrintDx.Text = holder;
            }
        }
    }
}