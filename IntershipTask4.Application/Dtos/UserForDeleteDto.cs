using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Application.Dtos
{
    public class UserForDeleteDto
    {
        public int Id { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
