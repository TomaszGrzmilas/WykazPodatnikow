using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WykazPodatnikow.Data;

namespace WykazPodatnikow.Core
{
	public class VatWhiteList
	{
		private readonly HttpClient httpClient;
		private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		public VatWhiteList(HttpClient httpClient, string url = "https://wl-api.mf.gov.pl")
		{
			httpClient.BaseAddress = new Uri(url);
			this.httpClient = httpClient;
		}

		/// <summary>
		/// Wyszukiwanie pojedynczego podmiotu po nip.
		/// </summary>
		/// <param name="nip"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityResponse> GetDataFromNipAsync(string nip, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				var Getresult = await httpClient.GetAsync($"/api/search/nip/{nip}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Wyszukiwanie podmiotow po numerach nip.
		/// </summary>
		/// <param name="nips"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityListResponse> GetDataFromNipsAsync(string nips, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				var Getresult = await httpClient.GetAsync($"/api/search/nips/{nips}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityListResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityListResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Wyszukiwanie pojedynczego podmiotu po regon.
		/// </summary>
		/// <param name="regon"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityResponse> GetDataFromRegonAsync(string regon, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				if (!Extension.IsValidREGON(regon))
					return new EntityResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid Regon" } };

				var Getresult = await httpClient.GetAsync($"/api/search/regon/{regon}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Wyszukiwanie podmiotow po numerach regon.
		/// </summary>
		/// <param name="regons"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityListResponse> GetDataFromRegonsAsync(string regons, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				foreach (var item in regons.Split(","))
				{
					if (!Extension.IsValidREGON(item))
						return new EntityListResponse { Exception = new WhiteListCheckException { Code = "6", Message = $"Invalid Regon: {item}" } };
				}

				var Getresult = await httpClient.GetAsync($"/api/search/regons/{regons}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityListResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityListResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Zwraca EntityListResponse na podstawie podanego konta bankowego.
		/// </summary>
		/// <param name="bankAccount"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityListResponse> GetDataFromBankAccountAsync(string bankAccount, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				if (!Extension.IsValidBankAccountNumber(bankAccount))
					return new EntityListResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid Bank Accounts" } };

				var Getresult = await httpClient.GetAsync($"/api/search/bank-account/{bankAccount}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityListResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityListResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Wyszukiwanie podmiotow po numerach kont.
		/// </summary>
		/// <param name="bankAccounts"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityListResponse> GetDataFromBankAccountsAsync(string bankAccounts, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				foreach (var item in bankAccounts.Split(","))
				{
					if (!Extension.IsValidBankAccountNumber(item))
						return new EntityListResponse { Exception = new WhiteListCheckException { Code = "6", Message = $"Invalid Bank Account: {item}" } };
				}

				var Getresult = await httpClient.GetAsync($"/api/search/bank-accounts/{bankAccounts}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityListResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityListResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Sprawdzenie pojedynczego podmiotu po nip i numerze konta.
		/// </summary>
		/// <param name="nip"></param>
		/// <param name="bankAccount"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityCheckResponse> CheckFromNipAndBankAccountsAsync(string nip, string bankAccount, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				if (!Extension.IsValidNIP(nip))
					return new EntityCheckResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid nip" } };

				if (!Extension.IsValidBankAccountNumber(bankAccount))
					return new EntityCheckResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid Bank Account" } };

				var Getresult = await httpClient.GetAsync($"/api/check/nip/{nip}/bank-account/{bankAccount}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityCheckResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityCheckResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Sprawdzenie pojedynczego podmiotu po regon i numerze konta
		/// </summary>
		/// <param name="regon"></param>
		/// <param name="bankAccount"></param>
		/// <param name="DateOnly"></param>
		/// <returns></returns>
		public async Task<EntityCheckResponse> CheckFromRegonAndBankAccountsAsync(string regon, string bankAccount, DateOnly DateOnly)
		{
			string GetString = string.Empty;
			try
			{
				if (!Extension.IsValidREGON(regon))
					return new EntityCheckResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid regon" } };

				if (!Extension.IsValidBankAccountNumber(bankAccount))
					return new EntityCheckResponse { Exception = new WhiteListCheckException { Code = "6", Message = "Invalid Bank Account" } };

				var Getresult = await httpClient.GetAsync($"/api/check/regon/{regon}/bank-account/{bankAccount}?date={DateOnly.ToString("yyyy-MM-dd")}");

				GetString = await Getresult.Content.ReadAsStringAsync();

				if (Getresult.IsSuccessStatusCode)
				{
					return JsonSerializer.Deserialize<EntityCheckResponse>(GetString, JsonSerializerOptions);
				}
				return new EntityCheckResponse { Exception = JsonSerializer.Deserialize<WhiteListCheckException>(GetString, JsonSerializerOptions) };
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}