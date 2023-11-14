# What is CPUCheckr?

CPUCheckr is simple **two layer CRUD API** written in [.NET6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). Using this app you can find out the specifications of a particular processor.

The database is being used is [MariaDb](https://mariadb.org/) and [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) as ORM.

# How To start the solution?

Type the following command:

~~~
docker-compose up -d
~~~

It will start the required infrastructure using Docker in the background. Then you can start actual application using your IDE or CLI:

~~~
cd CPUCheckr.Api
dotnet run
~~~

# Can I start solution without Docker?

Yes, you can run app using any other MariaDb provider like [xampp](https://www.apachefriends.org/pl/index.html).

Just edit connection string on **CPUCheckr.Api/appsettings.json** to a proper one. Then run solution as above.

# What is the API port?

API will run on **https://localhost:7286** and **http://localhost:5239**.

# What request can be sent to the API?

You can find list of all API endpoints and HTTP requests on https://localhost:7286/swagger/index.html.
