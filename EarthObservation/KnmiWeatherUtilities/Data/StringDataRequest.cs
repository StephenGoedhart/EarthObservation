using System.Net.Http;
using System.Threading.Tasks;

namespace KnmiWeatherUtilities.Data
{
    public class StringDataRequest
    {
        private string baseUrl;
        private HttpClient httpClient;
        private int timeOut;

        public void SetTimeOutSeconds(int seconds)
        {
            timeOut = seconds * 1000;
        }

        public StringDataRequest(string baseUrl = "http://projects.knmi.nl/klimatologie/daggegevens/getdata_dag.cgi")
        {
            this.baseUrl = baseUrl;
            httpClient = new HttpClient();
            SetTimeOutSeconds(60);
        }

        public T ExecuteRequest<T>(IDataConverter<string, T> dataConverter, HttpContent requestContent = null)
        {
            Task<HttpResponseMessage> response = httpClient.PostAsync(this.baseUrl, requestContent);
            response.Wait(timeOut);
            HttpResponseMessage responseMessage = response.Result;
            Task<string> readTask = responseMessage.Content.ReadAsStringAsync();
            readTask.Wait(timeOut);
            return dataConverter.Convert(readTask.Result);
        }
    }
}
