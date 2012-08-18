using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalSite.Core.Entities
{
    public class Blog
    {
        public int Id { get; protected set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
