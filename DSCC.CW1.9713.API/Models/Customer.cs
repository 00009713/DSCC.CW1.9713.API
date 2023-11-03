using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DSCC.CW1._9713.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
