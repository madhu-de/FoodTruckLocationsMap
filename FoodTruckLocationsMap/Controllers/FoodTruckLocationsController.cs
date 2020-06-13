using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using FoodTruckLocationsMap.Models;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodTruckLocationsMap.Controllers
{
    public class FoodTruckLocationsController : Controller
    {
        private const string FoodTruckJsonUrl = "https://data.sfgov.org/resource/rqzj-sfat.json";

        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Latitude,Longitude")] FoodTruckLocationViewModel foodTruckLocationViewModel)
        {
            var foodTruckLocations = await GetFoodTruckLocations();

            var foodTruckLocationsWithDistance = CalculateDistancesToFoodTrucks(
                foodTruckLocationViewModel.Latitude, foodTruckLocationViewModel.Longitude, foodTruckLocations);

            foodTruckLocationViewModel.FoodTruckLocations = foodTruckLocationsWithDistance.ToList();

            return View(foodTruckLocationViewModel);
        }

        private static async Task<List<FoodTruckLocation>> GetFoodTruckLocations()
        {
            List<FoodTruckLocation> foodTruckLocations = new List<FoodTruckLocation>();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync(FoodTruckJsonUrl);

            foodTruckLocations = await JsonSerializer.DeserializeAsync<List<FoodTruckLocation>>(await streamTask);
            
            return foodTruckLocations;
        }

        private static IEnumerable<FoodTruckLocation> CalculateDistancesToFoodTrucks(
            string latitude, string longitude, List<FoodTruckLocation> foodTruckLocations)
        {
            foreach (var foodTruckLocation in foodTruckLocations)
            {
                try
                {
                    var sCoord = new GeoCoordinate(double.Parse(latitude), double.Parse(longitude));
                    var eCoord = new GeoCoordinate(double.Parse(foodTruckLocation.Latitude), double.Parse(foodTruckLocation.Longitude));

                    foodTruckLocation.DistanceFromCurrentLocation = sCoord.GetDistanceTo(eCoord);
                }
                catch
                {
                    continue;
                }
            }

            foodTruckLocations?.Sort(delegate (FoodTruckLocation one, FoodTruckLocation two)
            {
                return one.DistanceFromCurrentLocation.CompareTo(two.DistanceFromCurrentLocation);
            });

            return foodTruckLocations.Take(5);
        }
    }
}