using System.Collections.Generic;

namespace Assignment_3_Network.RDJPT.Data
{
    public class Data
    {
        public static List<Category> Categories;

        public Data()
        {
            InitCategories();
        }

        public void InitCategories()
        {
            Categories = new List<Category>()
            {
                new Category() { Cid = 1, Name = "Beverages" },
                new Category() { Cid = 2, Name = "Condiments" },
                new Category() { Cid = 3, Name = "Confections" }
            };
        }


    }
}
