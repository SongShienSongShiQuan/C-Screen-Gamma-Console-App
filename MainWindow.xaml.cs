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
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
using System.Reflection.Emit;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;

namespace Mako_Gamma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int iArrayValue_Default_Global;
        Window1 new_Int_White_Filter = new Window1();
        public int int_Window1 = 0;
        public static class Global_Variables
        {
            public static int iArrayValue_High = 65535;
            public static int iArrayValue_Low = 35535;
        }
        public void Initialize_White_Filter()
        {
            if (new_Int_White_Filter.IsVisible == false)
            {
                new_Int_White_Filter.Show();
            }
            else if (new_Int_White_Filter.IsVisible == true)
            {
                new_Int_White_Filter.Visibility = Visibility.Hidden;
            }    
        }
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            this.Top = 0;
            this.Left = 0;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Blue;
        }
        public void SetGamma_High(int gamma)
        {
            if (gamma <= 256 && gamma >= 1)
            {
                RAMP Gammma_Ramp = new RAMP();
                Gammma_Ramp.Red = new ushort[256];
                Gammma_Ramp.Green = new ushort[256];
                Gammma_Ramp.Blue = new ushort[256];

                iArrayValue_Default_Global = Global_Variables.iArrayValue_Low;
                string iArrayValue_Default_Global_to_str = iArrayValue_Default_Global.ToString();
                label_a5_sub_3.Text = ("Array: " + iArrayValue_Default_Global_to_str);
                for (int i = 1; i < 256; i++)
                {
                    int iArrayValue = i * (gamma + 1000);

                    if (iArrayValue > iArrayValue_Default_Global)
                        iArrayValue = iArrayValue_Default_Global;
                    Gammma_Ramp.Red[i] = Gammma_Ramp.Blue[i] = Gammma_Ramp.Green[i] = (ushort)iArrayValue;
                }
                SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref Gammma_Ramp);
            }
        }
        public void SetGamma(int gamma)
        {
            if (gamma <= 256 && gamma >= 1)
            {
                RAMP Gammma_Ramp = new RAMP();
                Gammma_Ramp.Red = new ushort[256];
                Gammma_Ramp.Green = new ushort[256];
                Gammma_Ramp.Blue = new ushort[256];

                iArrayValue_Default_Global = Global_Variables.iArrayValue_High;
                string iArrayValue_Default_Global_to_str = iArrayValue_Default_Global.ToString();
                label_a5_sub_3.Text = ("Array: "+iArrayValue_Default_Global_to_str);
                for (int i = 1; i < 256; i++)
                {
                    int iArrayValue = i * (gamma + 256);

                    if (iArrayValue > iArrayValue_Default_Global)
                        iArrayValue = iArrayValue_Default_Global;
                    Gammma_Ramp.Red[i] = Gammma_Ramp.Blue[i] = Gammma_Ramp.Green[i] = (ushort)iArrayValue;
                }
                SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref Gammma_Ramp);
            }
        }
        public void gamma_set_value(object sender, RoutedEventArgs e)
        {
            string input_int_gamma = InputIntBox_Gamma.Text;
            label_a5_sub_1.Text = ("VALUE: " + input_int_gamma);
            int input_int_gamma_to_int = int.Parse(input_int_gamma);
            SetGamma(input_int_gamma_to_int);
            if (input_int_gamma_to_int >= 256)
            {
                InputIntBox_Gamma.Text = "256";
                label_a5_sub_1.Text = ("VALUE: " + "256");
                SetGamma_High(input_int_gamma_to_int);
                Gamma_add_.Visibility = Visibility.Hidden;
                Initialize_White_Filter();
                Gamma_sub_.Visibility = Visibility.Visible;
            }
            else if (input_int_gamma_to_int < 255)
            {
                SetGamma(input_int_gamma_to_int);
                Gamma_add_.Visibility = Visibility.Visible;
                if (input_int_gamma_to_int < 0)
                {
                    InputIntBox_Gamma.Text = "0";
                    label_a5_sub_1.Text = ("VALUE: " + "0");
                    new_Int_White_Filter.Visibility = Visibility.Hidden;
                    Gamma_sub_.Visibility = Visibility.Hidden;
                }
                else if (input_int_gamma_to_int > 0)
                {
                    SetGamma(input_int_gamma_to_int);
                }
            }
        }
        public void gamma_add(object sender, RoutedEventArgs e)
        {
            string input_int_gamma = InputIntBox_Gamma.Text;
            int input_int_gamma_to_int = int.Parse(input_int_gamma);
            int add_gamma_value = (input_int_gamma_to_int + 50);
            string add_gamma_value_to_str = add_gamma_value.ToString();
            InputIntBox_Gamma.Text = add_gamma_value_to_str;
            label_a5_sub_1.Text = ("VALUE: " + add_gamma_value_to_str);
            if (add_gamma_value >= 256)
            {
                InputIntBox_Gamma.Text = "256";
                label_a5_sub_1.Text = ("VALUE: " + "256");
                SetGamma_High(add_gamma_value);
                Gamma_add_.Visibility = Visibility.Hidden;
                Initialize_White_Filter();
                Gamma_sub_.Visibility = Visibility.Visible;
            }
            else if (add_gamma_value < 255)
            {
                SetGamma(add_gamma_value);
                Gamma_add_.Visibility = Visibility.Visible;
                if (add_gamma_value < 0)
                {
                    InputIntBox_Gamma.Text = "0";
                    label_a5_sub_1.Text = ("VALUE: " + "0");
                    new_Int_White_Filter.Visibility = Visibility.Hidden;
                    Gamma_sub_.Visibility = Visibility.Hidden;
                }
                else if (add_gamma_value > 0)
                {
                    SetGamma(add_gamma_value);
                }
            }
        }
        public void gamma_sub(object sender, RoutedEventArgs e)
        {
            string input_int_gamma = InputIntBox_Gamma.Text;
            int input_int_gamma_to_int = int.Parse(input_int_gamma);
            int sub_gamma_value = (input_int_gamma_to_int - 50);
            string sub_gamma_value_to_str = sub_gamma_value.ToString();
            InputIntBox_Gamma.Text = sub_gamma_value_to_str;
            label_a5_sub_1.Text = ("VALUE: " + sub_gamma_value_to_str);
            if (sub_gamma_value >= 256)
            {
                InputIntBox_Gamma.Text = "256";
                label_a5_sub_1.Text = ("VALUE: " + "256");
                SetGamma_High(sub_gamma_value);
                Gamma_add_.Visibility = Visibility.Hidden;
                Initialize_White_Filter();
                Gamma_sub_.Visibility = Visibility.Visible;
            }
            else if (sub_gamma_value < 255)
            {
                SetGamma(sub_gamma_value);
                Gamma_add_.Visibility = Visibility.Visible;
                if (sub_gamma_value < 0)
                {
                    InputIntBox_Gamma.Text = "0";
                    label_a5_sub_1.Text = ("VALUE: " + "0");
                    new_Int_White_Filter.Visibility = Visibility.Hidden;
                    Gamma_sub_.Visibility = Visibility.Hidden;
                }
                else if (sub_gamma_value > 0)
                {
                    SetGamma(sub_gamma_value);
                }
            }
        }
        private void Clean_up_process(object sender, RoutedEventArgs e)
        {
            var process_start_w_command = new ProcessStartInfo();
            process_start_w_command.FileName = "EXIT_PROGRAM.bat";
            process_start_w_command.UseShellExecute = true;
            process_start_w_command.RedirectStandardOutput = false;
            process_start_w_command.CreateNoWindow = false;
            Process.Start(process_start_w_command);
        }
    }
}
