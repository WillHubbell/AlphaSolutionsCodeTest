using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines.GetRenderingDatasource;

namespace WillRawInstanceProj.Controllers.renderings
{
    public class TeamMembersController : Controller
    {
        // GET: Default
        public ActionResult ListEmployees()
        {
            //Pass datasource item to view. Probably could have made a view rendering
            Item teamMembers = Context.Database.GetItem(RenderingContext.Current.Rendering.DataSource);

            return View(teamMembers);
        }
    }
}