using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore;
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
    public class CustInputController : SitecoreController
    {
        [HttpGet]
        public ActionResult SubmitCusty()
        {
            //display the form for customer submission
            return View(new Customer());
        }

        [HttpPost]
        public ActionResult SubmitCusty(Customer model)
        {
            //Get Page root, then "Customers" folder, so view can interate through its children
            Item currentContextItem = PageContext.Current.Item;
            Item customerFolder = null;
            customerFolder = currentContextItem.GetChildren()[0]; //really hacky since it depends on Customers folder being first child item ¯\_(ツ)_/¯
            String customerName = model.FirstName + model.LastName;
            Database masterDb = null;

            //Here, take the information submitted to the model, and create a new sitecore item under the "Customers" folder of template type: 
            //{ EF14436F - CBB2 - 4CE1 - 95A1 - C34A11D1EAA4}
            using (new SecurityDisabler())
            {
                //Get MasterDB and create new blank customer item
                masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
                TemplateItem customerTemplate = masterDb.GetTemplate("{EF14436F-CBB2-4CE1-95A1-C34A11D1EAA4}");
                Item customer = customerFolder.Add(customerName, customerTemplate);

                //bind model data to customer
                if (customer != null)
                {
                    customer.Editing.BeginEdit();
                    customer["First Name"] = model.FirstName;
                    customer["Last Name"] = model.LastName;
                    customer["E-mail Address"] = model.Email;
                    customer["Phone Number"] = model.Phone;
                    customer["Address Line One"] = model.AddressOne;
                    customer["Address Line Two"] = model.AddressTwo;
                    customer["City"] = model.City;
                    customer["State"] = model.State;
                    customer["Zip Code"] = model.ZipCode;
                    customer.Editing.EndEdit();
                }
            }

            //Redirect to listing page
            var pathInfo = LinkManager.GetItemUrl(masterDb.GetItem("{1C920C8E-49CC-4728-BD0F-F10D2F6E9CEC}"));

            return RedirectToRoute(MvcSettings.SitecoreRouteName, new { pathInfo = pathInfo.TrimStart(new char[] { '/' }) });
            /*return View(model); //Will not work right if there are multiple forms on page. Refer to below
            //https://ctor.io/posting-forms-in-sitecore-controller-renderings-another-perspective/ */
        }
    }
}