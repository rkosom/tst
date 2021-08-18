using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;
using System.Xml;

namespace Evoqua.PluginsV2
{
    class Data
    {
        //Static connection string is locked for Azure usage
        public string ConnectionString { get; set; }
        public object lockObject = new object();

        #region public DataCollection<Entity> RetrieveMultiple(IOrganizationService crmService, string fetch)
        public DataCollection<Entity> RetrieveMultiple(IOrganizationService crmService, string fetch)
        {
            try
            {
                lock (lockObject)
                {
                    RetrieveMultipleRequest query = new RetrieveMultipleRequest
                    {
                        Query = new FetchExpression(fetch)
                    };

                    EntityCollection recipients = ((RetrieveMultipleResponse)crmService.Execute(query)).EntityCollection;
                    return recipients.Entities;
                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> e)
            {
                //Log your Exception
                throw;
            }
        }
        #endregion

        #region public Entity Insert(CrmServiceClient crmService, Entity entity)
        public Entity Insert(IOrganizationService crmService, Entity entity)
        {
            try
            {
                lock (lockObject)
                {
                    entity.Id = crmService.Create(entity);
                    return entity;
                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> e)
            {
                //Log your Exception
                throw;
            }
        }
        #endregion
    }
}
