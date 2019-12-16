using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Plugin.Connectivity;
using Plugin.Vibrate;
using Plugin;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http;
using System.Threading.Tasks;

using RestSharp;


namespace TrackinFull2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private Usuario oUser = null;


        public MainPage()
        {
            InitializeComponent();
            Connected();
            if (Application.Current.Properties.ContainsKey("user") && Application.Current.Properties["user"] != null)
            {
                oUser = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(Application.Current.Properties["user"].ToString());
            }
            Page("login");
            if (oUser != null)
            {
                initUser(oUser);
            }


        }

        async void Connected()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                bool answer = await DisplayAlert("Aviso sin coneccion", "La aplicacion requiere coneccion para trabajar", "Reintentar", "Salir");
                if (answer) Connected();
                else
                {
                    System.Environment.Exit(0);
                }
            }
        }

        private void Back(object sender, EventArgs e)
        {
            Scanner("back");
        }
        private void Next(object sender, EventArgs e)
        {
            Scanner("next");
        }
        private void End(object sender, EventArgs e)
        {
            Page("code");
        }
        private void EndSend(object sender, EventArgs e)
        {

            Scanner("end", textcode.Text);

        }

        private void Login(object sender, EventArgs e)
        {

            Loader(true);
            if (textpassword.Text == "")
            {
                Loader(false);
                DisplayAlert("Error", "Falta la contraseña", "OK");
            }
            else if (textusers.Text == "")
            {
                Loader(false);
                DisplayAlert("Error", "Falta el usuario", "OK");
            }
            else
            {
                Connected();
                try
                {
                    var client = new RestClient("http://trackingfullapi2019.azurewebsites.net/api/auth/login");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", "{\"Email\":\"" + textusers.Text  + "\",\"Password\":\"" + textpassword.Text  + "\"}", ParameterType.RequestBody);
                    IRestResponse res = client.Execute(request);
                    if (res.StatusCode.ToString() == "OK")
                    {
                        Loader(false);

                        if (null != res.Content)
                        {
                            initUser(Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(res.Content));
                        }
                        else
                        {
                            DisplayAlert("Error", "Algo a fallado", "OK");
                        }
                    }
                    else
                    {
                        Loader(false);
                        DisplayAlert("Usuario no encontrado", "Verifique su usaurio o contraseña", "OK");
                    }

                }
                catch (Exception)
                {
                    DisplayAlert("Error", "algo salio re mal", "OK");
                }
            }
        }

        private async void Loader(Boolean res)
        {
            if (res)
            {
                IsLoad.IsVisible = true;
                IsLoad.IsRunning = true;
            } else
            {
                IsLoad.IsRunning = false;
                IsLoad.IsVisible = false;
            }
        }

        public void initUser(Usuario user)
        {
            oUser = user;

            Application.Current.Properties["user"] = Newtonsoft.Json.JsonConvert.SerializeObject(oUser);
            Application.Current.SavePropertiesAsync();
            saludo.Text = "Hola " + oUser.Email;
            Page("home");

        }

        private void Logout(object sender, EventArgs e)
        {

            oUser = null;
            Application.Current.Properties["user"] = null;
            Application.Current.SavePropertiesAsync();
            Page("login");
        }

        private async void Scanner(string option, string code = "")
        {
            Connected();
            var scannerPage = new ZXingScannerPage();
            scannerPage.Title = "Escaneando paquete";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    Loader(true);
                    if (CrossVibrate.IsSupported && CrossVibrate.Current.CanVibrate) CrossVibrate.Current.Vibration(new TimeSpan(0, 0, 0, 15));
                    string uri = "";
                    if (option == "back") uri = "api/paquete/retroceder";
                    else if (option == "next" || option == "end") uri = "api/paquete/avanzar";
                    else  uri = "api/paquete/entregacliente";
                    if (!ApiReset.changeStatePack(uri, result.Text, oUser.Id.ToString(), oUser.Token, code))
                    {
                        Page("home");
                        Loader(false);
                        DisplayAlert("Error", result.Text, "OK");
                    } else
                    {
                        Page("exito");
                        Loader(false);
                    }
                });
            };

            await Navigation.PushAsync(scannerPage);
        }

        /**
         * param name="page" value 'login' | 'home' | 'code' | 'exito'
         */
        private void Page(string page)
        {

//            textpassword.FadeTo(1, 1000, Easing.SpringOut);

            if (page == "login")
            {
                textpassword.Text = "";
                textusers.Text = "";
                saludo.Text = "";

                textpassword.IsVisible = true;
                textusers.IsVisible = true;
                btnLogin.IsVisible = true;
                saludo.IsVisible = false;
            }
            else
            {
                textpassword.IsVisible = false;
                textusers.IsVisible = false;
                btnLogin.IsVisible = false;
                saludo.IsVisible = true;
            }

            if (page == "home")
            {
                btnBack.IsVisible = true;
                btnNext.IsVisible = true;
                btnEnd.IsVisible = true;
                btnlogout.IsVisible = true;
            }
            else
            {
                btnBack.IsVisible = false;
                btnNext.IsVisible = false;
                btnEnd.IsVisible = false;
                btnlogout.IsVisible = false;

            }
            if (page == "code")
            {
                LabelSubTitle.IsVisible = true;
                textcode.IsVisible = true;
                btnEndSend.IsVisible = true;
            }
            else
            {
                LabelSubTitle.IsVisible = false;
                textcode.IsVisible = false;
                btnEndSend.IsVisible = false;
            }
            if (page == "exito")
            {
                resulado.IsVisible = true;
                btnreultado.IsVisible = true;
                imgresulado.IsVisible = true;
            }
            else
            {
                imgresulado.IsVisible = false;
                btnreultado.IsVisible = false;
                resulado.IsVisible = false;
            }



        }
    }
}
