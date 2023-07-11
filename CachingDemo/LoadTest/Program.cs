
using NBomber.CSharp;

var httpClient=new HttpClient();


var users = GenFu.GenFu.ListOf<User>();

var scenario = Scenario.Create("test_api_scenario", async context =>
{
    var name = users[GenFu.GenFu.Random.Next(25)].Name;
    await httpClient.GetAsync($"http://localhost:8002/search?name={name}");

    if(GenFu.GenFu.Random.Next(30) % 29 == 0)
        await httpClient.GetAsync($"http://localhost:8002/add?name={name}");

    return Response.Ok();
})
.WithLoadSimulations(
    Simulation.Inject(rate: 10,
                      interval: TimeSpan.FromSeconds(1),
                      during: TimeSpan.FromSeconds(30)),
    Simulation.Pause(during: TimeSpan.FromSeconds(5))
)

;

NBomberRunner
    .RegisterScenarios(scenario)    
    .Run();

class User
{
    public string Name { get; set; }
}