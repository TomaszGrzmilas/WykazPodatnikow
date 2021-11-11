using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WykazPodatnikow.Data;
public record EntryListResponse
{
	[JsonPropertyName("result")]
	public EntryList Result { get; set; }

}