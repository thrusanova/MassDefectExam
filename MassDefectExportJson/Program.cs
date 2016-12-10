using MasssDefect.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefectExportJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();
            ExportPlanetWhichAreNotAnomalyOrigins(context);
            ExportPeopleWhichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var exportedAnomaly = context.Anomalies
                 .OrderByDescending(anomaly => anomaly.Victims.Count)
                 .Take(1)
                 .Select(anomaly => new
                 {
                     id = anomaly.Id,
                     originPlanet = new
                     {
                         name = anomaly.OriginPlanet.Name
                     },
                     teleportPlanet = new
                     {
                         name = anomaly.TeleportPlanet.Name
                     },
                     victimsCount = anomaly.Victims.Count
                 });
            var anomalyASJson = JsonConvert.SerializeObject(exportedAnomaly, Formatting.Indented);
            File.WriteAllText("..//..//..//ExportedResults//anomalies.json", anomalyASJson);
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            var people = context.Persons.Where(p => p.Anomalies.Count == 0)
                .Select(person =>new
                {
                    name=person.Name,
                    homePlanet=new
                    {
                        name=person.HomePlanet.Name
                    }
                });
            var peopleASJson = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText("..//..//..//ExportedResults//people.json", peopleASJson);
        }

        private static void ExportPlanetWhichAreNotAnomalyOrigins(MassDefectContext contxet)
        {
            var exportedPlanets = contxet.PLanets.Where(pl => pl.OriginOfAnomalies.Count==0)
               .Select(p => new
               {
                   name=p.Name
               });
            var planetASJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("..//..//..//ExportedResults//exportedPlanets.json", planetASJson);
        }
    }
}
