// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace DemoApp;

public interface IFeeCalculator
{
    decimal Calculate(decimal baseAmount, bool isPriority = false);
}

public class FeeCalculator : IFeeCalculator
{
    private const decimal FeePercentage = 0.12M;
    private const decimal MinimumCharge = 0.50M;
    private const decimal PriorityFeePercentage = 0.25M;
    private const decimal PriorityMinimumCharge = 7.50M;

    public decimal Calculate(decimal baseAmount, bool isPriority = false)
    {
        if (isPriority)
        {
            return InternalCalculate(baseAmount, PriorityFeePercentage, PriorityMinimumCharge);
        }
        return InternalCalculate(baseAmount, FeePercentage, MinimumCharge);
    }

    private static decimal InternalCalculate(decimal amount, decimal percentage, decimal minimumFee)
    {
        var calculatedFee = amount * (percentage / 100);
        if (calculatedFee < minimumFee)
        {
            return minimumFee;
        }
        return calculatedFee;
    }
}

public class DatedFeeCalculator : IFeeCalculator
{
    private const decimal DiscountedFeePercentage = 0.07M;
    private const decimal FeePercentage = 0.12M;
    private const decimal MinimumCharge = 0.50M;
    private const decimal PriorityFeePercentage = 0.25M;
    private const decimal PriorityMinimumCharge = 7.50M;

    public decimal Calculate(decimal baseAmount, bool isPriority = false)
    {
        if (isPriority)
        {
            return InternalCalculate(baseAmount, PriorityFeePercentage, PriorityMinimumCharge);
        }

#pragma warning disable RS0030 // Do not used banned APIs
        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
#pragma warning restore RS0030 // Do not used banned APIs
        {
            return InternalCalculate(baseAmount, DiscountedFeePercentage, MinimumCharge);
        }
        return InternalCalculate(baseAmount, FeePercentage, MinimumCharge);
    }

    private static decimal InternalCalculate(decimal amount, decimal percentage, decimal minimumFee)
    {
        var calculatedFee = amount * (percentage / 100);
        if (calculatedFee < minimumFee)
        {
            return minimumFee;
        }
        return calculatedFee;
    }
}

public class SystemClockFeeCalculator : IFeeCalculator
{
    private const decimal DiscountedFeePercentage = 0.07M;
    private const decimal FeePercentage = 0.12M;
    private const decimal MinimumCharge = 0.50M;
    private const decimal PriorityFeePercentage = 0.25M;
    private const decimal PriorityMinimumCharge = 7.50M;

    private readonly ISystemClock systemClock;

    public SystemClockFeeCalculator(ISystemClock systemClock)
    {
        this.systemClock = systemClock;
    }


    public decimal Calculate(decimal baseAmount, bool isPriority = false)
    {
        if (isPriority)
        {
            return InternalCalculate(baseAmount, PriorityFeePercentage, PriorityMinimumCharge);
        }

        if (systemClock.Now.DayOfWeek == DayOfWeek.Monday)
        {
            return InternalCalculate(baseAmount, DiscountedFeePercentage, MinimumCharge);
        }
        return InternalCalculate(baseAmount, FeePercentage, MinimumCharge);
    }

    private static decimal InternalCalculate(decimal amount, decimal percentage, decimal minimumFee)
    {
        var calculatedFee = amount * (percentage / 100);
        if (calculatedFee < minimumFee)
        {
            return minimumFee;
        }
        return calculatedFee;
    }
}
