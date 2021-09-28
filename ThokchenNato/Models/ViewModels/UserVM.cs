using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThokchenNato.Models.Data;

namespace ThokchenNato.Models.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {

        }
        public UserVM(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            Username = user.Username;
            Password = user.Password;
        }

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}