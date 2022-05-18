using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TareaSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Eliminar : ContentPage
    {
        private const string Url = "http://192.168.1.3/Moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<TareaSemana6.Datos> _post;
        public Eliminar()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                int codigo = int.Parse(txtCodigo.Text.ToString());
                var content = await client.GetStringAsync(Url + "?codigo=" + codigo);
                content = "[" + content + "]";
                List<TareaSemana6.Datos> posts = JsonConvert.DeserializeObject<List<TareaSemana6.Datos>>(content);
                _post = new ObservableCollection<TareaSemana6.Datos>(posts);

                if (_post.Count > 0)
                {
                    txtCodigo.IsReadOnly = true;

                    Datos data = new Datos();

                    data = posts.FirstOrDefault();

                    txtNombre.Text = data.nombre.ToString();
                    txtApellido.Text = data.apellido.ToString();
                    txtEdad.Text = data.edad.ToString();
                }
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                    DisplayAlert("Alerta", "No ha seleccionado ningun estudiante.", "Ok");
                else
                {
                    WebClient cliente = new WebClient();

                    string parametros = "";

                    parametros += "?codigo=" + txtCodigo.Text;

                    var urlCompleta = new Uri(Url + parametros);

                    cliente.UploadString(urlCompleta, "DELETE", "");

                    DisplayAlert("Alerta", "Registro eliminado correctamente.", "Ok");

                    txtCodigo.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtEdad.Text = "";
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Mensaje de alerta " + ex.Message, "Ok");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }
    }
}