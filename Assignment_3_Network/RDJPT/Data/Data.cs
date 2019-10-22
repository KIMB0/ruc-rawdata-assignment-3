using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assignment_3_Network.RDJPT.Data
{
    public class APIData
    {
        public static List<Category> Categories;
        public static string CategoryPath = "/api/categories";

        public APIData()
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

        public static int? GetCategoryId(string path)
        {
            string stringNumber = Regex.Replace(path, "[^0-9]", "");
            if (stringNumber.Length < 1) return null;

            int intNumber = int.Parse(stringNumber);
            return intNumber;
        }


    }
}
