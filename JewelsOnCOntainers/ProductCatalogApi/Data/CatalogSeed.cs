using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Domains;

namespace ProductCatalogApi.Data
{
    public static class CatalogSeed
    {
        //We have to tell the entityframework, to migrate domains(classes) to the database world
        //If we dont call migrate, the tables dont exist nor subsequent changes wont happen
        //this class gonna seed dummy data for us 
        //We are now telling, what should be seeded in to tables when they are created
        //I want this class to seed CatalogContext database
        //when somebody is gonna call this below Seed method they are gonna tell me where the database is.
        
        public static void Seed(CatalogContext catalogContext)
        {
            /*In order to migrate, Entityframe work requires the translated version of catalog context, To do that
            View => otherwindows => Package Manager console, this is powershell window whhere we can run powershell
            commands nothing but c# commands.*/
            //Then type Add-Migration Initial(For Initial migration)
            //What does Migrate do?? If we dont migrate, the tables will not get created in the database.
            catalogContext.Database.Migrate();

            //we are checking whether the database is created or not in the below line.
            //catalogContext.Database.EnsureCreated();
            //Below Line checks the database, if it has any rows
            if (!catalogContext.CatalogBrands.Any()) //CatalogBrand Table
            {
                
                catalogContext.CatalogBrands.AddRange(GetCatalogBrands());
                //Until We save changes, it will not commit or create table.
                catalogContext.SaveChanges();
            }

            if (!catalogContext.CatalogTypes.Any()) //CatalogType Table
            {
                catalogContext.CatalogTypes.AddRange(GetCatalogTypes());
                //Until We save changes, it will not commit or create table.
                catalogContext.SaveChanges();
            }

            if (!catalogContext.Catalog.Any()) //CatalogItem Table
            {
                catalogContext.Catalog.AddRange(GetCatalogItems());
                //Until We save changes, it will not commit or create table.
                catalogContext.SaveChanges();
            }

        }

        private static IEnumerable<CatalogItem> GetCatalogItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "A ring that has been around for over 100 years", Name = "World Star", Price = 199.5f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "will make you world champions", Name = "White Line", Price = 88.50f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "You have already won gold medal", Name = "Prism White", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "High Jumper", Name = "Antique Ring", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, Description = "All round", Name = "Inredeible", Price = 248.5f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 3, Description = "You ar ethe star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 2, Description = "A ring popular in the 16th and 17th century in Western Europe that was used as an engagement wedding ring", Name = "London Star", Price = 218.5f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 1, Description = "A floppy, bendable ring made out of links of metal", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }
            };
        }

        private static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType
                {
                    Type = "Engagement Ring"
                },
                new CatalogType
                {
                    Type = "Wedding Ring"
                },
                new CatalogType
                {
                    Type = "Fashion Ring"
                }
            };
        }

        /*below CatalogBrand[] GetCatalogBrands() should be returning the array of catalog brands,
          *But we should know the exact length of an array, which we doesnt know*/
        //private static CatalogBrand[] GetCatalogBrands()

        //So in the place of CatalogBrand[] we can use IEnumerable<CatalogBrand>,
        // list or an array is an Enumerable
        //IEnumerable is an interface in c#, that provides you to iterate through elements in the list,array etc
        private static IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            /*This method should create a dummy set of catalog brands and return it
            to AddRange in 24th line, that way rows can be created*/
            return new List<CatalogBrand>
            //Its a list of catalog brand,  Evry item in the list should be a CatalogBrand
            {
                new CatalogBrand //Instance of the CatalogBrand
                {
                    Brand = "Tiffany & co."
                },
                new CatalogBrand
                {
                    Brand = "Blue Nile"
                },

                new CatalogBrand
                {
                    Brand = "James Allen"
                }
            };
        }
    }
}
