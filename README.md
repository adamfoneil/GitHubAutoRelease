For a long time, I've wanted a simple way always to know what code for a web app is in production. For code hosted in GitHub, Releases are the obvious way to do that. The issue for me however has always been how to automate this. There are various mature release automation solutions out there that do this I'm sure, but they all require substantial mental bandwidth to implement, not to mention cost (especially for private repositories). I've always needed a solution that complements my existing workflow rather than forces me to implement a whole new tool or learn a whole new process. This library is my attempt at a solution for creating Releases automatically during a web app publish or deployment in a very lightweight manner.

How to use:
1. Install Nuget package **GitHub.AutoRelease**
