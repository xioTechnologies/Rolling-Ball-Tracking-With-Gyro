using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace x_IMU_Ball_Tracking
{
    /// <summary>
    /// Ball tracking class. Uses Kinematic model to track planar motion of ball rolling on Earth XY plane.
    /// </summary>
    class BallTracking
    {
        private float privRadius;
        private float privSamplePeriod;
        private float[] privGyroscope;
        private float[] privRotationMatrix;
        private float[] privPosition;
        private float[] privVelocity;

        /// <summary>
        /// Gets or sets the radius of ball in meters.
        /// </summary>
        public float Radius
        {
            get
            {
                return privRadius;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new Exception("Radius must be a positive value.");
                }
                privRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the sample period in second.
        /// </summary>
        public float SamplePeriod
        {
            get
            {
                return privSamplePeriod;
            }
            set
            {
                if (value <= 0.0f)
                {
                    throw new Exception("Sample period must be a positive value.");
                }
                privSamplePeriod = value;
            }
        }

        /// <summary>
        /// Gets or sets the gyroscope measurement in radians per second.
        /// </summary>
        public float[] Gyroscope
        {
            get
            {
                return privGyroscope;
            }
            set
            {
                if (value.Length != 3) throw new Exception("Array must be of length 3.");
                privGyroscope = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation matrix describing the orientation of ball relative to surface.
        /// </summary>
        /// <remarks>
        /// Index order is row major. See http://en.wikipedia.org/wiki/Row-major_order
        /// </remarks> 
        public float[] RotationMatrix
        {
            get
            {
                return privRotationMatrix;
            }
            set
            {
                if (value.Length != 9) throw new Exception("Array must be of length 9.");
                privRotationMatrix = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of ball on surface.
        /// </summary>
        public float[] Position
        {
            get
            {
                return privPosition;
            }
            set
            {
                if (value.Length != 2) throw new Exception("Array must be of length 2.");
                privPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the velocity of ball on surface.
        /// </summary>
        public float[] Velocity
        {
            get
            {
                return privVelocity;
            }
            set
            {
                if (value.Length != 2) throw new Exception("Array must be of length 2.");
                privVelocity = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BallTracking"/> class.
        /// </summary>
        /// <param name="radius">
        /// Radius of ball in meters.
        /// </param>
        /// <param name="samplePeriod">
        /// Sample period in second.
        /// </param>
        public BallTracking(float radius, float samplePeriod)
        {
            Radius = radius;
            SamplePeriod = samplePeriod;
            RotationMatrix = new float[] { 1.0f, 0.0f, 0.0f,
                                           0.0f, 1.0f, 0.0f,
                                           0.0f, 0.0f, 1.0f };
            Position = new float[] { 0.0f, 0.0f };
            Velocity = new float[] { 0.0f, 0.0f };
        }

        /// <summary>
        /// Updates rolling ball kinematic model.
        /// </summary>
        public void Update()
        {
            // Rotate gyroscope vector to obtain body rates in Earth frame
            float[] angularVelocity = new float[] { 0.0f, 0.0f };
            angularVelocity[0] = RotationMatrix[0] * Gyroscope[0] + RotationMatrix[1] * Gyroscope[1] + RotationMatrix[2] * Gyroscope[2];
            angularVelocity[1] = RotationMatrix[3] * Gyroscope[0] + RotationMatrix[4] * Gyroscope[1] + RotationMatrix[5] * Gyroscope[2];

            // Velocity on Earth XY plane (cross product with Earth Z axis)
            Velocity[0] = angularVelocity[1] * Radius;
            Velocity[1] = -1.0f * angularVelocity[0] * Radius;

            // Update position (integrate velocity)
            Position[0] += Velocity[0] * SamplePeriod;
            Position[1] += Velocity[1] * SamplePeriod;
        }
    }
}