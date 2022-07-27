using BanklyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanklyTest.Interface
{
   public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
        string FormatEmailBody(User user);
        string GetTemplateType(string template);
    }
}
