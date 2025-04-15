using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AuthenticationRequest
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
    }
  
}
