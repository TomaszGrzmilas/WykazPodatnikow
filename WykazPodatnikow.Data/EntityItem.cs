using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record EntityItem
	{
		[JsonPropertyName("subject")]
		public Entity Subject { get; set; }

		[JsonPropertyName("requestId")]
		public string RequestId { get; set; }

		[JsonPropertyName("requestDateTime")]
		public string RequestDateTime { get; set; }
	}
}