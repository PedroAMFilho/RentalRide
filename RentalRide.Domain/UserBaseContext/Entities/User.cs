using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentalRide.Domain.UserBaseContext.Entities
{
    public class User : Entity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public EAccessLevel access_level { get; set; }
        public int deliverer_id { get; set; }
        public string email { get; set; }
    }
}
