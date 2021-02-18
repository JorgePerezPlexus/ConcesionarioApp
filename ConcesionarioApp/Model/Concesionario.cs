using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcesionarioApp.Model
{
    public class Concesionario
    {
        public int ID { get; set; }
        public string nombre { get; set; }
        public int direccionID { get; set; }
        public int stockID { get; set; }
    }
}
