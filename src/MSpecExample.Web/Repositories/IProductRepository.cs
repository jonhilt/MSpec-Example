using System.Collections.Generic;
using MSpecExample.Web.Models;

namespace MSpecExample.Web.Repositories
{
    public interface IProductRepository
    {
        IList<Product> FindProducts(string searchTerm);
    }
}