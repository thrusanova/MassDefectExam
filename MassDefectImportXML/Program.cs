
using MasssDefect.Data;
using MasssDefect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MassDefectImportXML
{
    class Program
    {
        private const string anomaliesPath = "../../../datasets/new-anomalies.xml";
        private static string Error = "Error: Invalid data.";

        static void Main(string[] args)
        {
            var context = new MassDefectContext();
            context.Database.Initialize(true);
            context.SaveChanges();
            ImportNewAnomalies(context);
        }

        private static void ImportNewAnomalies(MassDefectContext context)
        {
            var xml = XDocument.Load(anomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");
            foreach (var anomaly in anomalies)
            {
                ImportNewAnomalies(anomaly, context);
            }
        }

        private static void ImportNewAnomalies(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanet = anomalyNode.Attribute("origin-planet");
            var teleportPlanet = anomalyNode.Attribute("teleport-planet");
            if (originPlanet==null || teleportPlanet==null)
            {
                Console.WriteLine(Error);
                return;
            }
            var anomaly = new Anomaly
            {
                OriginPlanet = GetPlanetByName(originPlanet.Value, context),
                TeleportPlanet = GetPlanetByName(teleportPlanet.Value, context)
            };
            if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
            {
                Console.WriteLine(Error);
                return;
            }
            context.Anomalies.Add(anomaly);
            Console.WriteLine("Successfully imported anomaly.");
            var victims = anomalyNode.XPathSelectElements("victims/victim");
            foreach (var vic in victims)
            {
                ImportVictims(vic, context,anomaly);
            }
            context.SaveChanges();

        }

        private static void ImportVictims(XElement vicNode, MassDefectContext context, Anomaly anomaly)
        {
            var name = vicNode.Attribute("name");
            if (name==null)
            {
                Console.WriteLine(Error);
                return;
            }
            var personEntity = GetPersonByName(name.Value, context);
            if (personEntity == null)
            {
                Console.WriteLine(Error);
                return;
            }
            anomaly.Victims.Add(personEntity);
        }

        private static Person GetPersonByName(string value, MassDefectContext context)
        {
            foreach (var person in context.Persons)
            {
                if (person.Name == value)
                {
                    return person;
                }
            }

            return null;
        }

        private static Planet GetPlanetByName(string planetName, MassDefectContext context)
        {
            foreach (var planet in context.PLanets)
            {
                if (planet.Name == planetName)
                {
                    return planet;
                }
            }

            return null;
        }
    }
}
