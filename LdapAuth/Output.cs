using LdapAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LdapAuth
{
    public class Output
    {
        public bool Success { get; set; }

        public int IdRetorno { get; set; }
        public object objRetorno { get; set; }
        public string CodRetornoAuxiliar { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }

        public string Error
        {
            get
            {
                string error = string.Empty;
                error += !string.IsNullOrEmpty(Message) ? "Mensagem: " + Message : "";
                error += !string.IsNullOrEmpty(Source) ? @"\r\n - " + "Erro! " + "Source: " + Source : "";
                error += !string.IsNullOrEmpty(StackTrace) ? @"\r\n - " + "StackTrace: " + StackTrace : "";

                //return error.Replace("\\", "\"").Replace(System.Environment.NewLine, @"\r\n");
                var err = Regex.Replace(error, @"(?:\r\n|[\r\n]|\r(?!\n)|(?<!\r)\n){2,}", @"\r\n");
                err = Regex.Replace(err, System.Environment.NewLine, @"\r\n");
                err = err.Replace("" + (char)13, @"\r\n");
                err = err.Replace("" + (char)0x0D, @"\r\n");
                err = err.Replace(((char)0x0D).ToString(), @"\r\n");
                err = Regex.Replace(err, @"\s+", @"\r\n");
                return err;
            }
        }
    }
}
