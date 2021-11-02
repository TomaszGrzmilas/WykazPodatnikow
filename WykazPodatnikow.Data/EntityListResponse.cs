using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record EntityListResponse
	{
		[JsonPropertyName("exception")]
		public Exception Exception { get; set; }

		[JsonPropertyName("result")]
		public EntityListResult Result { get; set; }
	}
}