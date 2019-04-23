using System;
using System.Configuration;
using System.DirectoryServices.Protocols;


namespace LdapAuth
{
	[Serializable]
    public class LdapAuthentication
    {

        private string _username;
        private string _password;

        public LdapAuthentication(string usr, string pwd)
        {
            this._username = usr;
            this._password = pwd;
        }

        public Output IsAuthenticated()
        {
            var ldap = new LdapServer();

            try
            {
                var user = ldap.Authenticate(_username, _password);
                if (user == null)
                {
                    return new Output { Success = false, Message = "Falha na autenticação com o LDAP. Usuário ou senha incorreto.\r\n" };
                }

                return new Output { Success = true, Message = "Olá, " + user.DisplayName };

            }
            catch (LdapException ldapEx)
            {
                return new Output { Success = false, Message = ldapEx.Message, StackTrace = ldapEx.StackTrace, Source = ldapEx.Source };
                //throw ldapEx;
            }
            catch (Exception Ex)
            {
                return new Output { Success = false, Message = Ex.Message, StackTrace = Ex.StackTrace, Source = Ex.Source };
                //throw Ex;
            }
        }
    }
}
