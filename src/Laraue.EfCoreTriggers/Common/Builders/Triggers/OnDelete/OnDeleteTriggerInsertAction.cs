﻿using Laraue.EfCoreTriggers.Common.Builders.Triggers.Base;
using Laraue.EfCoreTriggers.Common.Builders.Visitor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Laraue.EfCoreTriggers.Common.Builders.Triggers.OnDelete
{
    public class OnDeleteTriggerInsertAction<TTriggerEntity, TInsertEntity> : TriggerInsertAction<TTriggerEntity, TInsertEntity>
        where TTriggerEntity : class
        where TInsertEntity : class
    {
        public OnDeleteTriggerInsertAction(Expression<Func<TTriggerEntity, TInsertEntity>> setValues)
            : base(setValues)
        {
        }

        public override Dictionary<string, ArgumentPrefix> InsertExpressionPrefixes => new Dictionary<string, ArgumentPrefix>
        {
            [InsertExpression.Parameters[0].Name] = ArgumentPrefix.Old,
        };
    }
}