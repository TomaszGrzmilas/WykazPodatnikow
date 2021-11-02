using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record FlatFileData
	{
		[JsonPropertyName("naglowek")]
		public Naglowek Naglowek { get; set; }

		[JsonPropertyName("skrotyPodatnikowCzynnych")]
		public List<string> SkrotyPodatnikowCzynnych { get; set; }

		[JsonPropertyName("skrotyPodatnikowZwolnionych")]
		public List<string> SkrotyPodatnikowZwolnionych { get; set; }

		[JsonPropertyName("maski")]
		public List<string> Maski { get; set; }
	}

	public record Naglowek
	{
		[JsonPropertyName("dataGenerowaniaDanych")]
		public string DataGgenerowaniaDanych { get; set; }

		[JsonPropertyName("liczbaTransformacji")]
		public string LiczbaTransformacji { get; set; }

		[JsonPropertyName("schemat")]
		public string Schemat { get; set; }
	}
}