using System.Collections.Generic;
using CarStore.Data.Models.Enums;

namespace CarStore.Web.ViewModels.Catalog
{
    public class CatalogCarViewModel
    {
        public CatalogCarViewModel()
        {
            this.CategoriesNames = new HashSet<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string BrandName { get; set; }
        
        public string ModelName { get; set; }
        
        public int YearOfManufacture { get; set; }
        
        public Color Color { get; set; }
        
        public int Mileage { get; set; }
        
        public Gearbox Gearbox { get; set; }

        public EngineType EngineType { get; set; }
        
        public ICollection<string> CategoriesNames { get; set; }
    }
}
