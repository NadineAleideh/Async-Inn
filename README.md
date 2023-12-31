## Project : API server for AsyncInn Hotel Asset Management System :house_with_garden:

### Author: Nadine Aleideh :cancer:


| Lab  |  | Date     |
| ---- |   -----    |  -----   |
| 11  | Databases and ERDs | 16/7/2023  |
| 12  | Intro to Entity Framework | 19/7/2023 |
| 13  | Dependency Injection | 23/7/2023      |
| 14  | Navigation Properites | 26/7/2023      |
| 16  | DTOs | 31/7/2023      |
| 17  | Testing and Swagger and Deployment | 5/8/2023      |
| 18  | Identity | 8/8/2023      |
| 19  | JWT authorize, claims and roles | 8/8/2023      |

## Description

The Async Inn Hotel Asset Management System is a web-based API designed to help Async Inn, a local hotel chain, better manage their hotel assets across multiple locations. This project aims to provide a RESTful API server that allows the management of rooms, amenities, and new hotel locations. The system leverages a relational database to store and maintain the integrity of the data.


## ERD Diagram

![Async Inn ERD](./assets/ERDhotel.png)

Explanation of the Tables:

1. **Hotel**:
   - Represents each hotel location with its unique identifier, name, city, state, address, and phone number.
   - Attributes: location_id (PK), name, city, state, address, phone_number

2. **Room**:
   - Represents the room it self and has a room id , nickname and a layout Enum.
   - Attributes: room_id (PK), nickname.
   - The room has a relationship with two tables it is one to many with Rooms because this room is one room in many rooms in the hotel and many to many with amenities because many rooms can have many of amenities and many amenities, can be assigned for many rooms.

3. **HotelRooms**:
   - Joint entity table with payload between two tables and it is many to one with hotel and many to one with Room.
   - room number it's a composite key between the room id and hotel location id
   - Attributes: room_number (CK), Location_id (CK, FK), room_id (CK, FK), price, pet_freindly.
   - It's a relationship between two tables it has payload attributes like a price and it is many to one with hotel and many to one with Room so in each Hotel we have an unique number of room number and it is composite key between the room id and hotel location id.

4. **Amenity**:
   - Represents the various amenities that rooms can have, such as air conditioning, coffee maker, etc.
   - Attributes: amenity_id (PK), name
   - Amenity table has a relation with room and it is many to many.

5. **RoomAmenities**:
   - Acts as a pure join table to establish a many-to-many relationship between rooms and amenities.
   - Attributes: room_id (PK, FK), amenity_id (PK, FK)

## Schema

![Async Inn Schema](./assets/s1.PNG)
![Async Inn Schema](./assets/s2.PNG)
![Async Inn Schema](./assets/s3.PNG)

## Identity

ASP.NET Core Identity was created to help with the security and management of users. It provides this abstraction layer between the application and the users/role data. We can use the API in it�s entirety, or just bits and pieces as we need (such as the salting/hashing by itself) or email services. There is a lot of flexibility within ASP.NET Core Identity. We have the ability to take or leave whatever we want. Identity combines well with EFCore and SQL Server.

### Register a user

![Async Inn register](./assets/register1.PNG)
![Async Inn register](./assets/register2.PNG)

### Login user

![Async Inn login](./assets/login1.PNG)
![Async Inn login](./assets/login2.PNG)


## JWT authorize, claims and roles

I added an JWT service in order to create a token for any user when he register or sign in and added a jwt secret json file in the configuration file to encrypt it, then I added an authorization in the program file to make the policy take the claims that seeds in the dbcontext. Also, I seed 4 roles in the database and each one has own claims policy.

In the controller I modified the routes :

- The district manager can make full CRUD operation on all routes and also can create accounts for all roles
- The poperty manager can add read update the hotel rooms to hotel and the amineties to room, and also can make a agent account only
- The Agent role can update and add hotelroom to hotel and add/ delete amenites to room and thats all
- The annyoums user can only read from all routes


## architecture 

- :heavy_check_mark: 3 esstinal models (Hotels, Rooms, and Amenities) and 2 other models to represent thr M-M relations in the ERD.
- :heavy_check_mark: 5 Interfaces for every model.
- :heavy_check_mark: service for each of the controllers that implement the appropriate interface.
- :heavy_check_mark: CRUD operations for evry class.
- :heavy_check_mark: I Update the Controller to use the appropriate method from the interface rather than the DBContext directly.
- :heavy_check_mark: I Update the code to use the DTOs principle.

## API Routes

![Async Inn](./assets/Lab14routes.png)

## API Requests (CRUD operations)

![Async Inn](./assets/GetRooms.PNG)
![Async Inn](./assets/GetRoom.PNG)
![Async Inn](./assets/GetAmenities.PNG)
![Async Inn](./assets/GetAmenity.PNG)
![Async Inn](./assets/PostHotel.PNG)
![Async Inn](./assets/DeleteHotel.PNG)

## API Requests to show the DTOs effect

- https://localhost:7082/api/Hotels

![Async Inn](./assets/hotels.png)

- https://localhost:7082/api/Hotels/1

![Async Inn](./assets/hotel.png)

- https://localhost:7082/api/Hotels/1/Rooms

![Async Inn](./assets/hotelrooms.png)

https://localhost:7082/api/Rooms

![Async Inn](./assets/Rooms.png)

https://localhost:7082/api/Rooms/1

![Async Inn](./assets/room.png)

- https://localhost:7082/api/amenities

![Async Inn](./assets/amenities.png)

