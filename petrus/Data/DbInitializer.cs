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
            string dateFormat = "d/M/yyyy";
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                User u = new User()
                {
                    UserId = row[0],
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
                    AdoptionId = row[0],
                    Species = row[1],
                    Name = row[2],
                    Age = Convert.ToInt32(row[3]),
                    Breed1 = row[4],
                    Breed2 = row[5],
                    Gender = row[6],
                    Color1 = row[7],
                    Color2 = row[8],
                    Color3 = row[9],
                    MaturitySize = row[10],
                    FurLength = row[11],
                    Vaccinated = Convert.ToBoolean(row[12]),
                    Dewormed = Convert.ToBoolean(row[13]),
                    Sterilized = Convert.ToBoolean(row[14]),
                    Health = row[15],
                    QuantityRepresented = Convert.ToInt32(row[16]),
                    Fee = Convert.ToDouble(row[17]),
                    /*VideoBytes = new[] {Convert.ToByte(row[18])},
                    ImageBytes = new [] {Convert.ToByte(row[19])},*/
                    Description = row[20],
                    ListingDate = DateTime.Parse(row[21], new CultureInfo("en-SG")),
                    ApplicationStatus = row[22]
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
                    RequestDate = DateTime.Parse(row[2], new CultureInfo("en-SG"))
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
