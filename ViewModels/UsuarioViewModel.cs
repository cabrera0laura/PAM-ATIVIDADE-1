using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBindingCommands.ViewModels
{
    public class UsuarioViewModel : INotifyPropertyChanged //InterfaceNotify... precisa de using System.ComponentModel;
    {
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
    }
}
