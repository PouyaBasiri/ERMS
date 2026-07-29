using ERMS.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure.Persistence.Converters
{
    public sealed class EmailConverter : ValueConverter<Email, string> 
    { 
        public EmailConverter() : base(email => email.Value, value => Email.Create(value).Value) 
        {
        } 
    }
}
