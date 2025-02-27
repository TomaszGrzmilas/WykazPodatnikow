﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;
public record EntityList
{
	[JsonPropertyName("subjects")]
	public List<Entity> Subjects { get; set; }

	[JsonPropertyName("requestDateTime")]
	public string RequestDateTime { get; set; }

	[JsonPropertyName("requestId")]
	public string RequestId { get; set; }
}
