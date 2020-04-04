using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using IEEE_COVID19.API;
using IEEE_COVID19.ViewModels.Base;


namespace IEEE_COVID19.ViewModels
{
    public class PickerViewModel : ViewModelBase
    {
		public IList<AccountDetails> StateAccountDetails => StateAccounts.StateAccountDetails;



		private AccountDetails _selectedStateAccount;

		public AccountDetails SelectedStateAccount

		{

			get => _selectedStateAccount;

			set
			{
				_selectedStateAccount = value;

				RaisePropertyChanged(() => SelectedStateAccount);
			}

		}





	}

    
}


