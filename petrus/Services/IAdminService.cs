using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petrus.Models;
using petrus.Resources;

namespace petrus.Services
{
    public interface IAdminService
    {
        ChartData GetAvgAdoptionSpeed();
        int GetCurrMonthAdoptionSpeed();
        ChartData GetAdoptionsBySpecies();
        ChartData GetAdoptionsByMonth();
        int GetCurrMonthAdoptions();
        IEnumerable<AdoptionData> GetLatestAdoptions();
        IEnumerable<ListingData> GetSlowAdoptionListings();
    }
}
