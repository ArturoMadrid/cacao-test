using models.cacao;
using System.Collections.Generic;

namespace web.cacao.Data
{
    public class ConsumeApi
    {
        readonly string urlApi = @"https://localhost:44322";

        public Response GetStudents()
        {
            var response = Utilities.ObtenerHtmlResponse($"{urlApi}/api/students", Utilities.Metodo.GET);
            
            return Utilities.DeserializarJson<Response>(response);
        }

        public Response SaveStudent(Student info)
        {
            var response = Utilities.ObtenerHtmlResponse($"{urlApi}/api/students", Utilities.Metodo.POST, Utilities.SerializarJson(info));

            return Utilities.DeserializarJson<Response>(response);
        }
    }
}
