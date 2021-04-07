using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class CatalogItem
    {
        // this is CatalogItem Model in WebMvc project
        /*so basically we made a copy of catalogItem.cs from productCatalogApi/domains, so thet way when we get
         back the data  from the microservice, the microservice data will represent this CatalogItem Type,
        so we know which columns should display where*/
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }     
        public string CatalogItemType { get; set; }   
        public string CatalogItemBrand { get; set; }
    }
}
