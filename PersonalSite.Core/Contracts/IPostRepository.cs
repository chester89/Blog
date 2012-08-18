using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalSite.Core.Entities;

namespace PersonalSite.Core.Contracts
{
    public interface IPostRepository
    {
        IQueryable<Post> GetAll();
        void AddNew(Post post);
    }
}
