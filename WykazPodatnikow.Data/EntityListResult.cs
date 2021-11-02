using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record EntityListResult
	{
		[JsonPropertyName("entries")]
		public List<Entity> Entries { get; set; }

		[JsonPropertyName("requestDateTime")]
		public string RequestDateTime { get; set; }

		[JsonPropertyName("requestId")]
		public string RequestId { get; set; }
	}
}