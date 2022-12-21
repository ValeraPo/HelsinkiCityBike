# Helsinki City Bike BackEnd
## Description
 This project is the pre-assignment for Solita Dev Academy Finland 2023. 
You can either work in Swagger or install [FrontEnd project](https://github.com/ValeraPo/helsinki-city-bike-front).
The application shows journeys and stations from DataBase. Data was validated before being inserted into the database. So there are not any journeys that lasted for less than ten seconds or that covered distances shorter than 10 meters.

For each journey you can see:
 - departure station
 - return station
 - covered distance in kilometers
 - duration in minutes

You can see a list of all stations with their name and address.

For each station:
- Station name
- Station address
- Total number of journeys starting from the station
- Total number of journeys ending at the station
- The average distance of a journey starting from the station
- The average distance of a journey ending at the station
- Top 5 most popular return stations for journeys starting from the station
- Top 5 most popular departure stations for journeys ending at the station
 
## Stack
The application is implemented on C#, .Net Core 6. Database on MS SQL Server. The application interacts with the DataBase by Dapper library.

## Prerequisites for starting the application
You need to have Windows 64 OS.
You need to have git. If you do not, you can [download](https://git-scm.com/download/win) it. 
Then you should install [Visual Studio](https://visualstudio.microsoft.com/vs/). 
>Note: It will not work correctly without the DataBase.

## Prerequisites for Database
In order to have the same tables and stored procedures, you need to download [backup](https://drive.google.com/file/d/1xXiCIHBPQ94Ohegh8Eans6oUDx_SKEXG/view?usp=sharing) of my database.
Then you need to have SQL 2019 server. If you do not, you can [install](https://www.microsoft.com/en-us/download/details.aspx?id=101064) it. 
After that, you need to install [SQL Server Management Studio (SSMS) 18.12.1](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16).

## Start database
Open SQL Server Management Studio. Select your server. Click `Connect`.
Then `Databases` -> `Restore Database` -> `Device` -> `…` -> `Add` -> `Choose` -> file `HelsinkiCityBike.bak` -> `OK`.
>Note: If the application cannot see the file HelsinkiCityBike.bak in Downloads, move the file to another folder.

Copy the Connection string and change `SqlConnection` in appsetting.json.
You should get a string like this: 
```C#
"ConnectionStrings": {
    "SqlConnection": "server={name of your server}; database=HelsinkiCityBike; Integrated Security=true; Encrypt=false",
  },
  ```
## Start application
To launch the backend - clone this repository.
Make sure that https://localhost:7180 is available.
Open this project in Visual Studio. 
Click this button <img width="180" alt="image3" src="https://user-images.githubusercontent.com/77934608/208941890-db092fe3-9ad0-4679-b573-9238b539e1dc.png">

Use [frontend project](https://github.com/ValeraPo/helsinki-city-bike-front) (recommended) or open https://localhost:7180/swagger/index.html to view it in the browser.

## Tests
There are three Test projects in this application for every layer. For running tests, open the project in Visual Studio, then you should click `Test` in Toolbar then `Run All Tests`. The application should be stopped if you want to test it.



## Instruction for swagger
If you did not install [frontend project](https://github.com/ValeraPo/helsinki-city-bike-front), you can use swagger.
- In order to see a list of journeys, you need to use /api/journey/{pageNo}/rowsOnPage} endpoint
<img width="800" alt="image2" src="https://user-images.githubusercontent.com/77934608/208944666-acc4e6ab-a31e-4ef8-819f-5ba1d8911f22.png">
After clicking `Execute` you will see the first ten journeys. If you want to upload more journeys, you should change `rowsOnPage`. If you want to upload the next ten journeys, you should change `pageRow`.

- In order to see a list of stations, you need to use /api/station endpoint
<img width="800" alt="image4" src="https://user-images.githubusercontent.com/77934608/208945042-220f69c8-0b10-452e-b54a-90399474b32d.png">
After clicking `Execute` you will see all names of stations with their addresses.

- In order to see more information about any station, you need to use /api/station/{name} endpoint
<img width="800" alt="image1" src="https://user-images.githubusercontent.com/77934608/208945390-52e64284-5fc1-4c27-8d2a-539654c00588.png">
After clicking `Execute` you will see:

 - Station name
 - Station address
 - Total number of journeys starting from the station
 - Total number of journeys ending at the station
 - The average distance of a journey starting from the station
 - The average distance of a journey ending at the station
 - Top 5 most popular return stations for journeys starting from the station
 - Top 5 most popular departure stations for journeys ending at the station
 
 If you write a non-existant name of a station,  you will get 400 Status Code.
