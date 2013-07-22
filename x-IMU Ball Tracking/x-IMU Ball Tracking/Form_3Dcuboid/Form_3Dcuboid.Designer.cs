namespace x_IMU_Ball_Tracking
{
    partial class Form_3Dcuboid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleOpenGlControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // simpleOpenGlControl
            // 
            this.simpleOpenGlControl.AccumBits = ((byte)(0));
            this.simpleOpenGlControl.AutoCheckErrors = false;
            this.simpleOpenGlControl.AutoFinish = false;
            this.simpleOpenGlControl.AutoMakeCurrent = true;
            this.simpleOpenGlControl.AutoSwapBuffers = true;
            this.simpleOpenGlControl.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl.ColorBits = ((byte)(32));
            this.simpleOpenGlControl.DepthBits = ((byte)(16));
            this.simpleOpenGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl.Name = "simpleOpenGlControl";
            this.simpleOpenGlControl.Size = new System.Drawing.Size(600, 480);
            this.simpleOpenGlControl.StencilBits = ((byte)(0));
            this.simpleOpenGlControl.TabIndex = 0;
            this.simpleOpenGlControl.Load += new System.EventHandler(this.simpleOpenGlControl_Load);
            this.simpleOpenGlControl.SizeChanged += new System.EventHandler(this.simpleOpenGlControl_SizeChanged);
            this.simpleOpenGlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl_Paint);
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form_3Dcuboid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.Controls.Add(this.simpleOpenGlControl);
            this.Name = "Form_3Dcuboid";
            this.Text = "3D Cuboid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_3Dcuboid_FormClosing);
            this.Load += new System.EventHandler(this.Form_3Dcuboid_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl;
        private System.Windows.Forms.Timer timer;
    }
}