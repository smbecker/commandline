﻿// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CommandLine.Core;
using CommandLine.Infrastructure;
using CommandLine.Tests.Fakes;
using Xunit;

namespace CommandLine.Tests.Unit.Core
{
    public class OptionMapperTests
    {
        [Fact]
        public void Map_boolean_switch_creates_boolean_value()
        {
            // Fixture setup
            var tokenPartitions = new[]
                {
                    new KeyValuePair<string, IEnumerable<string>>("x", new [] { "true" })
                };
            var specProps = new[]
                {
                    SpecificationProperty.Create(
                        new OptionSpecification("x", string.Empty, false, string.Empty, Maybe.Nothing<int>(), Maybe.Nothing<int>(), '\0', Maybe.Nothing<object>(), typeof(bool), TargetType.Switch, string.Empty, string.Empty, new List<string>()), 
                        typeof(FakeOptions).GetProperties().Single(p => p.Name.Equals("BoolValue", StringComparison.Ordinal)),
                        Maybe.Nothing<object>())
                };

            // Exercize system 
            var result = OptionMapper.MapValues(
                specProps.Where(pt => pt.Specification.IsOption()),
                tokenPartitions,
                (vals, type, isScalar) => TypeConverter.ChangeType(vals, type, isScalar, CultureInfo.InvariantCulture),
                StringComparer.InvariantCulture);

            // Verify outcome
            Assert.NotNull(result.Value.Single(
                a => a.Specification.IsOption()
                && ((OptionSpecification)a.Specification).ShortName.Equals("x")
                && (bool)((Just<object>)a.Value).Value));

            // Teardown
        }
    }
}
