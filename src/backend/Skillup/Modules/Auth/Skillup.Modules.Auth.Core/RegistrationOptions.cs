﻿namespace Skillup.Modules.Auth.Core
{
    public class RegistrationOptions
    {
        public bool Enabled { get; set; }
        public IEnumerable<string> InvalidEmailProviders { get; set; }
    }
}
