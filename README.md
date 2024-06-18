# plasmaclouds cheap and basic smoke test

Greetings, fellow cloud surfer!

I have used ChatGPT 4o and its Code Copilot to come up with an iteration of very cheap smoke test that can be deployed on a free-tier ASP.NET8 Azure Web-App, to test how just visiting a simple web page behaves in App Insights, produces some Log Analytics Workspace so you can try out KQL Queries and analyze their outputs.

Variables to modify for ever better SMOKE:
 - Number of threads
 - Number of iterations

Some big things are awaiting for version 2.0, like accessing a database and hammering that for a bit so some database insights can also be seen and analyzed.

If I have reinvented a wheel, good for me because it's been a long time since I touched any programming at all (if you can call scripting on-premises Powershell programming, HA!).
