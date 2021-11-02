using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WykazPodatnikow.Data;  

namespace WykazPodatnikow.Core
{
	public class VatWhiteListFlatFile
	{
		private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		public static FlatFileData flatFileData;

		public static async Task LoadFlatFileAsync(string PathToJson)
		{
			if (!File.Exists(PathToJson))
				throw new System.Exception("Json file not found");

			try
			{
				using FileStream s = new FileStream(PathToJson, FileMode.Open, FileAccess.Read);
				flatFileData = await JsonSerializer.DeserializeAsync<FlatFileData>(s, JsonSerializerOptions);
			}
			catch (System.Exception)
			{
				throw;
			}

			if (flatFileData == null)
				throw new System.Exception("Deserialize error, check if Json file is valide");

			if (string.IsNullOrEmpty(flatFileData.Naglowek?.DataGgenerowaniaDanych))
				throw new System.Exception("Invalide Json file, datagenerowaniadanych is empty");

			if (flatFileData.Maski == null || flatFileData.Maski.Count <= 0)
				throw new System.Exception("Invalide Json file, maski is empty");

			if (flatFileData.SkrotyPodatnikowCzynnych == null || flatFileData.SkrotyPodatnikowCzynnych.Count <= 0)
				throw new System.Exception("Invalide Json file, skrotypodatnikowczynnych is empty");

			if (flatFileData.SkrotyPodatnikowZwolnionych == null || flatFileData.SkrotyPodatnikowZwolnionych.Count <= 0)
				throw new System.Exception("Invalide Json file, skrotypodatnikowzwolnionych is empty");
		}

		public FlatFile IsInFlatFile(string nip, string bankAccount)
		{
			if (!(flatFileData?.SkrotyPodatnikowCzynnych?.Count >= 0))
				throw new System.Exception("Json file is not loaded. Use first LoadFlatFileAsync()");

			if (!nip.IsValidNIP())
				return FlatFile.InvalidNip;

			if (string.IsNullOrEmpty(bankAccount) || string.IsNullOrWhiteSpace(bankAccount) || bankAccount?.Length < 26)
				return FlatFile.InvalidBankAccount;

			switch (CheckInBody(bankAccount))
			{
				case FlatFile.FoundInActiveVatPayer:
					return FlatFile.FoundInActiveVatPayer;

				case FlatFile.FoundInExemptVatPayer:
					return FlatFile.FoundInExemptVatPayer;

				case FlatFile.InvalidNip:
					return FlatFile.InvalidNip;

				case FlatFile.InvalidBankAccount:
					return FlatFile.InvalidBankAccount;

				case FlatFile.NotFound:
					break;

				default:
					break;
			}

			string bankBranchNumber = bankAccount.Substring(2, 8);

			foreach (var item in flatFileData.Maski)
			{
				if (bankBranchNumber.Equals(item.Substring(2, 8), StringComparison.OrdinalIgnoreCase))
				{
					int IndexFrom = item.IndexOf("Y");
					int range = item.Count(p => p.Equals('Y'));
					string VirtualAccount = Regex.Replace(item, "Y\\w*Y", bankAccount.Substring(IndexFrom, range));

					FlatFile checkResult = CheckInBody(VirtualAccount);

					if (checkResult == FlatFile.NotFound)
						continue;
					else
						return checkResult;
				}
			}

			return FlatFile.NotFound;

			FlatFile CheckInBody(string account)
			{
				string hash = (flatFileData.Naglowek.DataGgenerowaniaDanych + nip + account).SHA512(Convert.ToInt32(flatFileData.Naglowek.LiczbaTransformacji));

				foreach (var item in flatFileData.SkrotyPodatnikowCzynnych)
				{
					if (item.Equals(hash, StringComparison.OrdinalIgnoreCase))
					{
						return FlatFile.FoundInActiveVatPayer;
					}
				}

				foreach (var item in flatFileData.SkrotyPodatnikowZwolnionych)
				{
					if (item.Equals(hash, StringComparison.OrdinalIgnoreCase))
					{
						return FlatFile.FoundInExemptVatPayer;
					}
				}

				return FlatFile.NotFound;
			}
		}
	}
}