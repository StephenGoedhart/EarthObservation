using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace KnmiWeatherUtilities.Data
{
    public class KnmiDataRequestParser: IDataConverter<string, KnmiDataSet>
    {
        private KnmiParameters parameters;
        public KnmiDataRequestParser(KnmiParameters parameters)
        {
            this.parameters = parameters;
        }

        public KnmiDataSet Convert(string input)
        {
            KnmiDataHeader header = new KnmiDataHeader(parameters);
            KnmiDataSet dataset = CreateDataSet(input, header);
            return dataset;
        }

        private KnmiDataSet CreateDataSet(string input, KnmiDataHeader header)
        {
            KnmiDataSet dataSet = new KnmiDataSet(header);
            StringReader sr = new StringReader(input);

            string line = string.Empty;

            List<double>[] data = Enumerable.Range(0, parameters.Variables.Length).Select(i => new List<double>())
                .ToArray();
            List<DateTime> dates = new List<DateTime>();

            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }

                string[] values = line.Split(',').Select(s => s.Trim()).ToArray();

                DateTime date = DateTime.ParseExact(values[1], "yyyyMMdd", CultureInfo.InvariantCulture);
                dates.Add(date);
                for (int i = 0; i < parameters.Variables.Length; i++)
                {
                    int intVal = int.Parse(values[i + 2]);
                    double realValue = ((double) intVal) / 10;
                    data[i].Add(realValue);
                }
            }

            dataSet.SetData(data.Select(l => l.ToArray()).ToArray());
            header.SetDateIndex(dates);
            return dataSet;
        }
    }
}
