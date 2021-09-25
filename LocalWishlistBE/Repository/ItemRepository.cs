using LocalWishlistBE.Entities;
using LocalWishlistBE.Models;
using LocalWishlistBE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalWishlistBE.Repository
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
