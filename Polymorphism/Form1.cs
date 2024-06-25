using System;
using System.Drawing;
using System.Windows.Forms;

namespace Polymorphism
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создаем тестовые уравнения
            Equation sineEquation = new SineEquation(2.0);
            Equation absoluteSineEquation = new AbsoluteSineEquation(3.0);

            // Рисуем графики
            DrawFunction(-10.0, 10.0, sineEquation, panel1);
            DrawFunction(-10.0, 10.0, absoluteSineEquation, panel2);
        }

        private void DrawFunction(double x1, double x2, Equation equation, Panel panel)
        {
            // Создаем bitmap для рисования
            Bitmap bitmap = new Bitmap(panel.Width, panel.Height);
            Graphics g = Graphics.FromImage(bitmap);

            // Настраиваем систему координат
            g.TranslateTransform(panel.Width / 2, panel.Height / 2);
            g.ScaleTransform(panel.Width / 20.0f, -panel.Height / 20.0f);

            // Рисуем график
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, (float)x1, 0, (float)x2, 0);
            g.DrawLine(Pens.Black, 0, (float)x1, 0, (float)x2);

            for (double x = x1; x <= x2; x += 0.1)
            {
                float y = (float)equation.Evaluate(x);
                g.DrawLine(Pens.Black, (float)x, 0, (float)x, y);
            }

            // Отображаем bitmap на панели
            panel.CreateGraphics().DrawImage(bitmap, 0, 0);
        }
    }

    // Абстрактный класс Equation
    public abstract class Equation
    {
        public abstract double Evaluate(double x);
    }

    // Производный класс для уравнения sin(a*x)/x
    public class SineEquation : Equation
    {
        private readonly double a;
        public SineEquation(double a)
        {
            this.a = a;
        }
        public override double Evaluate(double x)
        {
            if (x == 0)
                return 0;
            else
                return Math.Sin(a * x) / x;
        }
    }

    // Производный класс для уравнения a*x*|sin(x)|
    public class AbsoluteSineEquation : Equation
    {
        private readonly double a;
        public AbsoluteSineEquation(double a)
        {
            this.a = a;
        }
        public override double Evaluate(double x)
        {
            return a * x * Math.Abs(Math.Sin(x));
        }
    }
}
