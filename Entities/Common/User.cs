﻿using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    public class User : AppUser
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public override string? PhotoUrl { get => base.PhotoUrl; set => base.PhotoUrl = value; }
    }
}
