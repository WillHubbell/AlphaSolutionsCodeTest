using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;

namespace WillRawInstanceProj.Controllers.renderings
{
    public class MiniCrmController : SitecoreController
    {
        public ActionResult ListCustomers()
        {
            //Get Page root, then "Customers" folder, so view can interate through its children
            Item currentContextItem = PageContext.Current.Item;
            Item customerFolder = null;
            customerFolder = currentContextItem.Parent.GetChildren()[0]; //really hacky since it depends on Customers folder being first child item
            
            return View(customerFolder);
        }

        
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}