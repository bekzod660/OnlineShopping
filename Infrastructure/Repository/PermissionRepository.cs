using Domain.Entites.IdentityEntities;
using Infrastructure.Repository.Generic_Repository;

namespace Application.Services
{
    public class PermissionRepository : Repository<Permission>, Interfaces.IPermissionRepository
    {
        public PermissionRepository(Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
        {
        }
    }
}
