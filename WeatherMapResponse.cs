using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetter_App
{
    public class WeatherMapResponse
    {
        public Main main;
        public List<Weather> weather;
    }
}
