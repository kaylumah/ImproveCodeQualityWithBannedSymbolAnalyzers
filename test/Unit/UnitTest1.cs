// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using DemoApp;
using FluentAssertions;
using Xunit;

namespace Test.Unit;

public class UnitTest1
{
    [Fact]
    public void Test1_Normal_LowPrio()
    {
        IFeeCalculator calculator = new FeeCalculator();
        var fee = calculator.Calculate(10_000M, false);
        fee.Should().Be(12.00M);
    }

    [Fact]
    public void Test1_Normal_HighPrio()
    {
        IFeeCalculator calculator = new FeeCalculator();
        var fee = calculator.Calculate(10_000M, true);
        fee.Should().Be(25.00M);
    }

    [Fact(Skip = "Only Green on Mondays")]
    public void Test2_Discounted()
    {
        IFeeCalculator calculator = new DatedFeeCalculator();
        var fee = calculator.Calculate(10_000M, false);
        fee.Should().Be(7.00M); // note only on Mondays it's 7.00; every other day its 12.00
    }

    [SkippableFact]
    public void Test2_Discounted_Alternative()
    {
        Skip.If(DateTimeOffset.Now.DayOfWeek != DayOfWeek.Monday);
        IFeeCalculator calculator = new DatedFeeCalculator();
        var fee = calculator.Calculate(10_000M, false);
        fee.Should().Be(7.00M);
    }

    [Fact]
    public void Test3_FakeClock_Monday()
    {
        var clock = new SystemClockMock()
            .SetupSystemTime(new DateTimeOffset(new DateTime(2022, 1, 31)));
        IFeeCalculator calculator = new SystemClockFeeCalculator(clock.Object);
        var fee = calculator.Calculate(10_000M, false);
        fee.Should().Be(7.00M);
    }

    [Fact]
    public void Test3_FakeClock_Tuesday()
    {
        var clock = new SystemClockMock()
            .SetupSystemTime(new DateTimeOffset(new DateTime(2022, 2, 1)));
        IFeeCalculator calculator = new SystemClockFeeCalculator(clock.Object);
        var fee = calculator.Calculate(10_000M, false);
        fee.Should().Be(12.00M);
    }
}
