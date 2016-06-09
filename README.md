# Simple SOAP Client
Lightweight SOAP client for invoking HTTP SOAP endpoints.
Fluently create SOAP Envelopes, send them through the SOAP Client and extract the needed information from the returned SOAP Envelope.
How easier could it be? 

## Installation 
This library can be installed via [NuGet](https://www.nuget.org/packages/SimpleSOAPClient/) package. Just run the following command:

```powershell
Install-Package SimpleSOAPClient
```

## Compatibility

This library is compatible with the folowing frameworks:

* .NET for Windows Store apps (> .NETCore 4.5);
* .NET Framework 4.0
* .NET Framework 4.5
* .NET Platform (> DotNET 5.0)
* DNX Core (> DNXCore 5.0)

## Usage (Version 2.0.0-rc01)

```csharp
public static async Task MainAsync(string[] args, CancellationToken ct)
{
	using (var client =
		SoapClient.Prepare()
			.OnSerializeRemoveXmlDeclaration()
			.UsingRequestEnvelopeHandler((c, d) =>
			{
				d.Envelope.WithHeaders(
					KnownHeader.Oasis.Security.UsernameTokenAndPasswordText(
						"some-user", "some-password"));
			})
			.UsingRequestRawHandler((c, d) =>
			{
				Logger.Trace(
					"SOAP Outbound Request -> {0} {1}({2})\n{3}",
					d.Request.Method, d.Url, d.Action, d.Content);
			})
			.UsingResponseRawHandler((c, d) =>
			{
				Logger.Trace(
					"SOAP Outbound Response -> {0}({1}) {2} {3}\n{4}",
					d.Url, d.Action, (int) d.Response.StatusCode, d.Response.StatusCode, d.Content);
			}).UsingResponseEnvelopeHandler((c, d) =>
			{
				var header =
					d.Envelope.Header<UsernameTokenAndPasswordTextSoapHeader>(
						"{" +
						Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10 +
						"}Security");
			}))
	{
		var requestEnvelope =
			SoapEnvelope.Prepare().Body(new IsAliveRequest());
		
		SoapEnvelope responseEnvelope;	
		try
		{
			responseEnvelope =
				await client.SendAsync(
					"https://services.company.com/Service.svc",
					"http://services.company.com/IService/IsAlive",
					requestEnvelope, ct);
		}
		catch (SoapEnvelopeSerializationException e)
		{
			Logger.Error(e, $"Failed to serialize the SOAP Envelope [Envelope={e.Envelope}]");
			throw;
		}
		catch (SoapEnvelopeDeserializationException e)
		{
			Logger.Error(e, $"Failed to deserialize the response into a SOAP Envelope [XmlValue={e.XmlValue}]");
			throw;
		}

		try
		{
			var response = responseEnvelope.Body<IsAliveResponse>();
		}
		catch (FaultException e)
		{
			Logger.Error(e, $"The server returned a fault [Code={e.Code}, String={e.String}, Actor={e.Actor}]");
			throw;
		}
	}
}

[XmlRoot("IsAliveRequest", Namespace = "http://services.company.com")]
public class IsAliveRequest
{

}

[XmlRoot("IsAliveResponse", Namespace = "http://services.company.com")]
public class IsAliveResponse
{
	[XmlElement("IsAliveResult")]
	public bool IsAlive { get; set; }
}
```
