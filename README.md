# SinusCsharp

Reworking a php assignment with C#.

We added the IHttpContextAccessor, to be able to use the Cookies for storing our "cart object".
information we found to implement this, 
https://stackoverflow.com/questions/38184583/how-to-add-ihttpcontextaccessor-in-the-startup-class-in-the-di-in-asp-net-core-1

used this guide here, How to Turn a C# Object Into a JSON String in .NET? .
https://code-maze.com/csharp-object-into-json-string-dotnet/

Initializing the Cart cookie, as soon as someone enters the webpage. in the Homecontroller, Index method, so its ready to recieve whatever information we want it to recieve. Then adding a serialize and deserilize method in CartsController so we can add and remove from the cart.

We found a solution, or we, rather one person in our group spent sometime and worked our problem with the connections and that we had to reset them everytime but now its working as intended.

so far we've implemented almost everything we wanted, still have some refactoring and cleaning the code. styling etc.

we didn't put anytime into the authorization and logins, we used the auto complete on that. The only thing we've implemented into the project that needs to be logged in is a create button that is only showing if we are logged in. 

have to look more into how to set roles etc and giving them different levels of access...
