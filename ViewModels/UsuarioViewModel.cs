using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppBindingCommands.ViewModels
{
    public class UsuarioViewModel : INotifyPropertyChanged //InterfaceNotify... precisa de using System.ComponentModel;
    {
        public UsuarioViewModel()
        {
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(async () => await CountCaracters());
            CleanCommand = new Command(async () => await CleanConfirmation());
            OptionComman = new Command(async () => await ShowOptions());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) // quando ocorrere alguma mudança irá mapear a mudança na informação
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name = string.Empty;  // Comando para geração automatica de Get e Set (ctrl + r + e) --> cria propriedade

        //propriedade
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName => $"Nome digitado: {Name}";

        private string displayMessage = string.Empty;
        public string DisplayMessage
        {
            get => displayMessage;
            set
            {
                if (displayMessage == value)
                    return;

                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }
        public ICommand ShowMessageCommand { get; }

        public void ShowMessage()
        {
            DateTime data = DateTime.Now;
            DisplayMessage = $"Boa Noite {Name}, Hoje é {data}";
        }
       


        //  1 - Metodo para contar os caracteres
        public async Task CountCaracters()
        {
            string nameLenght =
                string.Format("Seu nome tem {0} Letras", name.Length);

            await Application.Current
                .MainPage.DisplayAlert("Informação", nameLenght, "OK");
        }
        public ICommand CountCommand { get; }



        //   2 - Metodo para exibir um alert ao usuário a resposta sendo positiva, limpará os campos da tela
        public async Task CleanConfirmation()
        {
            if (await Application.Current.MainPage
                .DisplayAlert("Confirmação","Confirma limpeza dos dados ?","YES","NO"))
            {
                Name = string.Empty;
                DisplayMessage = string.Empty;
                OnPropertyChanged(Name);
                OnPropertyChanged(DisplayMessage);

                await Application.Current.MainPage
                    .DisplayAlert("Informação", "Limpeza realizada com sucesso", "Ok");
            }
        }
        public ICommand CleanCommand { get; }



        //   3 - Metodo para ..
        public async Task ShowOptions()
        {
            string result = await Application.Current.MainPage.DisplayActionSheet("Selecione uma opção:", "", "Cancelar", "Limpar", "Contar Caracteres", "Exibir Saudação");

            if(result != null)
            {
                if (result.Equals("Limpar"))
                    await CleanConfirmation();
                if (result.Equals("Contar Caracteres"))
                    await CountCaracters();
                if (result.Equals("Exibir Saudação"))
                    ShowMessage();
            }
        }
        public ICommand OptionCommand { get; }

    }
}
