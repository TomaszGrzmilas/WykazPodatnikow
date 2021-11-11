using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;
public record EntityCheck
{
	/// <summary>
	/// Czy rachunek przypisany do podmiotu czynnego
	/// </summary>
	/// <value>Czy rachunek przypisany do podmiotu czynnego </value>
	[JsonPropertyName("accountAssigned")]
	public string AccountAssigned { get; set; }

	[JsonPropertyName("requestId")]
	public string RequestId { get; set; }

	[JsonPropertyName("requestDateTime")]
	public string RequestDateTime { get; set; }
}
