using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalWishlistBE.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IItemRepository Item { get; }
        IUserRepository User { get; }
        void Save();
    }
}
