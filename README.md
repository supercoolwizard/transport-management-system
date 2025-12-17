# Transport Management System CLI

A command-line interface (CLI) application to manage transport requests using .NET 10 and Python. The app allows users to:
* Post transport requests with origin and destination cities
* Calculate distances using a Python geolocation service `Nominatim`
* Calculate total cost of your transportation

## Getting Started
1. Clone the repository
  ```bash
  git clone https://github.com/yourusername/transport_management_system.git
  cd transport_management_system
  ```
2. Make sure docker is installed
  ```bash
  docker --version
  ```
3. Build the Docker image 
  ```bash
  docker build -t tms-cli
  ```
4. Run the CLI
  ```bash
  docker run -it tms-cli
  ```

## How it works
1. Start the CLI
2. Select **“Post a request”** or **“Exit”**
3. Enter **origin** and **destination** cities
4. The app calculates and displays:
   * Assigned **driver**
   * Assigned **vehicle**
   * **Distance (km)**
   * **Total cost** of the request
5. Choose to:
   * **Proceed with payment** –> confirms payment is completed
   * **Start a new request** –> enter new origin/destination
   * **Exit** –> close the application

**Note:** The distance is calculated using Nominatim service. Total cost is calculated using the following equation: `total_cost = distance * (cost_of_vehicle_per_km_in_usd + salary_of_driver_per_km_in_usd)`
