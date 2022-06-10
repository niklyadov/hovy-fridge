using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HovyFridge.Services.Auth
{
    public class JwtTokensServiceConfiguration
    {
        public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromMinutes(10);
        public string JwtSecret { get; set; } = "B?E(H+MbQeThWmZq";
    }
}