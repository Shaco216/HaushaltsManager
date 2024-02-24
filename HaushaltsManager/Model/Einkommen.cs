using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsManager.Model
{
    public class Einkommen
    {
        public long Id { get; set; }
        public int PersonId { get; set; }
        public int Jahr {  get; set; }
        public string Name { get; set; }
        public double Wert { get; set; }
        public int KategorieId { get; set; }
    }
}
