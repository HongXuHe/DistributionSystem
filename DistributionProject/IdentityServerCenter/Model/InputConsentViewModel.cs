﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Model
{
    public class InputConsentViewModel
    {
        public string Button { get; set; }
        public IEnumerable<string> ScopedConsent { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
    }
}
