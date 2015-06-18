# Buildionaire
## OverView
Buildionaire is a Gamification Platform where code quality matters. 
Buildionaire monitors build servers, collects application builds metrics ands scores each Build.

Let the games begin.

## Getting Started
You need to setup the Buildionaire Database and TFS connections. This can be done on the web.config file. For the database edit the connection string. To configure the TFS connection edit the following keys of appSettings:

```

    <add key="SourceControlEndpoint" value="http://MYTFSENDPOINT" />

    <add key="SourceControlUserName" value="" />

    <add key="SourceControlUserPassword" value="" />

    <add key="SourceControlDomain" value="MY SOURCECONTROL DOMAIN" />

```

## Credits

Buildionaire is built using the following great open source projects:

- [ASP.NET Web API](https://aspnetwebstack.codeplex.com/)
- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [Json.Net](http://james.newtonking.com/json)
- [SharpRepository](https://github.com/SharpRepository/SharpRepository)
- [Katana](https://katanaproject.codeplex.com/)

## Development
1. Fork it
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request!
