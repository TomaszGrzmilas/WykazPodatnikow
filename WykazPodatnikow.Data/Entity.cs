using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record Entity
	{
		[JsonPropertyName("identifier")]
		public string Identifier { get; set; }

		[JsonPropertyName("subjects")]
		public List<Subject> Subjects { get; set; }
	}
}