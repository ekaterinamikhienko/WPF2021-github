using System;

class Program
{
    public static void Main(string[] args)
    {
        const double g = 9.806;//ускорение свободного падения
        double V0 = Convert.ToDouble(Console.ReadLine());//скорость
        double a = Convert.ToDouble(Console.ReadLine());//угол
        double t = (2 * V0 * Math.Sin(a)) / g;//время полёта
        double[] X = new double[4];
        double[] Y = new double[4];
        double[] T = new double[4];
        double dT = t / 3;
        //double t1=0;
        int k = 0;
        for (double t1 = 0; t1 < Math.Abs(t + dT / 2); t1 += dT)
        {
            //for(int k=0;k<4;k++){
            X[k] = V0 * Math.Cos(a) * t1;
            Y[k] = V0 * Math.Sin(a) * t1 - (g * t1 * t1) / 2;
            T[k] = t1;
            k++;
            //t1+=dT;
        }
        for (int n = 0; n < 4; n++)
        {
            Console.WriteLine("Время:{0}; Координаты:({1},{2})", Math.Round(T[n]), Math.Round(X[n]), Math.Round(Y[n]));
        }
    }
}
