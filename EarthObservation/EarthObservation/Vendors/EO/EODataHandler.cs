﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthObservation.Vendors.EO
{
    class EODataHandler
    {
        public static void GetLatest()
        {
            var url = "https://eumetview.eumetsat.int/geoserv/wms?SERVICE=WMS&REQUEST=GetMap&TRANSPARENT=TRUE&EXCEPTIONS=INIMAGE&VERSION=1.3.0&LAYERS=meteosat%3Amsg_naturalenhncd&STYLES=raster&SRS=EPSG%3A4326&WIDTH=3712&HEIGHT=3712&BBOX=-76.9170259,-77,77.0829741,77&FORMAT=image%2Fgeotiff";
        }
    }
}