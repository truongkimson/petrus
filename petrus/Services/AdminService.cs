using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using petrus.Data;
using petrus.Models;
using petrus.Resources;

namespace petrus.Services
{
    public class AdminService : IAdminService
    {
        private readonly petrusDb _context;

        public AdminService(petrusDb context)
        {
            _context = context;
        }

        public ChartData GetAvgAdoptionSpeed()
        {
            var random = new Random();
            var currMonth = DateTime.Today.Month;

            var data = new List<int>();
            var labels = new List<string>();
            for (int i = 5; i >= 0; i--)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currMonth - i));
                data.Add(random.Next(20));
            }

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public int GetCurrMonthAdoptionSpeed()
        {
            return new Random().Next(20);
        }

        public ChartData GetAdoptionsBySpecies()
        {
            var random = new Random();
            var labels = new List<String>() { "Dog", "Cat", "Others" };
            var data = new List<int>() { random.Next(50), random.Next(50), random.Next(50) };

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public ChartData GetAdoptionsByMonth()
        {
            var currMonth = DateTime.Today.Month;
            var data = new List<int>();
            var labels = new List<string>();
            for (int i = 5; i >= 0; i--)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currMonth - i));
                data.Add(50);
            }

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public int GetCurrMonthAdoptions()
        {
            return new Random().Next(20);
        }

        public IEnumerable<AdoptionData> GetLatestAdoptions()
        {
            return new List<AdoptionData>()
            {
                new AdoptionData()
                {
                    name = "Karlee",
                    species = "Dog",
                    age = 2,
                    breed = "Husky",
                    adopter_name = "Kim",
                    email = "sontks@gmail.com"
                }
            };
        }

        public IEnumerable<ListingData> GetSlowAdoptionListings()
        {
            return new List<ListingData>()
            {
                new ListingData()
                {
                    name = "Karlee",
                    species = "Dog",
                    age = 2,
                    breed = "Husky",
                    days_elapsed = 14
                }
            };
        }
    }
}
