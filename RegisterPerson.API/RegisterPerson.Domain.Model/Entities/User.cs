
namespace AuthJWT.Domain.Model.Entities
{
    public class User
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string AccessKey { get; set; }
        
    }
}
