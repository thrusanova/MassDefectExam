using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasssDefect.Models
{
   public class Anomaly

    {
        public Anomaly()
        {
            this.Victims = new HashSet<Person>();
        }

        public int Id { get; set; }

        public virtual int? TeleportPlanetId { get; set; }
        public virtual Planet TeleportPlanet { get; set; }
        
        public virtual int? OriginPlanetId { get; set; }
        public virtual Planet OriginPlanet { get; set; }

        public virtual ICollection<Person>Victims { get; set; }

    }
}
