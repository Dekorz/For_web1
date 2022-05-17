using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public class Building
{
    public string Address { get; set; }
    public int Year { get; set; }
    public string Architect { get; set; }

}