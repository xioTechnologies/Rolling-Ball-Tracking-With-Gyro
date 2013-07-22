using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace x_IMU_Ball_Tracking
{
    class Program
    {
        /// <summary>
        /// BallTracking object to calculate rolling ball kinematics.
        /// </summary>
        static BallTracking ballTracking;

        /// <summary>
        /// Form_3Dcuboid object to display 3D cuboid graphics.
        /// </summary>
        static Form_3Dcuboid form_3Dcuboid;

        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">
        /// Unused.
        /// </param>
        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString());
            try
            {
                // Create ballTracking object
                ballTracking = new BallTracking(0.03f, 1.0f / 256.0f);

                // Connect to x-IMU
                Console.WriteLine("Searching for x-IMU...");
                x_IMU_API.PortAssignment[] portAssignment = (new x_IMU_API.PortScanner(true, true)).Scan();
                x_IMU_API.xIMUserial xIMUserial = new x_IMU_API.xIMUserial(portAssignment[0].PortName);
                xIMUserial.CalInertialAndMagneticDataReceived += new x_IMU_API.xIMUserial.onCalInertialAndMagneticDataReceived(xIMUserial_CalInertialMagneticDataReceived);
                xIMUserial.QuaternionDataReceived += new x_IMU_API.xIMUserial.onQuaternionDataReceived(xIMUserial_QuaternionDataReceived);
                xIMUserial.Open();
                Console.WriteLine("Connected to x-IMU " + portAssignment[0].DeviceID + " on " + portAssignment[0].PortName + ".");

                // Send 'algorithm initialise' command
                Console.WriteLine("Sending 'algorithm initialise' command...");
                xIMUserial.SendCommandPacket(x_IMU_API.CommandCodes.AlgorithmInitialise);

                // Running graphics
                Console.WriteLine("Running 3D Cuboid form...");
                form_3Dcuboid = new Form_3Dcuboid(new float[] { 0.06f, 0.04f, 0.02f }, Form_3Dcuboid.CameraViews.Top, 3.0f);
                form_3Dcuboid.WindowState = FormWindowState.Maximized;
                form_3Dcuboid.ShowDialog();
                form_3Dcuboid = null;

                // Closing form enables mouse control mode.
                Console.WriteLine("Mouse control active.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Calibrated inertial/magnetic data received event to update objects.
        /// </summary>
        static void xIMUserial_CalInertialMagneticDataReceived(object sender, x_IMU_API.CalInertialAndMagneticData e)
        {
            ballTracking.Gyroscope = new float[] { (e.Gyroscope[0] / 360) * (float)(2 * Math.PI), (e.Gyroscope[1] / 360) * (float)(2 * Math.PI), (e.Gyroscope[2] / 360) * (float)(2 * Math.PI) };
            ballTracking.Update();
            if (form_3Dcuboid != null)
            {
                form_3Dcuboid.TranslationVector = new float[] { ballTracking.Position[0], ballTracking.Position[1], 0.0f };
                form_3Dcuboid.RotationMatrix = ballTracking.RotationMatrix;
            }
            else
            {
                SendInputClass.MouseEvent((int)(SendInputClass.MOUSEEVENTF.MOVE), (int)(ballTracking.Velocity[0] * 25.0), (int)(ballTracking.Velocity[1] * -25.0f), 0);
                if (Math.Sqrt(e.Accelerometer[0] * e.Accelerometer[0] + e.Accelerometer[1] * e.Accelerometer[1] + e.Accelerometer[2] * e.Accelerometer[2]) > 5.0)       // if accelerometer magnitude > 5 g
                {
                    SendInputClass.MouseEvent((int)SendInputClass.MOUSEEVENTF.LEFTDOWN, 0, 0, 0);
                    SendInputClass.MouseEvent((int)SendInputClass.MOUSEEVENTF.LEFTUP, 0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Quaternion data received event to update rolling ball orientation data.
        /// </summary>
        static void xIMUserial_QuaternionDataReceived(object sender, x_IMU_API.QuaternionData e)
        {
            ballTracking.RotationMatrix = e.ConvertToRotationMatrix();
        }
    }
}