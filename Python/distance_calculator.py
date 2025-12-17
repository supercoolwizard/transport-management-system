import sys

from geopy.distance import geodesic as GD
from geopy.geocoders import Nominatim

from geopy.exc import GeocoderTimedOut, GeocoderServiceError
from requests.exceptions import ReadTimeout, ConnectionError

from exceptions import CityNotFoundException, CityNotInUkraineException

class DistanceCalculator:
    def __init__(self, city1, city2):
        self.city1 = city1
        self.city2 = city2
        self.distance = self.distance_calculator(self.city1, self.city2)
    
    def city_to_longitude(self, city1, city2):
        geolocator = Nominatim(
            user_agent="TransportManagementSystem/1.0 (contact: support@myapp.local)",
            timeout=10
        )

        def resolve_city(city):
            location = geolocator.geocode(city, addressdetails=True)

            if not location:
                raise CityNotFoundException(f"{city} was not found")

            address = location.raw.get("address", {})

            # Ensure it is actually a city/town/village
            if not any(k in address for k in ("city", "town", "village")):
                raise CityNotFoundException(f"{city} was not found")

            country = address.get("country_code", "").upper()
            if country != "UA":
                raise CityNotInUkraineException(f"{city} exists but is not in Ukraine")

            return (location.latitude, location.longitude)
        
        return resolve_city(city1), resolve_city(city2)
    
    def distance_calculator(self, city1, city2):
        city1_coord, city2_coord = self.city_to_longitude(city1, city2)
        distance = GD(city1_coord, city2_coord).km

        return distance

if __name__ == "__main__":
    try:
        city1 = sys.argv[1]
        city2 = sys.argv[2]

        distance = DistanceCalculator(city1, city2).distance
        print(distance)
        sys.exit(0)

    except CityNotFoundException as e:
        print(f"CITY_NOT_FOUND|{e}", file=sys.stderr)
        sys.exit(21)

    except CityNotInUkraineException as e:
        print(f"CITY_NOT_IN_UKRAINE|{e}", file=sys.stderr)
        sys.exit(22)



    except (GeocoderTimedOut, ReadTimeout) as e:
        print("GEOCODER_TIMEOUT|Geocoding service did not respond in time", file=sys.stderr)
        sys.exit(30)

    except (GeocoderServiceError, ConnectionError) as e:
        print("GEOCODER_UNAVAILABLE|Geocoding service is unavailable", file=sys.stderr)
        sys.exit(31)



    except Exception as e:
        print(f"UNKNOWN_ERROR|{e}", file=sys.stderr)
        sys.exit(1)