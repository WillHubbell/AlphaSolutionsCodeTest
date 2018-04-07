using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines.InsertRenderings.Processors;
using Sitecore.SecurityModel;
using WillRawInstanceProj.Models;

namespace WillRawInstanceProj.Controllers.renderings
{
    public class CustSearchController : SitecoreController
    {
        [HttpGet]
        public ActionResult SearchCustomers()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SearchCustomers(CustSearchModel queryModel)
        {
            String searchTerm = queryModel.SearchString;
            var webIndex = ContentSearchManager.GetIndex("sitecore_master_index"); //change to web index later or maybe add for production
            var context = webIndex.CreateSearchContext();
            var results = context.GetQueryable<SearchResultItem>().Where(x => x["First Name"] == searchTerm);

            //TODO: find a way to present the results
            
            return View();
        }
    }
}