﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WykazPodatnikow.Data;
using WykazPodatnikow.SharedLib;

namespace WykazPodatnikow.Standard
{
    public class VatWhiteListFlatFile
    {
        private readonly FlatFileData flatFileData;

        public VatWhiteListFlatFile(string PathToJson)
        {
            if (!File.Exists(PathToJson))
                throw new System.Exception("Json file not found");

            try
            {
                flatFileData = JsonConvert.DeserializeObject<FlatFileData>(File.ReadAllText(PathToJson));
            }
            catch (System.Exception)
            {
                throw;
            }

            if (flatFileData == null)
                throw new System.Exception("Deserialize error, check if Json file is valide");

            if (string.IsNullOrEmpty(flatFileData.naglowek?.datagenerowaniadanych))
                throw new System.Exception("Invalide Json file, datagenerowaniadanych is empty");

            if (flatFileData.maski == null || flatFileData.maski.Count <= 0)
                throw new System.Exception("Invalide Json file, maski is empty");

            if (flatFileData.skrotypodatnikowczynnych == null || flatFileData.skrotypodatnikowczynnych.Count <= 0)
                throw new System.Exception("Invalide Json file, skrotypodatnikowczynnych is empty");

            if (flatFileData.skrotypodatnikowzwolnionych == null || flatFileData.skrotypodatnikowzwolnionych.Count <= 0)
                throw new System.Exception("Invalide Json file, skrotypodatnikowzwolnionych is empty");
        }

        public FlatFile IsInFlatFile(string nip, string bankAccount)
        {
            if (!nip.IsValidNIP())
                return FlatFile.InvalidNip;

            if (!Extension.IsValidBankAccountNumber(bankAccount))
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
            string maskToCompare = string.Empty;

            foreach (var item in flatFileData.maski)
            {
                if (bankBranchNumber.Equals(item.Substring(2, 8), StringComparison.OrdinalIgnoreCase))
                {
                    maskToCompare = item;
                    break;
                }
            }

            if (string.IsNullOrEmpty(maskToCompare))
                return FlatFile.NotFound;

            int IndexFrom = maskToCompare.IndexOf("Y");
            int range = maskToCompare.Count(p => p.Equals('Y'));
            string VirtualAccount = Regex.Replace(maskToCompare, "Y.Y", bankAccount.Substring(IndexFrom, range));

            switch (CheckInBody(VirtualAccount))
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
                    return FlatFile.NotFound;

                default:
                    return FlatFile.NotFound;
            }

            FlatFile CheckInBody(string account)
            {
                string hash = (flatFileData.naglowek.datagenerowaniadanych + nip + account).SHA512();

                foreach (var item in flatFileData.skrotypodatnikowczynnych)
                {
                    if (item.Equals(hash, StringComparison.OrdinalIgnoreCase))
                    {
                        return FlatFile.FoundInActiveVatPayer;
                    }
                }

                foreach (var item in flatFileData.skrotypodatnikowzwolnionych)
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