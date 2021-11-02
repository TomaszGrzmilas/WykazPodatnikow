using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record EntityResponse
	{
		/// <summary>
		/// Gets or Sets Exception
		/// </summary>
		[JsonPropertyName("exception")]
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets or Sets Result
		/// </summary>
		[JsonPropertyName("result")]
		public EntityItem Result { get; set; }
	}
}