using System;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ResourceBookings.Plugins
{
    public partial class SaveResource : BasePlugin
    {
        public SaveResource(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            // Register for any specific events by instantiating a new instance of the 'PluginEvent' class and registering it
            base.RegisteredEvents.Add(new PluginEvent()
            {
                Stage = eStage.PreOperation,
                MessageName = MessageNames.Create,
                EntityName = EntityNames.bookableresourcebooking,
                PluginAction = SaveBooking
            });
        }

        public void SaveBooking(IServiceProvider serviceProvider)
        {
            using (var localContext = new LocalPluginContext<Entity>(serviceProvider))
            {
                // Plugin Logic
                IPluginExecutionContext context = localContext.PluginExecutionContext;
                IOrganizationService service = localContext.OrganizationService;

                Entity bookableResource = (Entity)context.InputParameters["Target"];
                Guid workorderid = ((EntityReference)bookableResource.Attributes["msdyn_workorder"]).Id;
                DateTime startTime = (DateTime)bookableResource.Attributes["starttime"];
                
                Data saveData = new Data();
                loadFetchQueries();

                List<Entity> entities = saveData
                    .RetrieveMultiple(localContext.OrganizationService, FetchQueries["GetWorkOrderBookings"]
                    .Replace("WorkOrderId", workorderid.ToString()))
                    .ToList();

                var r = entities
                    .Where(e => e.GetAttributeValue<DateTime>("starttime")
                    .ToShortDateString().Equals(startTime.ToShortDateString()));

                if (r.Count() > 0)
                {
                    throw new InvalidPluginExecutionException("Multiple bookings can not be scheduled for the same day. ");
                }
            }
        }

        public static Dictionary<string, string> FetchQueries = null;
        public static void loadFetchQueries()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Properties.Resources.Fetch);
                XmlNode xmlList = doc.SelectSingleNode("//Queries");

                Dictionary<string, string> queries = new Dictionary<string, string>();
                foreach (XmlNode node in xmlList.ChildNodes)
                {
                    queries.Add(node.Attributes["Id"].Value, node.InnerText);
                }
                FetchQueries = queries;
            }
            catch (Exception ex)
            {
                //Throw Exception
                throw;
            }
        }
    }
}
