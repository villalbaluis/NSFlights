# NSFlights

Technical test for Backend Developer in Flyr (New Shore)


This test consisted of creating a solution for a travel and flight system, consuming NS's own API as a resource.

In this solution, we have implemented an [N-layer](https://reactiveprogramming.io/blog/es/estilos-arquitectonicos/capas) architecture, which in this case was mainly based on 3 layers.

* NSFlightsAPI
* NSFlightsBusiness
* NSFlightsDataAccess
* Where each of them fulfills a specific role.


In the **NSFlightsAPI** layer, we have the initial functionality of our API created, which will be responsible for calling the dependencies necessary to resolve the problem, as well as returning the response from the user interaction.

In the **NSFlightsBusiness** layer we have all the logic of the flights and trips, with their respective consumption of the provided API, and their structural organization by folders, according to the need of the solution or project.

    We find in this layer an order of the elements according to the classification of the type of function that each one fulfills, having in this layer everything related to the DTOs (Models), API Clients, Entities, Services, which are consumed by the other layers of the solution.

And finally, in the **NSFlightsDataAccess** layer we have all the database management, through the Entitie Framework ORM to make the work and modeling of the database easier.


For this solution a few packages have been used, such as:

Entitie Framework 8.0.2 (And several dependencies thereof, such as Core, Tools, SQL Server)

NewSoftJSON 13.0.2


SQL Server was used as a database engine, where its connection string is registered in the file: ***NSFlightsAPI/appsettings.json*** in the ***ConnectionStrings/SqlServerConnect*** section. (Don't forget to change the "Initial Catalog" for your database.)

---

This solution was made with so much effort by [Luis Villalba](https://www.linkedin.com/in/villalbaluiz/ "LinkedIn Profile").
