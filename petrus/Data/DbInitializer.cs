using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using petrus.Data;
using petrus.Models;

namespace pwned_shop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(petrusDb db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (db.Users.Any())
                return;

            // populate Users table using data from csv/UserProfile.csv
            var rows = ReadCsv("Data/csv/Users.csv");
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                User u = new User()
                {
                    UserID = row[0],
                    Password = row[1],
                    Name = row[2],
                    PhoneNumber = Convert.ToInt32(row[3]),
                    EmailAddress = row[4],
                    SearchTerms = row[5]
                };

                db.Users.Add(u);
            }

            // populate Ratings table using data from csv/RatingESRB.csv
            //rows = ReadCsv("Data/csv/Admin.csv");
            //for (int i = 1; i < rows.Count; i++)
            //{
            //    var row = rows[i];
            //    Admin a = new Admin()
            //    {
            //        UserId = row[0],
            //        Password = row[1],
            //        Name = row[2],
            //        PhoneNumber = Convert.ToInt32(row[3]),
            //        EmailAddress = row[4],
            //        SearchTerms = row[5],
            //        AdminId = row[6]
            //    };

            //    db.Admins.Add(a);
            //}
            

            // populate Products table using data from csv/Product.csv
            rows = ReadCsv("Data/csv/AdoptionListing.csv");
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                AdoptionListing al = new AdoptionListing()
                {
                    AdoptionListingID = row[0],
                    Species = (Species)Enum.Parse(typeof(Species), row[1]),
                    Name = row[2],
                    Age = Convert.ToInt32(row[3]),
                    Breed1 = (Breed)Enum.Parse(typeof(Breed), row[4]),
                    Gender = (Gender)Enum.Parse(typeof(Gender), row[6]),
                    Color1 = (Color)Enum.Parse(typeof(Color), row[7]),
                    MaturitySize = (MaturitySize)Enum.Parse(typeof(MaturitySize), row[10]),
                    FurLength = (FurLength)Enum.Parse(typeof(FurLength), row[11]),
                    Vaccinated = (Vaccinated)Enum.Parse(typeof(Vaccinated), row[12]),
                    Dewormed = (Dewormed)Enum.Parse(typeof(Dewormed), row[13]),
                    Sterilized = (Sterilized)Enum.Parse(typeof(Sterilized), row[14]),
                    Health = (Health)Enum.Parse(typeof(Health), row[15]),
                    QuantityRepresented = Convert.ToInt32(row[16]),
                    Fee = Convert.ToDouble(row[17]),
                    Video = row[18],
                    Image = row[19],
                    Description = row[20],
                    ListingDate = DateTime.Parse(row[21], new CultureInfo("en-SG")),
                    ApplicationStatus = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), row[22]),
                    UserID = Convert.ToString(row[23])
                };

                db.AdoptionListings.Add(al);
            }
            db.SaveChanges();

            // populate Discounts table using data from csv/Discount.csv
            rows = ReadCsv("Data/csv/AdoptionRequest.csv");
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                AdoptionRequest ar = new AdoptionRequest()
                {
                    AdoptionRequestId = row[0],
                    Description = row[1],
                    residenceType = (Residence)Enum.Parse(typeof(Residence), row[2]),
                    RequestDate = DateTime.Parse(row[3], new CultureInfo("en-SG")),
                    requestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), row[4]),
                    OutcomeDateTime = row[5] == "" ? default(DateTime) : DateTime.Parse(row[5], new CultureInfo("en-SG")),
                    User = db.Users.Find(row[6]),
                    AdoptionListing = db.AdoptionListings.Find(row[7]),
                };

                db.AdoptionRequests.Add(ar);
            }
            db.SaveChanges();
        }

        public static List<string[]> ReadCsv(string path)
        {
            List<string[]> rows = new List<string[]>();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    var values = CSVParser.Split(row);

                    // clean up the fields (remove " and leading spaces)
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].TrimStart(' ', '"');
                        values[i] = values[i].TrimEnd('"');
                    }

                    rows.Add(values);
                }
            }

            return rows;
        }
    }
}
