using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PhoneBookApp.Core.Domain;
using System.Linq.Expressions;

namespace PhoneBookApp.Core.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyGlobalFilters(this ModelBuilder modelBuilder)
    {
        IEnumerable<IMutableEntityType> softDeleteEntites = modelBuilder
            .Model
            .GetEntityTypes()
            .Where(type => typeof(ISoftDeletable).IsAssignableFrom(type.ClrType) &&
                           !type.IsAbstract());

        foreach (IMutableEntityType softDeleteEntity in softDeleteEntites)
        {
            modelBuilder.Entity(softDeleteEntity.ClrType).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity.ClrType));
        }
    }

    private static LambdaExpression? GenerateQueryFilterLambda(Type type)
    {
        ParameterExpression parameter = Expression.Parameter(type, "w");
        ConstantExpression nullConstantValue = Expression.Constant(null);
        MemberExpression propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDeletable.DeletedOn));
        BinaryExpression equalExpression = Expression.Equal(propertyAccess, nullConstantValue);
        LambdaExpression lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }
}
