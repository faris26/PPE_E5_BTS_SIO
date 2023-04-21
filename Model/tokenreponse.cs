using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class tokenreponse
    {
        public tokenreponse()
        {
            this.Token = string.Empty;
            this.ReponseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }

        public string Token { get; set; }
        public HttpResponseMessage ReponseMsg { get; set; }
    }
}