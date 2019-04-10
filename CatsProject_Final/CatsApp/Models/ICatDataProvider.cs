using CatsWebApi.Models;
using System.Collections.Generic;

namespace CatsApp.Models
{
    public interface ICatDataProvider
    {
        bool AddNewCat(Cat catToAdd);
        bool DeleteCat(int catId);
        List<Cat> GetCats();
        bool UpdateCat(Cat catToUpdate);
    }
}