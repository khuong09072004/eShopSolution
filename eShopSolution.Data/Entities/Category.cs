using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }

        // Make Status nullable so EF won't treat CLR default as sentinel for DB-generated default
        public Status? Status { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
