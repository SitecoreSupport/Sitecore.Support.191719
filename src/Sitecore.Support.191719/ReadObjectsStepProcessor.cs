using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Salesforce.Common;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.Support.DataExchange.Providers.Salesforce.Endpoints;

namespace Sitecore.Support.DataExchange.Providers.Salesforce.ReadObjects
{
  public class ReadObjectsStepProcessor: Sitecore.DataExchange.Providers.Salesforce.ReadObjects.ReadObjectsStepProcessor
  {
    protected override AuthenticationClient GetAuthenticationClient(Endpoint endpoint, PipelineStep pipelineStep,
      PipelineContext pipelineContext, Sitecore.Services.Core.Diagnostics.ILogger logger)
    {
      return endpoint.GetPlugin<AuthenticationClientSettings>().AuthenticationClient;
    }
  }
}