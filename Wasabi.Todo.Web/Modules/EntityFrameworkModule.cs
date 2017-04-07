// ***********************************************************************
// Assembly         : AndroidRemote.Web
// Author           : Gerard
// Created          : 06-18-2016
//
// Last Modified By : Gerard
// Last Modified On : 06-18-2016
// ***********************************************************************
// <copyright file="EntityFrameworkModule.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Data.Entity;
using Autofac;
using Wasabi.Todo.Data;

namespace Wasabi.Todo.Web.Modules
{
    /// <summary>
    /// Class EntityFrameworkModule.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class EntityFrameworkModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(TodoDbContext)).As(typeof(DbContext)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.RegisterType(typeof(TaskRepository)).As(typeof (ITaskRepository)).InstancePerRequest();
        }

    }
}