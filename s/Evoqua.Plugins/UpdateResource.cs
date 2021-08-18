using System;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ResourceBookings.Plugins
{
    public partial class UpdateResource : BasePlugin
    {
        public UpdateResource(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            // Register for any specific events by instantiating a new instance of the 'PluginEvent' class and registering it
            base.RegisteredEvents.Add(new PluginEvent()
            {
                Stage = eStage.PreOperation,
                MessageName = MessageNames.Update,
                EntityName = EntityNames.bookableresourcebooking,
                PluginAction = UpdateBooking
            });
        }

        public void UpdateBooking(IServiceProvider serviceProvider)
        {
            // Use a 'using' statement to dispose of the service context properly
            // To use a specific early bound entity replace the 'Entity' below with the appropriate class type
            using (var localContext = new LocalPluginContext<Entity>(serviceProvider))
            {
                // Plugin Logic
                IPluginExecutionContext context = localContext.PluginExecutionContext;
                IOrganizationService service = localContext.OrganizationService;
                Entity bookableResourceBooking = (Entity)context.InputParameters["Target"];
                Entity preImageBookableResourceBooking = context.PreEntityImages["PreImage"];

                Guid workorderid = ((EntityReference)preImageBookableResourceBooking.Attributes["msdyn_workorder"]).Id;
                DateTime startTime = (DateTime)preImageBookableResourceBooking.Attributes["starttime"];

                if (bookableResourceBooking.Attributes.Contains("msdyn_workorder"))
                {
                    workorderid = ((EntityReference)bookableResourceBooking.Attributes["msdyn_workorder"]).Id;
                }

                if (bookableResourceBooking.Attributes.Contains("starttime"))
                {
                    startTime = ((DateTime)bookableResourceBooking.Attributes["starttime"]);
                }

                loadFetchQueries();
                Data saveData = new Data();

                List<Entity> entities = saveData
                    .RetrieveMultiple(localContext.OrganizationService, FetchQueries["GetWorkOrderBookings"]
                    .Replace("WorkOrderId", workorderid.ToString()))
                    .ToList();

                var r = entities
                    .Where(e => e.GetAttributeValue<DateTime>("starttime")
                    .ToShortDateString().Equals(startTime.ToShortDateString()));

                if (r.Count() >= 1)
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
