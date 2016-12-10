using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasssDefect.Models
{
   public class Planet
    {
        public Planet()
        {
            this.People = new List<Person>();
            this.OriginOfAnomalies = new HashSet<Anomaly>();
            this.TeleportOfAnomalies = new List<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SunId { get; set; }
        public virtual Star Sun { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Anomaly> OriginOfAnomalies { get; set; }

        public virtual ICollection<Anomaly> TeleportOfAnomalies { get; set; }
    }
}
