﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using Laraue.EfCoreTriggers.Common.Services;
using Laraue.EfCoreTriggers.Common.Services.Impl;
using Laraue.EfCoreTriggers.Common.TriggerBuilders;

namespace Laraue.EfCoreTriggers.SqlServer;

public class SqlServerSqlGenerator : SqlGenerator
{
    public SqlServerSqlGenerator(IDbSchemaRetriever adapter, SqlTypeMappings sqlTypeMappings) 
        : base(adapter, sqlTypeMappings)
    {
        
    }

    public override string NewEntityPrefix => "Inserted";

    public override string OldEntityPrefix => "Deleted";

    public override string GetOperand(Expression expression)
    {
        return expression.NodeType switch
        {
            ExpressionType.IsTrue => $"= {GetSql(true)}",
            ExpressionType.IsFalse => $"= {GetSql(false)}",
            ExpressionType.Not => $"= {GetSql(false)}",
            _ => base.GetOperand(expression)
        };
    }

    public override string GetSql(bool source)
    {
        return source ? "1" : "0";
    }

    public override string GetVariableSql(Type type, MemberInfo member, ArgumentType argumentType)
    {
        return argumentType switch
        {
            ArgumentType.New => $"@New{member.Name}",
            ArgumentType.Old => $"@Old{member.Name}",
            _ => throw new InvalidOperationException(
                $"Invalid attempt to generate declaring variable SQL using argument prefix {argumentType}")
        };
    }
}