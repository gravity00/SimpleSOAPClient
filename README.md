# Simple SOAP Client
Lightweight SOAP client for invoking HTTP SOAP endpoints.
Fluently create SOAP Envelopes, send them through the SOAP Client and extract the needed information from the returned SOAP Envelope.
The client also exposes a range of handler that can be used as a pipeline to work with the SOAP Envelope or the HTTP requests and responses. 

## Installation 
This library can be installed via [NuGet](https://www.nuget.org/packages/SimpleSOAPClient/) package. Just run the following command:

```powershell
Install-Package SimpleSOAPClient -Pre
```

## Compatibility

This library is compatible with the folowing frameworks:

* MonoAndroid 1.0;
* MonoTouch 1.0;
* .NETFramework 4.5;
* .NETCore 5.0;
* .NETStandard 1.1;
* Portable Class Library (.NETFramework 4.5, Windows 8.0, WindowsPhoneApp 8.1);
* WindowsPhoneApp 8.1;
* Xamarin.iOS 1.0;
* Xamarin.TVOS 1.0;

## Usage (Version 2.0.0-rc03 - _recommended_)

```csharp
public static async Task MainAsync(string[] args, CancellationToken ct)
{
	using (var client =
		SoapClient.Prepare()
			.WithHandler(new DelegatingSoapHandler
			{
				Order = int.MaxValue, // Will be the last handler before the request and the first after the response
				OnHttpRequestAsyncAction = async (c, args, cnt) =>
				{
					Logger.Trace(
						"SOAP Outbound Request [{0}] -> {1} {2}({3})\n{4}",
						args.TrackingId, args.Request.Method, args.Url, args.Action,
						await args.Request.Content.ReadAsStringAsync());
				},
				OnHttpResponseAsyncAction = async (c, args, cnt) =>
				{
					Logger.Trace(
						"SOAP Outbound Response [{0}] -> {1}({2}) {3} {4}\n{5}",
						args.TrackingId, args.Url, args.Action, (int) args.Response.StatusCode,
						args.Response.StatusCode, await args.Response.Content.ReadAsStringAsync());
				}
			})
			.OnSoapEnvelopeRequest(args =>
			{
				args.Envelope.WithHeaders(
					KnownHeader.Oasis.Security.UsernameTokenAndPasswordText(
						"some-user", "some-password"));
			})
			.OnSoapEnvelopeResponse(args =>
			{
				var header =
					args.Envelope.Header<UsernameTokenAndPasswordTextSoapHeader>(
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
