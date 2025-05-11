﻿namespace Auth.Service.Models
{
    public enum Role
    {
        Admin = 1
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
