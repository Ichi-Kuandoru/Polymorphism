using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Polymorphism
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Add("sin(a*x)/x");
            chart1.Series.Add("a*x*|sin(x)|");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Абстрактный класс Equation
        public abstract class Equation
        {
            public abstract double Evaluate(double x);
        }

        // Производный класс SineEquation
        public class SineEquation : Equation
        {
            private double a;

            public SineEquation(double a)
            {
                this.a = a;
            }

            public override double Evaluate(double x)
            {
                return Math.Sin(a * x) / x;
            }
        }

        // Производный класс ModulusSineEquation
        public class ModulusSineEquation : Equation
        {
            private double a;

            public ModulusSineEquation(double a)
            {
                this.a = a;
            }

            public override double Evaluate(double x)
            {
                return a * x * Math.Abs(Math.Sin(x));
            }
        }

        // Использование классов
        private void button1_Click_1(object sender, EventArgs e)
        {
            double Xmin = double.Parse(textBoxXmin.Text);
            double Xmax = double.Parse(textBoxXmax.Text);
            double Step = double.Parse(textBoxStep.Text);

            // Количество точек графика
            int count = (int)Math.Ceiling((Xmax - Xmin) / Step) + 1;

            // Массив значений X – общий для обоих графиков
            double[] x = new double[count];

            // Два массива Y – по одному для каждого графика
            double[] y1 = new double[count];
            double[] y2 = new double[count];

            // Создаем объекты классов уравнений
            Equation sineEquation = new SineEquation(2.0);
            Equation modulusSineEquation = new ModulusSineEquation(3.0);

            // Расчитываем точки для графиков функции
            for (int i = 0; i < count; i++)
            {
                // Вычисляем значение X
                x[i] = Xmin + Step * i;

                // Вычисляем значения Y для каждого графика
                y1[i] = sineEquation.Evaluate(x[i]);
                y2[i] = modulusSineEquation.Evaluate(x[i]);
            }

            // Отрисовываем графики
            chart1.ChartAreas[0].AxisX.Minimum = Xmin;
            chart1.ChartAreas[0].AxisX.Maximum = Xmax;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Step;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].Points.DataBindXY(x, y1);
            chart1.Series[1].Points.DataBindXY(x, y2);
        }

    }
}