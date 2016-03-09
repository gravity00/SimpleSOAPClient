# Simple SOAP Client
Lightweight SOAP client for invoking HTTP SOAP endpoints.
Fluently create SOAP Envelopes, send them through the SOAP Client and extract the needed information from the returned SOAP Envelope. How easier could it be? 

## Installation 
This library can be installed via NuGet package. Just run the following command:

```powershell
Install-Package SimpleSOAPClient -Pre
```

## Usage

```csharp
public async Task<AddUserResponse> AddUserAsync(string username, string password, CancellationToken ct) {
  
  using(var client = new SoapClient {
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
    
    try {
      return responseEnvelope.Body<AddUserResponse>();
    } catch(FaultException e) {
      Logger.Error(e, $"The server returned a fault [Code={e.Code}, String={e.String}, Actor={e.Actor}]");
      throw;
    }
    
  }
  
}

[XmlRoot("AddUserRequest", Namespace = "http://example.simplesoapclient.com/request")]
public class AddUserRequest {

  [XmlElement]
  public string Username { get; set; }
  
  [XmlElement]
  public string Password { get; set; }
}

[XmlRoot("AddUserResponse", Namespace = "http://example.simplesoapclient.com/response")]
public class AddUserResponse {

  [XmlElement]
  public string Id { get; set; }
  
  [XmlElement]
  public string Username { get; set; }
}
```
