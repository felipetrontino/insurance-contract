using FluentAssertions.Collections;
using FluentAssertions.Equivalency;
using FluentAssertions.Primitives;
using Insurance.Core.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;

namespace Insurance.Test.Common
{
    public static class ObjectAssertionsExtensions
    {
        public static void BeEquivalentToModel<T>(this ObjectAssertions assertions, T expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
            where T : class
        {
            assertions.BeEquivalentTo(expectation, x =>
            {
                x.Excluding(y => y.SelectedMemberInfo.Name.StartsWith(nameof(IEntity.Id)));

                config?.Invoke(x);

                return x;
            });
        }

        public static void BeEquivalentToModel<T>(this GenericCollectionAssertions<T> assertions, IEnumerable<T> expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
            where T : class
        {
            assertions.BeEquivalentTo(expectation, x =>
            {
                x.Excluding(y => y.SelectedMemberInfo.Name.StartsWith(nameof(IEntity.Id)));

                config?.Invoke(x);

                return x;
            });
        }
    }
}