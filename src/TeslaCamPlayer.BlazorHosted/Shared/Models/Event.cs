using Newtonsoft.Json;

namespace TeslaCamPlayer.BlazorHosted.Shared.Models;

public class Event
{
	[JsonProperty("timestamp")]
	public DateTime Timestamp;
	[JsonProperty("city")]
	public string City;
	[JsonProperty("est_lat")]
	public string EstLat;
	[JsonProperty("est_lon")]
	public string EstLon;
	[JsonProperty("reason")]
	public string Reason;
	[JsonProperty("camera")]
	public Cameras Camera;
}
