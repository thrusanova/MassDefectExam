using MassDefect.DTO;
using MasssDefect.Data;
using MasssDefect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefectCodeFirst
{
    public class ExportJSON
    {
        private const string starPath = "../../../datasets/stars.json";
        private const string solarSystemPath = "../../../datasets/solar-systems.json";
        private const string planetPath = "../../../datasets/planets.json";
        private const string anomaliesPath = "../../../datasets/anomalies.json";
        private const string personPath = "../../../datasets/persons.json";
        private static string Error = "Error: Invalid data.";

        static void Main(string[] args)
        {
            //ImportSolarSystem();
            //ImportStars();
            //ImportPLanets();
            //   ImportPersons();
            ImportAnomalies();
        }

        private static void ImportAnomalies()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(anomaliesPath);
            var anomEntities = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);
            foreach (var anom in anomEntities)
            {
                if (anom.OriginPlanet == null || anom.TeleportPlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                var anomalyEntiry = new Anomaly
                {
                    OriginPlanet = GetPlanetByName(anom.OriginPlanet, context),
                    TeleportPlanet = GetPlanetByName(anom.TeleportPlanet, context)
                };
                if (anom.OriginPlanet==null || anom.TeleportPlanet==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                context.Anomalies.Add(anomalyEntiry);
                Console.WriteLine($"Successfully imported anomaly {anom.OriginPlanet} , {anom.TeleportPlanet}.");
            }
            context.SaveChanges();
        }
    
        private static void ImportPersons()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(personPath);
            var personEntities = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);
            foreach (var person in personEntities)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                var personEntity = new Person
                {
                    Name = person.Name,
                    HomePlanet = GetPlanetByName(person.HomePlanet, context)
                };
                if (person.HomePlanet==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                context.Persons.Add(personEntity);
                Console.WriteLine($"Successfully imported person {person.Name}.");
            }
            context.SaveChanges();
        }

        private static Planet GetPlanetByName(string homePlanet, MassDefectContext context)
        {
              foreach (var pl in context.PLanets)
            {
                if (pl.Name == homePlanet)
                {
                    return pl;
                }
            }
            return null;
        }
    

        private static void ImportPLanets()
      {
            var context = new MassDefectContext();
            var json = File.ReadAllText(planetPath);
            var planetEntities = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);
            foreach (var planet in planetEntities)
            {
                if (planet.Name==null || planet.Sun==null || planet.SolarSystem==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                var planetEntity = new Planet
                {
                    Name = planet.Name,
                    Sun = GetStarByName(planet.Sun, context),
                    SolarSystem = GetSolarSystemByName(planet.SolarSystem, context)
                };
                if (planetEntity.SolarSystem==null || planetEntity.Sun==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                context.PLanets.Add(planetEntity);
                Console.WriteLine($"Successfully imported Planet {planetEntity.Name}.");

            }
            context.SaveChanges();
        }
        private static void ImportStars()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(starPath);
            var starsEntities = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);
            foreach (var star in starsEntities)
            {
                if (star.Name==null || star.SolarSystem==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                var starEntity = new Star
                {
                    Name = star.Name,
                    SolarSystem = GetSolarSystemByName(star.SolarSystem, context)
                };
                if (starEntity.SolarSystem.Name==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                context.Stars.Add(starEntity);
                Console.WriteLine($"Successfully imported Star {starEntity.Name}.");
            }
            context.SaveChanges();
        }

        private static SolarSystem GetSolarSystemByName(string solarSystemName, MassDefectContext context)
        {
            foreach (var solarSystem in context.SolarSystems)
            {
                if (solarSystem.Name==solarSystemName)
                {
                    return solarSystem;
                }
            }
            return null;
        }
        private static Star GetStarByName(string starName, MassDefectContext context)
        {
            foreach (var sun in context.Stars)
            {
                if (sun.Name==starName)
                {
                    return sun;
                }
            }
            return null;
        }

        private static void ImportSolarSystem()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(solarSystemPath);
            var solarSystemEntities = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);
            foreach (var ss in solarSystemEntities)
            {
                if (ss.Name==null)
                {
                    Console.WriteLine(Error);
                    continue;
                }
                var solarsSystem = new SolarSystem
                {
                    Name = ss.Name
                };
                context.SolarSystems.Add(solarsSystem);
                Console.WriteLine($"Successfully imported Solar System {solarsSystem.Name}.");
            }
            context.SaveChanges();  
        }
    }
}
