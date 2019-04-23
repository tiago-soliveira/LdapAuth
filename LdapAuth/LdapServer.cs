using System;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Net;
using System.Security.Permissions;


namespace LdapAuth
{
	[Serializable]
    public class LdapServer
    {

        public LdapServer()
        {
            this._hostName = Settings.Default.Server;
            this._port = Convert.ToInt32(Settings.Default.Port);
            this._baseDn = Settings.Default.BaseDn;
            this._filter = "uid={0}";
            this._loginDn = Settings.Default.Login;
            this._pwdDn = Settings.Default.Password;
        }

        private string _hostName;
        private int _port;
        private string _loginDn;
        private string _pwdDn;
        private string _baseDn;
        private string _filter;

        [SecurityPermission(SecurityAction.Demand)]
        public LdapUser Authenticate(string userName, string password)
        {
            using (var ldap = new LdapConnection(new LdapDirectoryIdentifier(_hostName, _port)))
            {
                ldap.SessionOptions.ProtocolVersion = 3;

                ldap.AuthType = AuthType.Basic;
                ldap.Bind(new NetworkCredential(_loginDn, _pwdDn));
                var dn = GetDn(ldap, userName);

                if (dn != null)
                {
                    try
                    {
                        ldap.AuthType = AuthType.Basic;
                        ldap.Bind(new NetworkCredential(dn, password));
                        return GetUser(ldap, dn);
                    }
                    catch (DirectoryOperationException ex1)
                    {
                        //Invalid user.
                        //Debug.WriteLine(ex1.Message);
                        throw ex1;
                    }
                    catch (LdapException ex2)
                    {
                        //Invalid password.
                        //Debug.WriteLine(ex2.Message);
                        throw ex2;
                    }
                }
            }

            return null;
        }

        private String GetDn(LdapConnection ldap, String userName)
        {
            var request = new SearchRequest(_baseDn, string.Format(CultureInfo.InvariantCulture, _filter, userName), SearchScope.Subtree);
            var response = (SearchResponse)ldap.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                return response.Entries[0].DistinguishedName;
            }
            return null;
        }

        private LdapUser GetUser(LdapConnection ldap, String dn)
        {
            var request = new SearchRequest(dn, "(objectclass=*)", SearchScope.Base);
            var response = (SearchResponse)ldap.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                return new LdapUser(response.Entries[0]);
            }
            return null;
        }

    }
}
