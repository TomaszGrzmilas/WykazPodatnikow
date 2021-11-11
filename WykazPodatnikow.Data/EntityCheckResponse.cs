using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;

public record EntityCheckResponse
{
	[JsonPropertyName("result")]
	public EntityCheck Result { get; set; }
}
