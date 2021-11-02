
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record EntityCheck
	{
		/// <summary>
		/// Czy rachunek przypisany do podmiotu czynnego
		/// </summary>
		/// <value>Czy rachunek przypisany do podmiotu czynnego </value>
		[JsonPropertyName("accountAssigned")]
		public string AccountAssigned { get; set; }

		/// <summary>
		/// Gets or Sets RequestId
		[JsonPropertyName("requestId")]
		public string RequestId { get; set; }
	}
}