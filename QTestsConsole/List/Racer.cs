using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTests
{
    public class Racer : IComparable<Racer>, IFormattable
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }

        public Racer(int id, string firstName, string lastName, string country):this(id, firstName, lastName, country, wins: 0)
        { }
        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Wins = wins;
        }

        public override string ToString() => $"{FirstName} {LastName}";
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "N";
            switch (format.ToUpper())
            {
                case "N": // name
                    return ToString();
                case "F": // first name
                    return FirstName;
                case "L": // last name
                    return LastName;
                case "W":
                    return $"{ToString()} Wins: {Wins}";
                case "C":
                    return $"{ToString()} Country: {Country}";
                case "A":
                    return $"{ToString()} Country: {Country} Wins:{Wins}";
                default:
                    throw new FormatException(String.Format(formatProvider, $"Format {format} is not supported"));
            }
        }
        public string ToString(string format) => ToString(format, null);

        public int CompareTo(Racer other)
        {
            int compare = LastName?.CompareTo(other?.LastName) ?? -1;
            if(compare == 0)
            {
                return FirstName?.CompareTo(other?.FirstName) ?? -1;
            }
            return compare;
        }


    }
}
