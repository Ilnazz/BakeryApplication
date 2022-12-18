using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public partial class Employee
    {
        public string Initials { get => $"{Surname} {Name[0]}. {Patronymic[0]}."; }
    }
}
