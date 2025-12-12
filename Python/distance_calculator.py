import sys

from geopy.distance import geodesic as GD
from geopy.geocoders import Nominatim

from exceptions import CityNotFoundException, CityNotInUkraineException, InvalidCityTypeException

class DistanceCalculator:
    def __init__(self, city1, city2):
        if not isinstance(city1, str):
            raise InvalidCityTypeException(f"city1 must be a string, got {type(city1).__name__}")
        if not isinstance(city2, str):
            raise InvalidCityTypeException(f"city2 must be a string, got {type(city2).__name__}")

        self.city1 = city1
        self.city2 = city2
        self.distance = self.distance_calculator(self.city1, self.city2)
    
    def city_to_longitude(self, city1, city2):
        geolocator = Nominatim(user_agent="MyApp")

        location_city1 = geolocator.geocode(city1, addressdetails=True, country_codes="UA")
        location_city2 = geolocator.geocode(city2, addressdetails=True, country_codes="UA")

        if not location_city1 or not location_city2:
            raise CityNotFoundException("One of the cities was not found")
        
        country1 = location_city1.raw.get("address", {}).get("country_code", "").upper()
        country2 = location_city2.raw.get("address", {}).get("country_code", "").upper()

        if country1 != "UA":
            raise CityNotInUkraineException(f"{city1} exists but is not in Ukraine")
        if country2 != "UA":
            raise CityNotInUkraineException(f"{city2} exists but is not in Ukraine")
        
        lat_long_city1 = (location_city1.latitude, location_city1.longitude)
        lat_long_city2 = (location_city2.latitude, location_city2.longitude)

        return lat_long_city1, lat_long_city2
    
    def distance_calculator(self, city1, city2):
        city1_coord, city2_coord = self.city_to_longitude(city1, city2)
        distance = GD(city1_coord, city2_coord).km

        return distance

if __name__ == "__main__":
    city1 = sys.argv[1]
    city2 = sys.argv[2]

    distance_between_city1_city2 = DistanceCalculator(city1, city2)
    
    print(distance_between_city1_city2.distance)
