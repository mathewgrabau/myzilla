using System;
using System.Collections.Generic;
using System.Text;



namespace MyZilla.BL.Interfaces
{
    public class BLControllerFactory
    {
        /// <summary>
        /// Get the concrete factory registered in app.config.
        /// </summary>
        /// <param name="UserID">Identifies the connection and user credentials</param> 
        /// <returns></returns>
        public static object GetRegisteredConcreteFactory(object UserID)
        {
#if DEBUG
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
#endif

            string typeName = "MyZilla.BL.BugzillaHttp.HttpEngine, MyZilla.BL.BugzillaHttp";

#if DEBUG
            watch.Stop();
            Console.WriteLine("Factory ellapsed: " + watch.ElapsedMilliseconds);
#endif

            return GetConcreteFactory(typeName, UserID);



        }

        /// <summary>
        /// Get a concrete factory based on the type name.
        /// </summary>
        /// <param name="typeName">The name of the type to retrieve. Such as: Namespace.Type, Namespace</param>
        /// <returns></returns>
        private static object GetConcreteFactory(string typeName, object UserID)
        {
            Type type = Type.GetType(typeName);
            return Activator.CreateInstance(type, new object[] {UserID });
        }
    }
}
