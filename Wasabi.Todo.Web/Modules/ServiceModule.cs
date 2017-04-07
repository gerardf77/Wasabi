// ***********************************************************************
// Assembly         : AndroidRemote.Web
// Author           : Gerard
// Created          : 06-18-2016
//
// Last Modified By : Gerard
// Last Modified On : 06-18-2016
// ***********************************************************************
// <copyright file="ServiceModule.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Reflection;
using Autofac;

namespace Wasabi.Todo.Web.Modules
{
    /// <summary>
    /// Class ServiceModule.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class ServiceModule : Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Wasabi.Todo.Services"))
                      .Where(t => t.Name.EndsWith("Service"))
                      .AsImplementedInterfaces()
                      .InstancePerRequest();
        }
    }
}