
using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace AuthJWT.Domain.Model.Entities
{
    public class Users : ISupportsHyperMedia
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string AccessKey { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
        
    }
}
