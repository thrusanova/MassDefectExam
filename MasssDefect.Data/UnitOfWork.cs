using MasssDeffect.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasssDefect.Models;
using MasssDefect.Data;

namespace MasssDefect.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private MassDefectContext context;
        private IRepository<Anomaly> anomalies;
        private IRepository<Planet> planets;
        private IRepository<Person> persons;
        private IRepository<SolarSystem> solarSystems;
        private IRepository<Star> stars;

        public UnitOfWork()
        {
            this.context = new MassDefectContext();
        }

        public IRepository<Anomaly> Anomalies => this.anomalies ?? (this.anomalies = new Repository<Anomaly>(this.context.Anomalies));

        public IRepository<Person> Persons => this.persons ?? (this.persons = new Repository<Person>(this.context.Persons));

        public IRepository<Planet> Planets => this.planets ?? (this.planets = new Repository<Planet>(this.context.PLanets));

        public IRepository<SolarSystem> SolarSystems => this.solarSystems ?? (this.solarSystems = new Repository<SolarSystem>(this.context.SolarSystems));

        public IRepository<Star> Stars => this.stars ?? (this.stars = new Repository<Star>(this.context.Stars));
       
        void IUnitOfWork.Commit()
        {
            this.context.SaveChanges();
        }
    }
}
