using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IEEE_COVID19.API
{
    public static class StateAccounts
    {
        public static IList<AccountDetails> StateAccountDetails {get; private set;}

        static StateAccounts()
        {
              StateAccountDetails=new List<AccountDetails>();

            StateAccountDetails.Add(new AccountDetails
            {
                StateName= "Andhra Pradesh",
                AccountHolderName = "Chief Minister Relief Fund, Andhra Pradesh",
                AccountNumber = "38588079208",
                IFSCCode = "SBIN0018884",
                BranchName = "Velagapudi Secretariat Branch"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Arunachal Pradesh",
                AccountHolderName = "Chief Minister's Relief Fund",
                AccountNumber = "10940061389",
                IFSCCode = "SBIN0006091",
                BranchName = ""
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Assam",
                AccountHolderName = "Assam Arogya Nidhi",
                AccountNumber = "32124810101",
                IFSCCode = "SBIN0010755",
                BranchName = ""
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Bihar",
                AccountHolderName = "Chief Minister's Bihar Relief Fund",
                AccountNumber = "2065104000002257",
                IFSCCode = "IBKL0002065",
                BranchName = "Kidwaipuri,Patna"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Chhattisgarh",
                AccountHolderName = "CM's Relief Fund for Corona",
                AccountNumber = "30198873179",
                IFSCCode = "SBIN0004286",
                BranchName = ""
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Delhi",
                AccountHolderName = "Delhi CM Relief Fund",
                AccountNumber = "91042150000237",
                IFSCCode = "SYNB0009104",
                BranchName = "Secretariat Branch"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Goa",
                AccountHolderName = "CM's Covid Relief Fund",
                AccountNumber = "39235546238",
                IFSCCode = "SBIN0010719",
                BranchName = "Vidhan Bhawan, Panaji"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Gujarat",
                AccountHolderName = "Chief Minister's Relief Fund",
                AccountNumber = "10354901554",
                IFSCCode = "SBIN0008434",
                BranchName = "NSC BRANCH"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = " Haryana",
                AccountHolderName = "Haryana Corona Relief Fund",
                AccountNumber = "39234755902",
                IFSCCode = "SBIN0001509",
                BranchName = "SBI, Sector - 10, Panchkula"
            });
            StateAccountDetails.Add( new AccountDetails
            {
                StateName = "Himachal Pradesh ",
                AccountHolderName = "HP COVID-19 Solidarity Response Fund",
                AccountNumber = "50100340267282",
                IFSCCode = "HDFC0004116",
                BranchName = "Chotta Shimla"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Jharkhand",
                AccountHolderName = "Chief Minister Relief Fund",
                AccountNumber = "11049021058",
                IFSCCode = "SBIN0000167",
                BranchName = "Ranchi"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Karnataka",
                AccountHolderName = "Chief Minister Relief Fund Covid-19",
                AccountNumber = "39234923151",
                IFSCCode = "SBIN0040277",
                BranchName = "Vidhana Soudha "
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Madhya Pradesh",
                AccountHolderName = "Chief Minister Relief Fund (Covid19)",
                AccountNumber = "10078152483",
                IFSCCode = "SBIN0001056",
                BranchName = "Vallabh Bhawan"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Maharashtra",
                AccountHolderName = "Chief Minister's Relief Fund- COVID 19",
                AccountNumber = "39239591720",
                IFSCCode = "SBIN0000300",
                BranchName = "Main Branch, Fort, Mumbai - 400023"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Manipur",
                AccountHolderName = "Chief Minister's COVID-19 Relief Fund",
                AccountNumber = "70600875695",
                IFSCCode = "YESB0MSCB01",
                BranchName = ""
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Meghalaya",
                AccountHolderName = "Meghalaya CM's Relief Fund",
                AccountNumber = "38617186405",
                IFSCCode = "SBIN0006320",
                BranchName = "Meghalaya Secretariat"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Mizoram",
                AccountHolderName = "Chief Minister's Relief Fund",
                AccountNumber = "18141450000025",
                IFSCCode = "HDFC0001814",
                BranchName = "Aizawl"
            }); StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Nagaland",
                AccountHolderName = "Chief Minister's Relief Fund",
                AccountNumber = "10530527879",
                IFSCCode = "SBIN0000214",
                BranchName = " Kohima"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Odisha ",
                AccountHolderName = "Chief Minister Relief Fund (For-Covid 19)",
                AccountNumber = "39235504967",
                IFSCCode = "SBIN0010236",
                BranchName = ""
            }); StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Rajasthan",
                AccountHolderName = "Rajasthan CMRF Covid-19 Mitigation Fund",
                AccountNumber = "39233225397",
                IFSCCode = "SBIN0031031",
                BranchName = "Secretariat Branch, Jaipur"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Tamil Nadu",
                AccountHolderName = "Government of Tamil Nadu CM's Public Relief Fund (CMPRF)",
                AccountNumber = "11720 10000 00070",
                IFSCCode = "IOBA0001172",
                BranchName = "Secretariat Branch, Chennai 600 009"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Telangana",
                AccountHolderName = "Telangana Chief Minister Relief Fund",
                AccountNumber = "62354157651",
                IFSCCode = " SBIN0020077",
                BranchName = "Secretariat, Hyderabad, Telangana"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Tripura",
                AccountHolderName = "Chief Minister's Relief Fund- Tripura",
                AccountNumber = "37939987790",
                IFSCCode = "SBIN0016355",
                BranchName = "New Secretariat Branch "
            });
            StateAccountDetails.Add( new AccountDetails
            {
                StateName = "Uttar Pradesh",
                AccountHolderName = "Chief Minister Distress Relief Fund",
                AccountNumber = "1378820696",
                IFSCCode = "CBIN0281571",
                BranchName = "C.B.I. Cantt. Road, Lucknow"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Uttarakhand",
                AccountHolderName = "Mukhya Mantri Rahat Kosh Uttarakhand",
                AccountNumber = "30395954328",
                IFSCCode = "30395954328",
                BranchName = ""
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "West Bengal",
                AccountHolderName = "West Bengal State Emergency Relief Fund",
                AccountNumber = "628005501339",
                IFSCCode = "ICIC0006280",
                BranchName = "Howrah"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Jammu and Kashmir",
                AccountHolderName = "J& K Relief Fund",
                AccountNumber = "0110010100000016",
                IFSCCode = "JAKA0MOVING",
                BranchName = "Moving Secretariat"
            });
            StateAccountDetails.Add(new AccountDetails
            {
                StateName = "Punjab",
                AccountHolderName = "Punjab Chief Minister Relief Fund - Covid 19",
                AccountNumber = "50100333026124",
                IFSCCode = "HDFC0000213",
                BranchName = "Chandigarh, Sector 17-C"
            });






        }
        

            
    }
}

