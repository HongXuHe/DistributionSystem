﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Model
{
    public class ConsentViewModel
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientLogoUrl { get; set; }
        public bool AllowRememberConsent { get; set; }
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeViewModel> ResorceScopes { get; set; }
        public string ReturnUrl { get; set; }
    }
}
