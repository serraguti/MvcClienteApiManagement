using MvcClienteApiManagement.Models;
using System.Net.Http.Headers;
using System.Web;

namespace MvcClienteApiManagement.Services
{
    public class ServiceApiManagement
    {
        private string ApiUrlEmpleados;
        private string ApiUrlDepartamentos;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiManagement(string urlemp, string urldept)
        {
            this.ApiUrlEmpleados = urlemp;
            this.ApiUrlDepartamentos = urldept;
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //TENEMOS QUE MODIFICAR EL REQUEST.  DEBEMOS ENVIAR AL FINAL
                //UNA CADENA VACIA PARA LA PETICION
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                string request = "/api/empleados?" + queryString;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //DEBEMOS INDICAR QUE NO UTILIZAMOS CACHE PARA LAS PETICIONES
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //LA PETICION AL API MANAGEMENT SE REALIZA CON LA URL Y EL REQUEST
                //A LA VEZ
                HttpResponseMessage response =
                    await client.GetAsync(this.ApiUrlEmpleados + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados =
                        await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync(string suscripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                string request = "/api/departamentos?" + queryString;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.CacheControl =
                     CacheControlHeaderValue.Parse("no-cache");
                //AÑADIMOS NUESTRA CLAVE DE SUBSCRIPCION
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", suscripcion);
                HttpResponseMessage response =
                    await client.GetAsync(this.ApiUrlDepartamentos + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Departamento> departamentos =
                        await response.Content.ReadAsAsync<List<Departamento>>();
                    return departamentos;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
