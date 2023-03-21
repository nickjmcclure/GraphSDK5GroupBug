using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

Console.WriteLine("Get Groups");

string graphURL = "";
string tenantId = "";
string clientId = "";
string secret = "";
string upn = "";

string[] scopes = new[] { $"{graphURL}/.default" };
ClientSecretCredential clientCredential = new(tenantId, clientId, secret);
GraphServiceClient graphServiceClient = new(clientCredential, scopes);

var groupResponse = await graphServiceClient
    .Users[upn]
    .MemberOf
    .GetAsync(requestConfiguration =>
    {
        requestConfiguration.QueryParameters.Select = new string[] { "DisplayName" };
    });


var pageIterator = PageIterator<Group, GroupCollectionResponse>
    .CreatePageIterator(graphServiceClient, groupResponse!,
        (g) =>
        {
            Console.WriteLine(g.DisplayName);
            return true;
        }
    );

await pageIterator.IterateAsync();



