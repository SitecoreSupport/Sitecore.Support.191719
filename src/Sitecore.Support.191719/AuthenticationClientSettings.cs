using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Salesforce.Common;
using Sitecore.DataExchange;

namespace Sitecore.Support.DataExchange.Providers.Salesforce.Endpoints
{
  public class AuthenticationClientSettings : IPlugin
  {
    public AuthenticationClientSettings(string clientId, string clientSecret, string username, string password, string securityToken)
    {
      _clientId = clientId;
      _clientSecret = clientSecret;
      _username = username;
      _password = password;
      _securityToken = securityToken;
      this.TokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";
    }
    private string _clientId;
    private string _clientSecret;
    private string _username;
    private string _password;
    private string _securityToken;
    public string TokenRequestEndpointUrl { get; set; }
    private AuthenticationClient _client = null;
    public AuthenticationClient AuthenticationClient
    {
      get
      {
        if (_client == null)
        {
          _client = new AuthenticationClient();
        }
        var task = Task.Run(async () => await _client.UsernamePasswordAsync(_clientId, _clientSecret, _username, _password + _securityToken, this.TokenRequestEndpointUrl));
        task.Wait();
        return _client;
      }
    }
  }
}