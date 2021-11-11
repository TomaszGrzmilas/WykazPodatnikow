using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;
public record EntityResponse
{
	[JsonPropertyName("result")]
	public EntityItem Result { get; set; }
}
