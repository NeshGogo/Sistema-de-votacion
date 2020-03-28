using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
