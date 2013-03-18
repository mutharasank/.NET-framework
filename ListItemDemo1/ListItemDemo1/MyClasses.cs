using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListItemDemo1
{
  public class Product
  {
    string name;
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    string category;
    public string Category
    {
      get { return category; }
      set { category = value; }
    }

    double price;
    public double Price
    {
      get { return price; }
      set { price = value; }
    }

    int inStock;
    public int InStock
    {
      get { return inStock; }
      set { inStock = value; }
    }

    public override string ToString()
    {
      return Name+" "+category+" "+price+" "+inStock;
    }
  }

  public class Service
  {
    private static Service service = null;

    List<Product> products = new List<Product>();
    public List<Product> Products
    {
      get { return products; }
    }

    private Service() 
    {
      products.Add(new Product { Name = "PC", Category ="Office", Price=5000.0, InStock=4});
      products.Add(new Product { Name = "White bread", Category = "Food", Price = 25.0, InStock = 20 });
      products.Add(new Product { Name = "Cake", Category = "Food", Price = 40.0, InStock = 8 });
      products.Add(new Product { Name = "Pencil", Category = "Office", Price = 12.0, InStock = 134 });
      products.Add(new Product { Name = "BMW", Category = "Car", Price = 500000.0, InStock = 1 });
      products.Add(new Product { Name = "Audi", Category = "Car", Price =420000.0, InStock = 2 });
    }

    public static Service Instance
    {
      get
      {
        if (service == null)
          service = new Service();

        return service;
      }
    }
  }
}
