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
        bool exitApp = false;

        while (!exitApp)
        {
            // main menu
            var mainChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option:")
                    .AddChoices(new[] { "Post a request", "Exit" })
            );

            if (mainChoice == "Exit")
            {
                exitApp = true;
                continue;
            }

            // post a request
            var origin = AnsiConsole.Ask<string>("Enter origin city:");
            var destination = AnsiConsole.Ask<string>("Enter destination city:");

            try
            {
            var result = _facade.CreateRequest(origin, destination);

            var table = new Table();
            table.AddColumn("Driver");
            table.AddColumn("Vehicle");
            table.AddColumn("Distance (km)");
            table.AddColumn("Cost (USD)");

            table.AddRow(result.DriverName, result.VehicleName, result.DistanceKm.ToString("0.00"), result.TotalCost.ToString("0.00"));

            AnsiConsole.Write(table);
            
            bool requestProcessed = false;
            while (!requestProcessed)
                {
                    var nextChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What do you want to do next?")
                            .AddChoices(new[] { "Proceed with payment", "New request", "Exit" })
                    );

                    switch (nextChoice)
                    {
                        case "Proceed with payment":
                            AnsiConsole.MarkupLine("[green]Payment completed successfully![/]");
                            requestProcessed = true;
                            break;
                        case "New request":
                            requestProcessed = true;  // breaks inner loop to start new request
                            break;
                        case "Exit":
                            exitApp = true;
                            requestProcessed = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }
        
        AnsiConsole.MarkupLine("[bold blue]Goodbye![/]");
    }
}
