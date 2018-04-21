using GrovePi;
using GrovePi.I2CDevices;
using GrovePi.Sensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GABIoT2018
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;

        // Connect the Button to digital port 2
        IButtonSensor button = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin2);
        int brightness = 0;
        ILed Led1 = DeviceFactory.Build.Led(Pin.DigitalPin3);
        // Connect the Buzzer to digital port 5
        IBuzzer buzzer = DeviceFactory.Build.Buzzer(Pin.DigitalPin4);
        ILightSensor light1 = DeviceFactory.Build.LightSensor(Pin.AnalogPin0);

        IRgbLcdDisplay display = DeviceFactory.Build.RgbLcdDisplay();
        IUltrasonicRangerSensor distance1 = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin7);

        public MainPage()
        {
            this.InitializeComponent();

            this.Setup();
        }

        private async void Setup()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += this.OnTick;
            this.timer.Start();
        }

        private void OnTick(object sender, object e)
        {
            try
            {
                // Check the value of the button.

                string buttonon = button.CurrentState.ToString();
                // bool buttonison = buttonon.Equals("On", StringComparison.OrdinalIgnoreCase);
                System.Diagnostics.Debug.WriteLine("Button is " + buttonon);

            }
            catch (Exception ex)
            {
                // NOTE: There are frequent exceptions of the following:
                // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                // This appears to be caused by the rapid frequency of writes to the GPIO
                // These are being swallowed here/

                // If you want to see the exceptions uncomment the following:
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            try
            {
                System.Diagnostics.Debug.WriteLine("Brightness: " + brightness.ToString());

                // Check the brightness, if it's going to overflow, reset it.
                if (brightness > 250)
                {
                    brightness = 0;
                }

                // Increase the brightness by 5 points.
                brightness = brightness + 5;
                // Write the values to the three LEDs.
                // USA!  Red, White, and Blue!
                Led1.AnalogWrite(Convert.ToByte(brightness));

            }

            catch (Exception ex)
            {
                // NOTE: There are frequent exceptions of the following:
                // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                // This appears to be caused by the rapid frequency of writes to the GPIO
                // These are being swallowed here/

                // If you want to see the exceptions uncomment the following:
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            try
            {
                // Check the value of the button.

                string buttonon = button.CurrentState.ToString();
                bool buttonison = buttonon.Equals("On", StringComparison.OrdinalIgnoreCase);

                // Check the state of the buzzer.  This is just to output to debug!
                SensorStatus status = buzzer.CurrentState;
                bool buzzeron = status.ToString().Equals("On", StringComparison.OrdinalIgnoreCase);

                // Print out Diagnostics.
                System.Diagnostics.Debug.WriteLine("Button is " + buttonon);
                System.Diagnostics.Debug.WriteLine("Buzzer is " + status.ToString());

                // If the Button is on . . . .
                if (buttonison)
                {
                    buzzer.ChangeState(GrovePi.Sensors.SensorStatus.On);
                }
                else
                {
                    buzzer.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                }
                try
                {
                    // Check the value of the button, turn it into a string.
                    string sensorvalue = light1.SensorValue().ToString();
                    System.Diagnostics.Debug.WriteLine("light is " + sensorvalue);

                }
                catch (Exception ex)
                {
                    // NOTE: There are frequent exceptions of the following:
                    // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                    // This appears to be caused by the rapid frequency of writes to the GPIO
                    // These are being swallowed here/

                    // If you want to see the exceptions uncomment the following:
                    // System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

            }
            catch (Exception ex)
            {
                // NOTE: There are frequent exceptions of the following:
                // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                // This appears to be caused by the rapid frequency of writes to the GPIO
                // These are being swallowed here/

                // If you want to see the exceptions uncomment the following:
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            //Task.Delay(100).Wait();     // Delay 0.1 second
                                        // We need to make sure we delay here.  If we don't, we won't be able to read
                                        // the LCD Screen.
            try
            {
                // First, output to the LCD Display.
                display.SetText("Light:"+light1.SensorValue()).SetBacklightRgb(255, 50, 255);
                // Then output to the debug window.
                System.Diagnostics.Debug.WriteLine("Hello from Dexter Industries!");

            }
            catch (Exception ex)
            {
                // NOTE: There are frequent exceptions of the following:
                // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                // This appears to be caused by the rapid frequency of writes to the GPIO
                // These are being swallowed here/

                // If you want to see the exceptions uncomment the following:
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            try
            {
                // Check the value of the Ultrasonic Sensor
                string sensorvalue = distance1.MeasureInCentimeters().ToString();
                System.Diagnostics.Debug.WriteLine("Ultrasonic reads " + sensorvalue);

            }
            catch (Exception ex)
            {
                // NOTE: There are frequent exceptions of the following:
                // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                // This appears to be caused by the rapid frequency of writes to the GPIO
                // These are being swallowed here/

                // If you want to see the exceptions uncomment the following:
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }
    }
}
