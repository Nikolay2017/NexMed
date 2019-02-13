using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestNexMed.Models
{
    public class ModelWeather
    {
        public class Weather
        {
            public string icon { get; set; }
            public string code { get; set; }
            public string description { get; set; }
        }

        public class Datum
        {
            public string wind_cdir { get; set; }
            public int rh { get; set; }
            public string pod { get; set; }
            public double pres { get; set; }
            public string timezone { get; set; }
            public Weather weather { get; set; }
            public string country_code { get; set; }
            public int clouds { get; set; }
            public int ts { get; set; }
            public int solar_rad { get; set; }
            public string state_code { get; set; }
            public double wind_spd { get; set; }
            public double lat { get; set; }
            public string wind_cdir_full { get; set; }
            public int slp { get; set; }
            public int vis { get; set; }
            public double lon { get; set; }
            public string sunrise { get; set; }
            public string datetime { get; set; }
            public int h_angle { get; set; }
            public double dewpt { get; set; }
            public int snow { get; set; }
            public int uv { get; set; }
            public int wind_dir { get; set; }
            public double elev_angle { get; set; }
            public int ghi { get; set; }
            public int dhi { get; set; }
            public int dni { get; set; }
            public string city_name { get; set; }
            public int precip { get; set; }
            public string sunset { get; set; }
            public double temp { get; set; }
            public string station { get; set; }
            public string ob_time { get; set; }
            public double app_temp { get; set; }
        }

        public class RootObject
        {
            public List<Datum> data { get; set; }
            public int count { get; set; }
        }
    }
}