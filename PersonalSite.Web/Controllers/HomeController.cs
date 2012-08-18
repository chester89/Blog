using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalSite.Web.Controllers
{
    public class HomeController
    {
        public MainPageInputModel Index()
        {
            return new MainPageInputModel();
        }
    }

    public class MainPageInputModel
    {
    }
}