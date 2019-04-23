using System;


namespace TradeApp
{

    //Class which performs statistical operations on an array of data

    class Statistics
    {
            private double[] data;
            private int size;

            public Statistics(double[] data)
            {
                this.data = data;
                size = data.Length;
            }

            public double getMean()
            {
                double sum = 0.0;
                foreach (double a in data)
                    sum += a;
                return sum / size;
            }

            public double getVariance()
            {
                double mean = getMean();
                double temp = 0;
                foreach (double a in data)
                    temp += (a - mean) * (a - mean);
                return temp / (size - 1);
            }

            public double getStdDev()
            {
                return Math.Sqrt(getVariance());
            }

            public double median()
            {
                Array.Sort(data);
                if (data.Length % 2 == 0)
                    return (data[(data.Length / 2) - 1] + data[data.Length / 2]) / 2.0;
                return data[data.Length / 2];
            }

            public double simpleMovingAverage(int length)
            {
                double sum = 0.0;
                for (int i = (size-length); i<size; i++)
                    {
                        sum += data[i];
                    }
                return sum / length;
            }
    }
    }

