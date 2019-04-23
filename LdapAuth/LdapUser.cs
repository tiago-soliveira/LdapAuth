using System;
using System.Collections;
using System.Diagnostics;
using System.DirectoryServices.Protocols;

namespace LdapAuth {
    [DebuggerDisplay("{Uid}")]
    public class LdapUser {

        internal LdapUser(SearchResultEntry entry) {
            foreach (DictionaryEntry attr in entry.Attributes) {
                var name = attr.Key.ToString().ToUpperInvariant();
                var values = (DirectoryAttribute)attr.Value;
                switch (name) {
                    case "UID": if (string.IsNullOrEmpty(this.Uid)) { this.Uid = values[0].ToString(); }; break;
                    case "CN": if (string.IsNullOrEmpty(this.Name)) { this.Name = values[0].ToString(); }; break;
                    case "DISPLAYNAME": if (string.IsNullOrEmpty(this.DisplayName)) { this.DisplayName = values[0].ToString(); }; break;
                    case "GIVENNAME": if (string.IsNullOrEmpty(this.FirstName)) { this.FirstName = values[0].ToString(); }; break;
                    case "SN": if (string.IsNullOrEmpty(this.LastName)) { this.LastName = values[0].ToString(); }; break;
                }
            }
        }


        public string Uid { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

    }
}
