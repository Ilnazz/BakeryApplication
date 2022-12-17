using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public partial class User
    {
        public string Initials { get => $"{Employee.Surname} {Employee.Name[0]}. {Employee.Patronymic[0]}."; }
    }
}
