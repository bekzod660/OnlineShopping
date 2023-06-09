using Application.Abstraction;
using Application.Interfaces;
using Application.Models.Token;
using Infrastructure.Repository.Generic_Repository;
using System.Linq.Expressions;

namespace Application.Services
{
    public class RefreshTokenRepository : Repository<UserRefreshToken>, IRefreshTokenRepository
    {
        public readonly IApplicationDbContext _db;
        public RefreshTokenRepository(Abstraction.IApplicationDbContext ecommerceDb) : base(ecommerceDb)
        {
            _db = ecommerceDb;
        }
        public List<UserRefreshToken?> Get(Expression<Func<UserRefreshToken, bool>> expression)
        {
            var res = (_db.UserRefreshToken?.Where(expression)?.ToList());
            if (res.Count == 0)
            {
                return null;
            }
            return res;
        }


    }
}
