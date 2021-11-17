using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record WhiteListCheckException
	{
		/// <summary>
		/// Gets or Sets Message
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or Sets Code
		/// </summary>
		[JsonPropertyName("code")]
		public string Code { get; set; }
	}
}