using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Domains
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public float Price { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }

        /*Now i have two columns CatalogType Id and CatelogBrand Id.
        We have to associate these two columns with catelogItem*/

        //We have to build the relationship

        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        /*so for int CatalogTypeId in catalogItem table(Foriegn key to CatalogType table)
        so we are adding the below property of type CatalogType*/
        public CatalogType CatalogItemType { get; set; }

        /*so for int CatalogBrandId in catalogItem table(Foriegn key to CatalogType table)
        so we are adding the below property of type CatalogType*/
        public CatalogBrand CatalogItemBrand { get; set; }

    }
} 
