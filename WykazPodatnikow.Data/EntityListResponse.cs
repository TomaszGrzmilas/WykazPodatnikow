using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;
public record EntityListResponse
{
	[JsonPropertyName("result")]
	public EntityList Result { get; set; }

	public WhiteListCheckException Exception { get; set; }
}
