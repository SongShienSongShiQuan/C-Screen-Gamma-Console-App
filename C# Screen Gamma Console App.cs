using System.Collections;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
public class SET_GAMMA_PROGRAM_1_to_256
{

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

    public static void SetGamma(int gamma)
    {
        if (gamma <= 256 && gamma >= 1)
        {
            RAMP Gammma_Ramp = new RAMP();
            Gammma_Ramp.Red = new ushort[256];
            Gammma_Ramp.Green = new ushort[256];
            Gammma_Ramp.Blue = new ushort[256];
            for (int i = 1; i < 256; i++)
            {
                int iArrayValue = i * (gamma + 256);

                if (iArrayValue > 65535)
                    iArrayValue = 65535;
                Gammma_Ramp.Red[i] = Gammma_Ramp.Blue[i] = Gammma_Ramp.Green[i] = (ushort)iArrayValue;
            }
            SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref Gammma_Ramp);
        }
    }

    public static void Main(string[] args)
    {
        string string_input = "INPUT";
        int gamma_to_int = 0;
        while (string_input != "EXIT")
        {
            Console.WriteLine("Enter gamma value from 1 to 256: ");
            string_input = Console.ReadLine();
            try
            {
                Console.WriteLine("Type \"EXIT\" as gamma value to exit.");
                gamma_to_int = int.Parse(string_input);
                if (gamma_to_int <= 256 )
                    SetGamma(gamma_to_int);
                else if (gamma_to_int >= 257)
                    Console.WriteLine("ERROR: Got \"" + string_input + "\" as parameters or not EXIT. \nType an integer value 1 to 256 or \"EXIT\" only.");
            }
            catch
            {
                Console.WriteLine("ERROR: Got \""+string_input+"\" as parameters or not EXIT. \nType an integer value 1 to 256 or \"EXIT\" only.");
            }
        }
    }
}
