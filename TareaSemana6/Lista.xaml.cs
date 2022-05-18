using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TareaSemana6
{
    public partial class Lista : ContentPage
    {
        private const string Url = "http://192.168.1.3/Moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<TareaSemana6.Datos> _post;

        public Lista()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(Url);
            List<TareaSemana6.Datos> posts = JsonConvert.DeserializeObject<List<TareaSemana6.Datos>>(content);
            _post = new ObservableCollection<TareaSemana6.Datos>(posts);

            MyListView.ItemsSource = _post;
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Modificar());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Eliminar());
        }
    }
}