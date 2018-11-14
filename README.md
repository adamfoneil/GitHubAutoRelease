For a long time, I've wanted a simple way always to know what code for a web app is in production. For code hosted in GitHub, Releases are the obvious way to do that. The issue for me however has always been how to automate this. There are various mature release automation solutions out there that do this I'm sure, but they all require substantial mental bandwidth to implement, not to mention cost (especially for private repositories). I've always needed a solution that complements my existing workflow rather than forcing me to implement a whole new tool or process. Therefore, this library is my attempt at a solution for creating Releases automatically during a web app publish or deployment in a very lightweight manner.

How to use:

1. Install Nuget package **GitHub.AutoRelease**.

2. In the startup code of your web app, create an [AutoRelease](https://github.com/adamosoftware/GitHubAutoRelease/blob/master/GitHubAutoRelease/AutoRelease.cs) object and call its `CreateOnStartupAsync` method. Note this is currently in beta.

The `AutoRelease` constructor requires a few arguments:

- a GitHub API personal access token. To generate a token, go [here](https://github.com/settings/tokens). This token should be in your Web.config or settings file.

- a UserAgent string that identifies your application to GitHub. I'm not entirely clear what this, but the info about this is [here](https://developer.github.com/v3/#user-agent-required). See the related [Octokit documentation here](https://octokitnet.readthedocs.io/en/latest/getting-started/).

When calling [CreateOnStartupAsync](https://github.com/adamosoftware/GitHubAutoRelease/blob/master/GitHubAutoRelease/AutoRelease.cs#L33) you pass 

- a `bool` indicating whether your app is in production or not. This is determined in various ways. I didn't think it was right to make any assumptions here.

- optionally, a tag name. The typical use case I imagined was to leave the tag name null so that AutoRelease can [generate a tag number](https://github.com/adamosoftware/GitHubAutoRelease/blob/master/GitHubAutoRelease/AutoRelease.cs#L49) for the current date.
