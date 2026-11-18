
namespace api.Models
{
    public class Actors : BaseEntity
    {
        public string Name {get;set;} = string.Empty;

        public string Country {get;set;} = string.Empty;

        public int BirthYear {get;set;}
    }
}