using EmailParser.API;
using System;
using Terrasoft.Core;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Entities.Events;
using Terrasoft.Core.Factories;

namespace EmailParser.Files.cs.EntityEventListener
{
    /// <summary>
    /// Listener for Case entity events.
    /// </summary>
    /// <seealso cref="Terrasoft.Core.Entities.Events.BaseEntityEventListener" />
    [EntityEventListener(SchemaName = "Case")]
    class CaseEventListener : BaseEntityEventListener
    {
        public override void OnInserting(object sender, EntityBeforeEventArgs e)
        {
            base.OnInserting(sender, e);
            Entity entity = (Entity)sender;

            Guid emailId = entity.GetTypedColumnValue<Guid>("ParentActivityId");
            if (emailId == Guid.Empty) return;
                        
            ConstructorArgument ucArg = new ConstructorArgument("userConnection", entity.UserConnection);
            IParser parser = ClassFactory.Get<IParser>("Academy", ucArg);

            if (parser.TryGetGroup(emailId, out Guid SysAdminUnitId))
            {
                entity.SetColumnValue("GroupId", SysAdminUnitId);
            }
        }
    }
}
