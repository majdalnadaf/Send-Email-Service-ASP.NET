﻿namespace SendEmailService.NewFolder
{
    public class SendEmailSettingsModel
    {
        public string Email { get; set; }
        public string DispalyName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string AuthenticationRequired { get; set; }
    }
}
