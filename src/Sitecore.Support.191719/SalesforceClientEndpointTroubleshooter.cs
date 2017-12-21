using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Troubleshooters;

namespace Sitecore.Support.DataExchange.Providers.Salesforce.Endpoints
{
  public class SalesforceClientEndpointTroubleshooter : BaseEndpointTroubleshooter
  {
    public SalesforceClientEndpointTroubleshooter()
    {
    }
    protected override ITroubleshooterResult Troubleshoot(Endpoint endpoint, TroubleshooterContext context)
    {
      if (endpoint == null)
      {
        return TroubleshooterResult.FailResult("Endpoint is null", null);
      }
      var settings = endpoint.GetPlugin<AuthenticationClientSettings>();
      if (settings == null)
      {
        return TroubleshooterResult.FailResult("Endpoint settings plugin is null.", null);

      }
      if (settings.AuthenticationClient == null)
      {
        return TroubleshooterResult.FailResult("Authentication client is null on the endpoint settings plugin. This may be because no connection string is specified, or because the specified connection string does not exist.", null);
      }
      try
      {
        var task = Task.Run(async () => await settings.AuthenticationClient.TokenRefreshAsync(settings.AuthenticationClient.Id, settings.AuthenticationClient.RefreshToken));
      }
      catch (Exception ex)
      {
        return TroubleshooterResult.FailResult($"{"Exception during connection."} {"Read more in log file."} {ex.Message}", ex);

      }
      return TroubleshooterResult.SuccessResult("Connection was successfully established.");
    }
  }
}