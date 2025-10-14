using System.Windows.Forms;
using System;

namespace lab4._4._4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox textBoxX;
        private TextBox textBoxF;
        private TextBox textBoxG;
        private TextBox textBoxOutput;
        private Button buttonCalculate;
        private Button buttonClear;
        private Label label1, label2, label3, label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxF = new System.Windows.Forms.TextBox();
            this.textBoxG = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(120, 20);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(100, 22);
            this.textBoxX.TabIndex = 0;
            // 
            // textBoxF
            // 
            this.textBoxF.Location = new System.Drawing.Point(120, 50);
            this.textBoxF.Name = "textBoxF";
            this.textBoxF.ReadOnly = true;
            this.textBoxF.Size = new System.Drawing.Size(100, 22);
            this.textBoxF.TabIndex = 1;
            // 
            // textBoxG
            // 
            this.textBoxG.Location = new System.Drawing.Point(120, 80);
            this.textBoxG.Name = "textBoxG";
            this.textBoxG.ReadOnly = true;
            this.textBoxG.Size = new System.Drawing.Size(100, 22);
            this.textBoxG.TabIndex = 2;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(23, 193);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(300, 150);
            this.textBoxOutput.TabIndex = 3;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(250, 20);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculate.TabIndex = 4;
            this.buttonCalculate.Text = "Вычислить";
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(250, 50);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Введите x:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "f(x) =";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "g(x) =";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "История вычислений:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(730, 406);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.textBoxF);
            this.Controls.Add(this.textBoxG);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Лабораторная работа 1 - Вариант 4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}