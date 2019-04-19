using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTests
{
    public class FindCountry
    {
        public FindCountry(string country)
        {
            _country = country;
        }
        private string _country;
        public bool FindCountryPredicate(Racer racer) => racer?.LastName == _country;
    }
}
