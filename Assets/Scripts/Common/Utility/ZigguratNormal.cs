using DecimalMath;
using System;

namespace Common
{
    public class ZigguratNormal
    {
        private readonly int R = 3;
        private readonly int N = 256;
        private readonly double[] x;
        private readonly double[] y;
        private readonly Random rng;

        private readonly decimal[] xDec;
        private readonly decimal[] yDec;


        public ZigguratNormal(int r = 3, int n = 256, Random rng = null)
        {
            R = r;
            N = n;
            x = new double[N + 1];
            y = new double[N + 1];
            xDec = new decimal[N + 1];
            yDec = new decimal[N + 1];
            this.rng = rng ?? new Random();
            InitializeTables();
        }

        private void InitializeTables()
        {
            double f = Math.Exp(-0.5 * R * R);
            x[0] = R / f;
            y[0] = f;
            x[N] = 0;
            y[N] = 1;

            decimal fDec = DecimalEx.Exp(-0.5m * R * R);
            xDec[0] = R / fDec;
            yDec[0] = fDec;
            xDec[N] = 0;
            yDec[N] = 1;

            for (int i = 1; i < N; i++)
            {
                x[i] = Math.Sqrt(-2.0 * Math.Log(y[i - 1]));
                y[i] = Math.Exp(-0.5 * x[i] * x[i]);
                xDec[i] = DecimalEx.Sqrt(-2m * DecimalEx.Log(yDec[i - 1]));
                yDec[i] = DecimalEx.Exp(-0.5m * xDec[i] * xDec[i]);
            }
        }

        public double NextGaussian()
        {
            while (true)
            {
                int i = rng.Next(N);
                double u = 2.0 * rng.NextDouble() - 1.0;
                double xVal = u * x[i];

                if (Math.Abs(xVal) < x[i + 1])
                {
                    return xVal;
                }

                if (i == 0)
                {
                    return GenerateTail((double)(u < 0 ? -R : R)) * (u < 0 ? -1 : 1);
                }

                if (y[i] + (y[i - 1] - y[i]) * rng.NextDouble() < Math.Exp(-0.5 * xVal * xVal))
                {
                    return xVal;
                }
            }
        }

        public decimal NextGaussianDecimal()
        {
            while (true)
            {
                int i = rng.Next(N);
                decimal u = 2.0m * rng.NextDecimal() - 1.0m;
                decimal xVal = u * xDec[i];

                if (Math.Abs(xVal) < xDec[i + 1])
                {
                    return xVal;
                }

                if (i == 0)
                {
                    return GenerateTail((decimal)(u < 0 ? -R : R) * (u < 0 ? -1 : 1));
                }

                if (yDec[i] + (yDec[i - 1] - yDec[i]) * rng.NextDecimal() < DecimalEx.Exp(-0.5m * xVal * xVal))
                {
                    return xVal;
                }
            }
        }

        private double GenerateTail(double r)
        {
            double x;
            double y;
            do
            {
                x = -Math.Log(rng.NextDouble()) / r;
                y = -Math.Log(rng.NextDouble());
            } while (y + y < x * x);

            return r + x;
        }

        private decimal GenerateTail(decimal r)
        {
            decimal x;
            decimal y;
            do
            {
                x = -DecimalEx.Log(rng.NextDecimal()) / r;
                y = -DecimalEx.Log(rng.NextDecimal());
            } while (y + y < x * x);

            return r + x;
        }
    }
}
