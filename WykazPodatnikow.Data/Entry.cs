using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WykazPodatnikow.Data;

public record Entry
{
	[JsonPropertyName("identifier")]
	public string identifier { get; set; }

	[JsonPropertyName("subjects")]
	public List<Entity> Subjects { get; set; }

	[JsonPropertyName("error")]
	public Exception Error { get; set; }
}