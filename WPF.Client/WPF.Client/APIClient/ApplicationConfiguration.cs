﻿using System;

namespace KF.WPF.Client.APIClient
{
    public class ApplicationConfiguration : IClientApplicationConfiguration
    {
        public string ServerAddress => "https://localhost:7036";

        public string AppUri => "AppUri";

        public string ClientId => "ClientId";
      
    }
}
