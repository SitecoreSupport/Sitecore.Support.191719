using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace Sitecore.Support.DataExchange.Providers.Salesforce.Endpoints
{
  public class SalesforceClientEndpointConverter: BaseConnectionStringEndpointConverter
  {
    public SalesforceClientEndpointConverter(IItemModelRepository repository) : base(repository)
    {
    }
    protected override void AddPlugins(ItemModel source, Endpoint endpoint)
    {
      var cs = this.GetConnectionString(source);
      if (string.IsNullOrEmpty(cs))
      {
        return;
      }
      var builder = new DbConnectionStringBuilder
      {
        ConnectionString = cs
      };
      if (!string.IsNullOrWhiteSpace(builder.ConnectionString))
      {
        var username = (string)builder["user id"];
        var password = (string)builder["password"];
        var clientId = (string)builder["client id"];
        var secretKey = (string)builder["secret key"];
        var securityToken = (string)builder["security token"];
        var isSandbox = false;
        if (builder.ContainsKey("sandbox"))
        {
          bool.TryParse((string)builder["sandbox"], out isSandbox);
        }
        var credentials = new AuthenticationClientSettings(clientId, secretKey, username, password, securityToken)
        {
          IsSandbox = isSandbox
        };
        endpoint.AddPlugin(credentials);
      }
    }
  }
}