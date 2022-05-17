using WebApplication1.Models;

namespace WebApplication1;

public interface IWebApplication1Storage
{
    public int CountBuildings();
    public IEnumerable<Architect> ListArchitects();
    public IEnumerable<Building> ListBuildings();

    public Building FindBuilding(string registration);

    //public void CreateVehicle(Vehicle vehicle);
    //public void UpdateVehicle(Vehicle vehicle);
    //public void DeleteVehicle(Vehicle vehicle);
}