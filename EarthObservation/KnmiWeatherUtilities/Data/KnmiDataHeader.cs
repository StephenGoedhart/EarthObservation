using System;
using System.Collections.Generic;
using System.Linq;

namespace KnmiWeatherUtilities.Data
{
    public class KnmiDataHeader
    {
        public string[] Stations => knmiParameters.Stations;

        public string[] Vars => knmiParameters.Variables;

        public DateTime StartDate => knmiParameters.StartDate;

        public DateTime EndDate => knmiParameters.EndDate;

        private KnmiParameters knmiParameters;
        private Dictionary<string, string> variableDescriptions;
        private Dictionary<KnmiVariables, int> variableIndex;
        private Dictionary<DateTime, int> dateIndex;

        public KnmiDataHeader(KnmiParameters knmiParameters, Dictionary<string, string> descriptions = null)
        {
            this.knmiParameters = knmiParameters;

            variableDescriptions = descriptions;
            variableIndex = new Dictionary<KnmiVariables, int>();

            for (int i = 0; i < this.Vars.Length; i++)
            {
                if (Enum.TryParse(this.Vars[i], out KnmiVariables variable))
                {
                    variableIndex.Add(variable, i);
                }
            }
        }

        public void SetDateIndex(IEnumerable<DateTime> dates)
        {
            int idx = 0;
            dateIndex = dates.ToDictionary(date => date, date => idx++);
        }

        public int GetDateIndex(DateTime date)
        {
            if (dateIndex.TryGetValue(date, out int index))
            {
                return index;
            }

            return -1;
        }

        public int GetVariableColumn(KnmiVariables variable)
        {
            if (variableIndex.TryGetValue(variable, out int index))
            {
                return index;
            }

            return -1;
        }
    }
}
