using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public class EntityPerson
	{
		/// <summary>
		/// Gets or Sets CompanyName
		/// </summary>
		[JsonPropertyName("companyName")]
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or Sets FirstName
		/// </summary>
		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or Sets LastName
		/// </summary>
		[JsonPropertyName("lastName")]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or Sets Pesel
		/// </summary>
		[JsonPropertyName("pesel")]
		public Pesel Pesel { get; set; }

		/// <summary>
		/// Gets or Sets Nip
		/// </summary>
		[JsonPropertyName("nip")]
		public string Nip { get; set; }
	}
}