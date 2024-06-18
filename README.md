# Garage Management System

This project is a Garage Management System implemented in C#. It allows for managing different types of vehicles, including cars, motorcycles, and trucks. The system supports both fuel and electric vehicles, and provides features for adding vehicles, managing their properties, and updating their status.

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [License](#license)

## Features

- Add and manage various types of vehicles (cars, motorcycles, trucks).
- Support for both fuel and electric power sources.
- Detailed management of vehicle properties and status.
- Dynamic generation of property input prompts for vehicles.

## Usage

After setting up the project, you can create and manage vehicles through the provided classes and methods. The system allows you to:

1. Create different types of vehicles (car, motorcycle, truck) using a factory method.
2. Set properties for each vehicle, including wheels, power source, and unique attributes.
3. Add a vehicle to the garage and manage its status through a garage ticket system.

## Project Structure

The project is organized into several main components:

- **Vehicle**: An abstract class representing a general vehicle. Concrete vehicle classes inherit from this class to define specific types of vehicles.
- **Car, Motorcycle, Truck**: Classes representing different types of vehicles, each with specific properties and methods.
- **PowerSource**: An abstract class for different power sources. Concrete classes such as Fuel and Electric inherit from this class.
- **VehicleCreator**: A factory class for creating vehicles. It provides a method to create a vehicle based on its type.
- **GarageTicket**: A class representing a ticket for a vehicle in the garage. It includes the vehicle's owner information and its status in the garage.
- **UniquePropertyDetailsBuilder**: A helper class for generating dynamic input prompts for vehicle properties based on their types.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
