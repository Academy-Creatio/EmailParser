using EmailParser.API;
using EmailParser.Model;
using System;
using System.Collections.Generic;
using Terrasoft.Core;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Factories;

namespace EmailParser.Files.cs
{
    /// <inheritdoc cref="IParser"/>
    [DefaultBinding(typeof(IParser), Name = "Academy")]
    public class Parser : IParser
    {
        private readonly UserConnection _userConnection;
  
        public Parser(UserConnection userConnection)
        {
            _userConnection = userConnection;
        }

        #region IParser
        public bool TryGetGroup(Guid activityId, out Guid SysAdminUnitId)
        {
            SysAdminUnitId = Guid.Empty;

            IEmailModel email = GetEmailById(activityId);
            List<Group> groups = GetCategories();

            for (int i = 0; i < groups.Count; i++)
            {
                for (int j = 0; j < groups[i].Words.Length; j++)
                {
                    string word = groups[i].Words[j];
                    if (email.Preview.Contains(word))
                    {
                        SysAdminUnitId =  groups[i].GroupId;
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion


        /// <summary>
        /// Gets email properties by ActivityId
        /// </summary>
        /// <param name="recordId">Id of an activity that is an email</param>
        /// <returns></returns>
        private IEmailModel GetEmailById(Guid recordId)
        {
            const string schemaName = "Activity";
            EntitySchema schema = _userConnection.EntitySchemaManager.GetInstanceByName(schemaName);
            Entity entity = schema.CreateEntity(_userConnection);

            string[] columnsToFetch = new string[] { "IsHtmlBody", "Title", "Body", "Preview" };

            if (entity.FetchFromDB(schema.PrimaryColumn.Name, recordId, columnsToFetch, false))
            {
                return new EmailModel
                {
                    IsHtmlBody = entity.GetTypedColumnValue<bool>("IsHtmlBody"),
                    Title = entity.GetTypedColumnValue<string>("Title"),
                    Body = entity.GetTypedColumnValue<string>("Body"),
                    Preview = entity.GetTypedColumnValue<string>("Preview")
                };
            }
            return default(EmailModel);
        }

        /// <summary>
        /// Gets list of all magic strings by category from WordToGroupMapper table
        /// </summary>
        /// <returns>List</returns>
        private List<Group> GetCategories()
        {
            const string schemaName = "WordToGroupMapper";
            
            EntitySchemaQuery esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, schemaName);
            esq.AddColumn("Name");
            esq.AddColumn("Description");
            esq.AddColumn("Group");
            EntityCollection entities = esq.GetEntityCollection(_userConnection);

            List<Group> groups = new List<Group>();

            for (int i = 0; i < entities.Count; i++)
            {
                Guid groupId = entities[i].GetTypedColumnValue<Guid>("GroupId");
                var group = new Group()
                {
                    Words = entities[i].GetTypedColumnValue<string>("Description").Split(','),
                    GroupId = entities[i].GetTypedColumnValue<Guid>("GroupId")
                };
                groups.Add(group);
            }
            return groups;
        }
    }
    

}
