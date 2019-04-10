using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsApp.Models
{
    interface IDataProviderFactory
    {
        ICatDataProvider GetCatsDataProvider();
    }
}
