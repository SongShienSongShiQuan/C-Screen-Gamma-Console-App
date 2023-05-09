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
        public static class Global_Variables
        {
            public static int iArrayValue_High = 65535;
            public static int iArrayValue_Low = 40535;
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
        }
        public void gamma_add(object sender, RoutedEventArgs e)
        {
            string input_int_gamma = InputIntBox_Gamma.Text;
            int input_int_gamma_to_int = int.Parse(input_int_gamma);
            int add_gamma_value = (input_int_gamma_to_int + 50);
            string add_gamma_value_to_str = add_gamma_value.ToString();
            InputIntBox_Gamma.Text = add_gamma_value_to_str;
            label_a5_sub_1.Text = ("VALUE: " + add_gamma_value_to_str);
            if (add_gamma_value > 250)
            {
                InputIntBox_Gamma.Text = "256";
                label_a5_sub_1.Text = ("VALUE: " + "256");
                SetGamma_High(add_gamma_value);
            }
            else if (add_gamma_value < 256)
            {
                SetGamma(add_gamma_value);
                if (add_gamma_value < 1)
                {
                    InputIntBox_Gamma.Text = "1";
                    label_a5_sub_1.Text = ("VALUE: " + "1");
                }
                else if (add_gamma_value > 1)
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
            if (sub_gamma_value > 250)
            {
                InputIntBox_Gamma.Text = "256";
                label_a5_sub_1.Text = ("VALUE: " + "256");
                SetGamma_High(sub_gamma_value);
            }
            else if (sub_gamma_value < 256)
            {
                SetGamma(sub_gamma_value);
                if (sub_gamma_value < 1)
                {
                    InputIntBox_Gamma.Text = "1";
                    label_a5_sub_1.Text = ("VALUE: " + "1");
                }
                else if (sub_gamma_value > 1)
                {
                    SetGamma(sub_gamma_value);
                }
            }
        }
    }
}
