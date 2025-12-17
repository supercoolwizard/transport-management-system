using Spectre.Console;
using transport_management_system.Applications.Services;
using transport_management_system.Applications.Strategies;
using transport_management_system.Infrastructure.Repositories;

namespace transport_management_system.UI;

public class RequestCli
{
    private readonly RequestFacade _facade;

    public RequestCli(RequestFacade facade)
    {
        _facade = facade;
    }

    public void Run()
    {
        var origin = AnsiConsole.Ask<string>("Enter origin city:");
        var destination = AnsiConsole.Ask<string>("Enter destination city:");

        var result = _facade.CreateRequest(origin, destination);

        var table = new Table();
        table.AddColumn("Driver");
        table.AddColumn("Vehicle");
        table.AddColumn("Distance (km)");
        table.AddColumn("Cost");

        table.AddRow(result.DriverName, result.VehicleName, result.DistanceKm.ToString(), result.TotalCost.ToString());

        AnsiConsole.Write(table);
    }
}
