namespace Farfetch.Buildionaire.Data.Gateway
{
    using System;
    using System.Net;

    using Microsoft.TeamFoundation.Client;

    public class TfsConnection
    {
        private readonly string userName;

        private readonly string password;

        private readonly string domain;

        private readonly bool useLocalAccount;

        private readonly Uri uri;

        public TfsConnection(Uri uri, string userName, string password, string domain)
        {
            this.uri = uri;
            if (string.IsNullOrEmpty(userName))
            {
                this.useLocalAccount = true;
            }
            else
            {
                this.domain = domain;
                this.password = password;
                this.userName = userName;
                this.useLocalAccount = false;
            }
        }

        public TfsTeamProjectCollection Connect()
        {
            if (this.uri == null)
            {
                return null;
            }

            TfsTeamProjectCollection projectCollection;

            if (!this.useLocalAccount)
            {
                var cred = new NetworkCredential(this.userName, this.password, this.domain);

                projectCollection = new TfsTeamProjectCollection(this.uri, cred);
            }
            else
            {
                projectCollection = new TfsTeamProjectCollection(this.uri);
            }

            projectCollection.Authenticate();

            return projectCollection;
        }
    }
}
