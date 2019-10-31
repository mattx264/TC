using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TC.Entity;

namespace TC.DataAccess.DatabaseContext
{
    public partial class TestingCenterDbContext : DbContext
    {
        public virtual int SaveChanges(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new NullReferenceException("UnitOfWork cannot be null");
            }

            var addedEntities =
                ChangeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Added).Select(p => p.Entity);
            var modifiedEntities =
                ChangeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Modified).Select(p => p.Entity);
            var deletedEntities =
                ChangeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Deleted).ToList();

            var dateModified = DateTime.Now;

            // string currentPrincipalName = string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name)
            //    ? "Unknown"
            //   : Thread.CurrentPrincipal.Identity.Name;
            string currentPrincipalName = "not set";
            foreach (var entity in addedEntities)
            {
                entity.CreatedBy = currentPrincipalName;
                entity.ModifiedBy = currentPrincipalName;
                entity.DateAdded = dateModified;
                entity.DateModified = dateModified;
                entity.IsActive = true;
            }

            foreach (var entity in modifiedEntities)
            {
                entity.ModifiedBy = currentPrincipalName;
                entity.DateModified = dateModified;
            }

            foreach (var deletedEntity in deletedEntities)
            {
                var entity = deletedEntity.Entity;
                entity.ModifiedBy = currentPrincipalName;
                entity.DateModified = dateModified;
            }

            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    foreach (var entry in deletedEntities)
                    {
                        // SoftDelete(entry, currentPrincipalName, dateModified);
                    }

                    var saveResult = base.SaveChanges();
                    transaction.Commit();

                    return saveResult;
                }

                // catch (ValidationException ex)
                // {
                //     // Retrieve the error messages as a list of strings.
                //     var errorMessages =
                //         ex..SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                //     var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ",
                //         string.Join("; ", errorMessages));

                //     transaction.Rollback();
                //     throw new ValidationException(exceptionMessage, ex);
                // }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        //        private void SoftDelete(EntityEntry entry, string currentPrincipalName, DateTime dateModified)
        //     {
        //         Type entryEntityType = entry.Entity.GetType();

        //         string tableName = GetTableName(entryEntityType);
        //         string primaryKeyName = GetPrimaryKeyName(entryEntityType);

        //         string deletequery = string.Format("UPDATE {0} SET [Active] = 0, [DateModified] = @dateModified, [ModifiedBy] = @modifiedBy WHERE [{1}] = @id", tableName, primaryKeyName);

        //         Database.ExecuteSqlCommand(
        //             deletequery,
        //             new SqlParameter("@id", entry.OriginalValues[primaryKeyName]),
        //             new SqlParameter("@dateModified", dateModified),
        //             new SqlParameter("@modifiedBy", currentPrincipalName));

        //         entry.State = EntityState.Detached;
        //     }

        //     private string GetTableName(Type type)
        //     {
        //         EntitySetBase es = GetEntitySet(type);

        //         return string.Format("[{0}].[{1}]",
        //             es.MetadataProperties["Schema"].Value,
        //             es.MetadataProperties["Table"].Value);
        //     }

        //     private string GetPrimaryKeyName(Type type)
        //     {
        //         EntitySetBase es = GetEntitySet(type);

        //         return es.ElementType.KeyMembers[0].Name;
        //     }

        //     private EntitySetBase GetEntitySet(Type type, [CallerMemberName] string callerName="")
        //     {
        //         if (!_mappingCache.ContainsKey(type))
        //         {
        //             ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;

        //             string typeName = ObjectContext.GetObjectType(type).Name;

        //             var es = octx.MetadataWorkspace
        //                             .GetItemCollection(DataSpace.SSpace)
        //                             .GetItems<EntityContainer>()
        //                             .SelectMany(c => c.BaseEntitySets
        //                                             .Where(e => e.Name == typeName))
        //                             .FirstOrDefault();

        //             if (es == null)
        //                 throw new ArgumentException(String.Format("Entity type not found in {0}", callerName), typeName);

        //             _mappingCache.Add(type, es);
        //         }

        //         return _mappingCache[type];
        //     }
    }

}

