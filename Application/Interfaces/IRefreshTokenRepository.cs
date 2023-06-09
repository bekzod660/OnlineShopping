using Application.Interfaces.Repository;
using Application.Models.Token;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<UserRefreshToken>
    {
        public List<UserRefreshToken?> Get(Expression<Func<UserRefreshToken, bool>> expression);

    }
}
