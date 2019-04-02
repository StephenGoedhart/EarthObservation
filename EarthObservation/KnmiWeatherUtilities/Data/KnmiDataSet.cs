using System;
using System.Linq;

namespace KnmiWeatherUtilities.Data
{
    public class KnmiDataSet
    {
        private KnmiDataHeader header;
        
        private double[][] data;

        public KnmiDataSet(KnmiDataHeader header, double[][] data = null)
        {
            this.header = header;
            this.data = data;
        }

        public double[] GetDataBetween(KnmiVariables variable, DateTime start, DateTime end)
        {
            double[] result = null;

            int startIndex = header.GetDateIndex(start);
            int endIndex = header.GetDateIndex(end);
            int length = endIndex - startIndex + 1;

            if (length > 0)
            {
                double[] data = GetData(variable);
                result = new double[length];
                Array.Copy(data, startIndex, result, 0, length);
            }

            return result;
        }

        public double[] GetData(KnmiVariables variable)
        {
            int col = header.GetVariableColumn(variable);
            return data[col];
        }

        public void SetData(double[][] data)
        {
            if (data.Length != this.header.Vars.Length)
            {
                return;
            }

            this.data = data;
        }

        public double AverageOf(KnmiVariables variable)
        {
            return GetData(variable).Average();
        }
    }
}
