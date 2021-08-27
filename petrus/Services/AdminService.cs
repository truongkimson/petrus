using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server;
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
            var currDateTime = DateTime.Now;
            var startDate = currDateTime.AddMonths(-5);

            var monthList = new List<DateTime>();
            for (int i = 5; i >= 0; i--)
            {
                var prevMonth = currDateTime.AddMonths(-i);
                monthList.Add(new DateTime(prevMonth.Year, prevMonth.Month, 1));
            }

            var res = _context.AdoptionRequests
                .Where(r => r.OutcomeDateTime >= new DateTime(startDate.Year, startDate.Month, 1)
                            && r.requestStatus.Equals(RequestStatus.Accepted))
                .Select(r => new
                {
                    r.OutcomeDateTime,
                    AdoptionSpeed = r.GetAdoptionSpeed()
                }).AsEnumerable().GroupBy(r => new
                {
                    r.OutcomeDateTime.Year,
                    r.OutcomeDateTime.Month
                }).ToDictionary(p => p.Key,
                    p => p.Average(e => e.AdoptionSpeed));

            var labels = new List<string>();
            var data = new List<int>();
            foreach (var month in monthList)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Month));

                var key = new
                {
                    Year = month.Year,
                    Month = month.Month
                };

                if (res.ContainsKey(key))
                {
                    data.Add((int)res[key]);
                }
                else
                {
                    data.Add(0);
                }
            }

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public int GetCurrMonthAdoptionSpeed()
        {
            var currMonth = DateTime.Now.Month;

            var res = _context.AdoptionRequests.Where(r => r.OutcomeDateTime.Month == currMonth
                                                 && r.requestStatus.Equals(RequestStatus.Accepted)).ToList();

            if (res.Any())
            {
                return (int) res.Average(e => e.GetAdoptionSpeed());
            } 

            return 0;
        }

        public ChartData GetAdoptionListingsBySpecies()
        {
            var res = _context.AdoptionListings.GroupBy(l => l.Species)
                .Select(g => new
                {
                    g.Key,
                    ListingCount = g.Count()
                });

            var labels = new List<String>();
            var data = new List<int>();

            foreach (var p in res)
            {
                labels.Add(p.Key.ToString());
                data.Add(p.ListingCount);
            }

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public ChartData GetAdoptionsByMonth()
        {
            var currDateTime = DateTime.Now;
            var startDate = currDateTime.AddMonths(-5);

            var monthList = new List<DateTime>();
            for (int i = 5; i >= 0; i--)
            {
                var prevMonth = currDateTime.AddMonths(-i);
                monthList.Add(new DateTime(prevMonth.Year, prevMonth.Month, 1));
            }

            var res = _context.AdoptionRequests
                .Where(r => r.OutcomeDateTime >= new DateTime(startDate.Year, startDate.Month, 1)
                            && r.requestStatus.Equals(RequestStatus.Accepted))
                .GroupBy(r => new
                {
                    r.OutcomeDateTime.Year,
                    r.OutcomeDateTime.Month
                })
                .Select(g => new
                {
                    g.Key,
                    AdoptionCount = g.Count()
                })
                .ToDictionary(e => e.Key, e => e.AdoptionCount);

            //var monthList = res.Keys.ToList();
            //monthList.Sort((a, b)
            //    => a.Year.CompareTo(b.Year) == 0 ? a.Month.CompareTo(b.Month) : a.Year.CompareTo(b.Year));

            var labels = new List<string>();
            var data = new List<int>();
            foreach (var month in monthList)
            {
                labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Month));

                var key = new
                {
                    Year = month.Year,
                    Month = month.Month
                };

                if (res.ContainsKey(key))
                {
                    data.Add(res[key]);
                }
                else
                {
                    data.Add(0);
                }
            }

            return new ChartData()
            {
                labels = labels,
                data = data
            };
        }

        public int GetCurrMonthAdoptions()
        {
            var currMonth = DateTime.Now.Month;

            return _context.AdoptionRequests.Count(r => r.OutcomeDateTime.Month == currMonth
                                                        && r.requestStatus.Equals(RequestStatus.Accepted));
        }

        public IEnumerable<AdoptionData> GetLatestAdoptions()
        {
            return _context.AdoptionRequests
                .Where(r => r.requestStatus.Equals(RequestStatus.Accepted))
                .OrderByDescending(r => r.OutcomeDateTime).Take(1)
                .Select(r => new AdoptionData()
                {
                    name = r.AdoptionListing.Name,
                    species = r.AdoptionListing.Species.ToString(),
                    age = r.AdoptionListing.Age,
                    breed = r.AdoptionListing.Breed1.ToString(),
                    adopter_name = r.User.Name,
                    email = r.User.EmailAddress,
                    img = r.AdoptionListing.Image
                });
        }

        public IEnumerable<ListingData> GetSlowAdoptionListings()
        {
            return _context.AdoptionListings.Where(l => l.ApplicationStatus.Equals(ApplicationStatus.Open))
                .OrderBy(l => l.ListingDate).Take(1)
                .Select(l => new ListingData()
                {
                    name = l.Name,
                    species = l.Species.ToString(),
                    age = l.Age,
                    breed = l.Breed1.ToString(),
                    days_elapsed = l.GetDaysElapsed(),
                    listing_id = l.AdoptionListingID,
                    img = l.Image
                }).ToList();
        }
    }
}
