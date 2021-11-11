using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WykazPodatnikow.Data;
public record EntryList
{
	[JsonPropertyName("entries")]
	public List<Entry> Entries { get; set; }

	[JsonPropertyName("requestDateTime")]
	public string RequestDateTime { get; set; }

	[JsonPropertyName("requestId")]
	public string RequestId { get; set; }
}
