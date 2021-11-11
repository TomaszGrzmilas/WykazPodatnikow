using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WykazPodatnikow.Data
{
	public record Entity
	{
		/// <summary>
		/// Firma (nazwa) lub imię i nazwisko
		/// </summary>
		/// <value>Firma (nazwa) lub imię i nazwisko </value>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets Nip
		/// </summary>
		[JsonPropertyName("nip")]
		public string Nip { get; set; }

		/// <summary>
		/// Status podatnika VAT.
		/// </summary>
		/// <value>Status podatnika VAT. </value>
		[JsonPropertyName("statusVat")]
		public string StatusVat { get; set; }

		/// <summary>
		/// Numer identyfikacyjny REGON
		/// </summary>
		/// <value>Numer identyfikacyjny REGON </value>
		[JsonPropertyName("regon")]
		public string Regon { get; set; }

		/// <summary>
		/// Gets or Sets Pesel
		/// </summary>
		[JsonPropertyName("pesel")]
		public Pesel Pesel { get; set; }

		/// <summary>
		/// numer KRS jeżeli został nadany
		/// </summary>
		/// <value>numer KRS jeżeli został nadany </value>
		[JsonPropertyName("krs")]
		public string Krs { get; set; }

		/// <summary>
		/// Adres siedziby
		/// </summary>
		/// <value>Adres siedziby </value>
		[JsonPropertyName("residenceAddress")]
		public string ResidenceAddress { get; set; }

		/// <summary>
		/// Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności
		/// </summary>
		/// <value>Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności </value>
		[JsonPropertyName("workingAddress")]
		public string WorkingAddress { get; set; }

		/// <summary>
		/// Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL
		/// </summary>
		/// <value>Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL </value>
		[JsonPropertyName("representatives")]
		public List<EntityPerson> Representatives { get; set; }

		/// <summary>
		/// Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL
		/// </summary>
		/// <value>Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL </value>
		[JsonPropertyName("authorizedClerks")]
		public List<EntityPerson> AuthorizedClerks { get; set; }

		/// <summary>
		/// Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numeryNIP i/lub PESEL
		/// </summary>
		/// <value>Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numeryNIP i/lub PESEL </value>
		[JsonPropertyName("partners")]
		public List<EntityPerson> Partners { get; set; }

		/// <summary>
		/// Data rejestracji jako podatnika VAT
		/// </summary>
		/// <value>Data rejestracji jako podatnika VAT </value>
		[JsonPropertyName("registrationLegalDate")]
		public DateTime? RegistrationLegalDate { get; set; }

		/// <summary>
		/// Data odmowy rejestracji jako podatnika VAT
		/// </summary>
		/// <value>Data odmowy rejestracji jako podatnika VAT </value>
		[JsonPropertyName("registrationDenialDate")]
		public DateTime? RegistrationDenialDate { get; set; }

		/// <summary>
		/// Podstawa prawna odmowy rejestracji
		/// </summary>
		/// <value>Podstawa prawna odmowy rejestracji </value>
		[JsonPropertyName("registrationDenialBasis")]
		public string RegistrationDenialBasis { get; set; }

		/// <summary>
		/// Data przywrócenia jako podatnika VAT
		/// </summary>
		/// <value>Data przywrócenia jako podatnika VAT </value>
		[JsonPropertyName("restorationDate")]
		public DateTime? RestorationDate { get; set; }

		/// <summary>
		/// Podstawa prawna przywrócenia jako podatnika VAT
		/// </summary>
		/// <value>Podstawa prawna przywrócenia jako podatnika VAT </value>
		[JsonPropertyName("restorationBasis")]
		public string RestorationBasis { get; set; }

		/// <summary>
		/// Data wykreślenia odmowy rejestracji jako podatnika VAT
		/// </summary>
		/// <value>Data wykreślenia odmowy rejestracji jako podatnika VAT </value>
		[JsonPropertyName("removalDate")]
		public DateTime? RemovalDate { get; set; }

		/// <summary>
		/// Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT
		/// </summary>
		/// <value>Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT </value>
		[JsonPropertyName("removalBasis")]
		public string RemovalBasis { get; set; }

		/// <summary>
		/// Gets or Sets AccountNumbers
		/// </summary>
		[JsonPropertyName("accountNumbers")]
		public List<string> AccountNumbers { get; set; }

		/// <summary>
		/// Podmiot posiada maski kont wirtualnych
		/// </summary>
		/// <value>Podmiot posiada maski kont wirtualnych </value>
		[JsonPropertyName("hasVirtualAccounts")]
		public bool? HasVirtualAccounts { get; set; }
	}
}