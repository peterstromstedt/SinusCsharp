# SinusCsharp

Reworking a php assignment with C#.

Added all of our connectionstrings to the project so that you can see how you can add yours if you want to fiddle with the project.
They can be found in the appsettings.json

We added the IHttpContextAccessor, to be able to use the Cookies for storing our "cart object".
information we found to implement this, 
https://stackoverflow.com/questions/38184583/how-to-add-ihttpcontextaccessor-in-the-startup-class-in-the-di-in-asp-net-core-1

used this guide here, How to Turn a C# Object Into a JSON String in .NET? .
https://code-maze.com/csharp-object-into-json-string-dotnet/

Initializing the Cart cookie, as soon as someone enters the webpage. in the Homecontroller, Index method, so its ready to recieve whatever information we want it to recieve. Then adding a serialize and deserilize method in CartsController so we can add and remove from the cart.
