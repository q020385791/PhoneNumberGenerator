namespace PhoneNumberGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumberCount = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.btnRunGenerate = new System.Windows.Forms.Button();
            this.labMessage = new System.Windows.Forms.Label();
            this.chkSequential = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtNumberCount
            // 
            this.txtNumberCount.Location = new System.Drawing.Point(73, 34);
            this.txtNumberCount.Name = "txtNumberCount";
            this.txtNumberCount.Size = new System.Drawing.Size(100, 23);
            this.txtNumberCount.TabIndex = 0;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 37);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(55, 15);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "產出數量";
            // 
            // btnRunGenerate
            // 
            this.btnRunGenerate.Location = new System.Drawing.Point(300, 37);
            this.btnRunGenerate.Name = "btnRunGenerate";
            this.btnRunGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnRunGenerate.TabIndex = 2;
            this.btnRunGenerate.Text = "執行";
            this.btnRunGenerate.UseVisualStyleBackColor = true;
            this.btnRunGenerate.Click += new System.EventHandler(this.btnRunGenerate_Click);
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Location = new System.Drawing.Point(12, 9);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(31, 15);
            this.labMessage.TabIndex = 3;
            this.labMessage.Text = "訊息";
            // 
            // chkSequential
            // 
            this.chkSequential.AutoSize = true;
            this.chkSequential.Location = new System.Drawing.Point(179, 38);
            this.chkSequential.Name = "chkSequential";
            this.chkSequential.Size = new System.Drawing.Size(110, 19);
            this.chkSequential.TabIndex = 4;
            this.chkSequential.Text = "平均化隨機產出";
            this.chkSequential.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 68);
            this.Controls.Add(this.chkSequential);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.btnRunGenerate);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.txtNumberCount);
            this.Name = "Form1";
            this.Text = "電話號碼產生器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtNumberCount;
        private Label labelCount;
        private Button btnRunGenerate;
        private Label labMessage;
        private CheckBox chkSequential;
    }
}