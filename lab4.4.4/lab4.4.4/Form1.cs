using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4._4._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Считываем значение x
                double x = double.Parse(textBoxX.Text);

                // Функция f для 4 варианта:x-2+sin(1/x)
                double f = x - 2 + Math.Sin(1/x);

                // Функция g для 4 варианта: 0.4x^12+arctg(крень третий степени(x)-x)  1-2x^7-cos(3x)
                double g = 0.4 * Math.Pow(x, 12) + Math.Atan(x) - x;

                // Выводим результаты в отдельные поля
                textBoxF.Text = f.ToString("F6");
                textBoxG.Text = g.ToString("F6");

                // Добавляем в историю вычислений
                textBoxOutput.Text += $"x = {x:F3}, f = {f:F6}, g = {g:F6}" + Environment.NewLine;
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Введите корректное число!", "Ошибка ввода");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Очистка полей вывода
            textBoxF.Text = "";
            textBoxG.Text = "";
            textBoxOutput.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация при загрузке формы
            textBoxOutput.Text = "История вычислений:" + Environment.NewLine;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}