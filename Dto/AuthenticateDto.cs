﻿namespace AstraLicenceManager.Dto
{
    public class AuthenticateDto : MessageDto 
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string? Token { get; set; }
       
    }
    
}
