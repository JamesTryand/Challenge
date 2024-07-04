gitignore added from https://gist.github.com/kmorcinek/2710267

skeleton builds succesfully ```dotnet build .\challenge.sln```


skeleton test harness works ``` dotnet test .\challenge.sln```


skeleton console app runs successfully ```dotnet run --project .\LendingPlatformApp\LendingPlatformApp.csproj```

now for the design... 

time is of the essence. 

* a simple repl for the app.
* a load decision class
* small metrics classes for reporting
* tests over the decision
* if time tests over the metrics


Event & Command are simply there to indicate structure

Initially build up the structure and workflow