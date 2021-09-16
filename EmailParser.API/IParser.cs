using System;
using System.Collections.Generic;
using System.Text;

namespace EmailParser.API
{
    
    /// <summary>
    /// An email parser abstraction
    /// <example>
    /// Parser Instantiation <b>REQUIRES</b> constructor
    /// <code>
    /// ConstructorArgument ucArg = new ConstructorArgument("userConnection", UserConnection);
    /// Terrasoft.Core.Factories.ClassFactory.Get&lt;IParser&gt;("Academy", ucArg);
    /// </code>
    /// </example>
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Gets SysAdminUnitId given an email.
        /// <example>
        /// Usage
        /// <code>
        /// if(parser.TryGetGroup(id, out Guid SysAdminUnitId))
        /// {
        ///     ... your code here
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="activityId">Id of an activity that is email</param>
        /// <param name="SysAdminUnitId">Id of a org-group</param>
        /// <returns>True if SysAdminUnit is found, otherwise false</returns>

        bool TryGetGroup(Guid activityId, out Guid SysAdminUnitId);
    }
}
