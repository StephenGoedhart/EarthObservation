using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace EarthObservation.Vendors.EO
{
    /// <summary>
    /// Earth Observation Event Handler. Use this class to start the gathering of satellite imagery. 
    /// </summary>
    class EODataHandler
    {
        static Thread background = new Thread(new ThreadStart(GetLatest));
        static bool busy;
        /// <summary>
        /// Start gathering satellite imagery on a new thread. 
        /// </summary>
        public static void Start()
        {
            busy = true;
            background.IsBackground = true;
            background.Start();
        }
        public static void Stop()
        {
            background.Abort();
        }

        private static void GetLatest()
        {
            var url = "https://eumetview.eumetsat.int/geoserv/wms?SERVICE=WMS&REQUEST=GetMap&TRANSPARENT=TRUE&EXCEPTIONS=INIMAGE&VERSION=1.3.0&LAYERS=meteosat%3Amsg_naturalenhncd&STYLES=raster&SRS=EPSG%3A4326&WIDTH=3712&HEIGHT=3712&BBOX=-76.9170259,-77,77.0829741,77&FORMAT=image%2Fgeotiff";
            var web = new WebClient();
            var time = DateTime.Now.ToString("yyyyMMddTHHmm");
            web.DownloadFile(url, "../../Models/EO/Eumetsat"+time+".tif");            
        }
    }
}
