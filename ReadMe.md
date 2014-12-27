# Threepio #
A C# library for accessing the Star Wars API at [http://swapi.co/](http://swapi.co/) (It's written in C#, and it translates things from JSON into .Net objects - hence Threepio)

## Usage ##
Available classes are:  
    Film  
    Character  
    Species  
    Planet  
    Starship  
    Vehicle

All classes have two static methods:  
    Get(int id)   
    GetPage(int pageNumber = 1)

