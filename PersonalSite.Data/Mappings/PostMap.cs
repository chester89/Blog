using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PersonalSite.Core.Entities;

namespace PersonalSite.Data.Mappings
{
    public class PostMap: ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id);
            Map(x => x.Text);
            Map(x => x.Title);
        }
    }
}
