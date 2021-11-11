using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data;
public class EntityPerson
{

	[JsonPropertyName("companyName")]
	public string CompanyName { get; set; }

	[JsonPropertyName("firstName")]
	public string FirstName { get; set; }

	[JsonPropertyName("lastName")]
	public string LastName { get; set; }

	[JsonPropertyName("pesel")]
	public Pesel Pesel { get; set; }

	[JsonPropertyName("nip")]
	public string Nip { get; set; }
}