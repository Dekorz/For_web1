using System.Reflection;
using WebApplication1.Models;
using static System.Int32;
using Microsoft.Extensions.Logging;

namespace WebApplication1;

public class WebApplication1CsvFileStorage : IWebApplication1Storage
{
        private static readonly IEqualityComparer<string> collation = StringComparer.OrdinalIgnoreCase;

        private readonly Dictionary<string, Architect> architects = new Dictionary<string, Architect>(collation);
        private readonly Dictionary<string, Building> buildings = new Dictionary<string, Building>(collation);
        private readonly ILogger<WebApplication1CsvFileStorage> logger;

        public WebApplication1CsvFileStorage(ILogger<WebApplication1CsvFileStorage> logger) {
            this.logger = logger;
            ReadArchitectsFromCsvFile("architects.csv");
            ReadBuildingsFromCsvFile("buildings.csv");
            //ResolveReferences();
        }

        //private void ResolveReferences() {
        //    foreach (var mfr in manufacturers.Values) {
        //        mfr.Models = models.Values.Where(m => m.ManufacturerCode == mfr.Code).ToList();
        //        foreach (var model in mfr.Models) model.Manufacturer = mfr;
        //    }

        //    foreach (var model in models.Values) {
        //        model.Vehicles = vehicles.Values.Where(v => v.ModelCode == model.Code).ToList();
        //        foreach (var vehicle in model.Vehicles) vehicle.VehicleModel = model;
        //    }
        //}

        private string ResolveCsvFilePath(string filename) {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var csvFilePath = Path.Combine(directory, "csv-data");
            return Path.Combine(csvFilePath, filename);
        }

        private void ReadArchitectsFromCsvFile(string filename) {
            var filePath = ResolveCsvFilePath(filename);
            foreach (var line in File.ReadAllLines(filePath)) {
                var tokens = line.Split(",");
                var architect = new Architect {
                    Name = tokens[0],
                    Email = tokens[1]
                };
            }
            logger.LogInformation($"Loaded {architects.Count} models from {filePath}");
        }

        private void ReadBuildingsFromCsvFile(string filename) {
            var filePath = ResolveCsvFilePath(filename);
            foreach (var line in File.ReadAllLines(filePath)) {
                var tokens = line.Split(",");
                var building = new Building {
                    Address = tokens[0],
                    Architect = tokens[2],
                };
            if (TryParse(tokens[1], out var year)) building.Year = year;
            buildings[building.Address] = building;
        }
            logger.LogInformation($"Loaded {buildings.Count} models from {filePath}");
        }

        public int CountBuildings() => buildings.Count;

        public IEnumerable<Architect> ListArchitects() => architects.Values;

        public IEnumerable<Building> ListBuildings() => buildings.Values;


        public Building FindBuilding(string address) => buildings.GetValueOrDefault(address);


        //public void CreateVehicle(Vehicle vehicle) {
        //    vehicle.VehicleModel.Vehicles.Add(vehicle);
        //    vehicle.ModelCode = vehicle.VehicleModel.Code;
        //    UpdateVehicle(vehicle);
        //}

        //public void UpdateVehicle(Vehicle vehicle) {
        //    vehicles[vehicle.Registration] = vehicle;
        //}

        //public void DeleteVehicle(Vehicle vehicle) {
        //    var model = FindModel(vehicle.ModelCode);
        //    model.Vehicles.Remove(vehicle);
        //    vehicles.Remove(vehicle.Registration);
        //}
    }