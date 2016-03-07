# SimpleSOAPClient
Lightweight SOAP client for invoking HTTP SOAP endpoints

## Usage

```csharp
[XmlType("AddUserRequest", Namespace = "http://example.simplesoapclient.com/request")]
public class AddUserRequest {

  [XmlElement]
  public string Username { get; set; }
  
  [XmlElement]
  public string Password { get; set; }
}

[XmlType("AddUserResponse", Namespace = "http://example.simplesoapclient.com/response")]
public class AddUserResponse {

  [XmlElement]
  public string Id { get; set; }
  
  [XmlElement]
  public string Username { get; set; }
}

public async Task<AddUserResponse> AddUserAsync(string username, string password, CancellationToken ct) {
  
  using(var client = new SoapClient{
    RequestRawHandler = (url, xml) => {
      Logger.Trace("SOAP Outbound Request -> {0} \n {1}", url, xml);
      return xml;
    },
    ResponseRawHandler = (url, xml) => {
      Logger.Trace("SOAP Outbound Response -> {0} \n {1}", url, xml);
      return xml;
    }
  }) {
    
    var requestEnvelope = 
        SoapEnvelope.Prepare()
          .WithHeaders(
            KnownHeader.Oasis.Security.UsernameTokenAndPasswordText("some-user", "some-password"))
          .Body(new AddUserRequest {
            Username = username,
            Password = password
          });
    
    var responseEnvelope = await client.SendAsync("http://localhost/TestSoapServer", requestEnvelope, ct);
    
    return responseEnvelope.Body<AddUserResponse>();
    
  }
  
}
```
