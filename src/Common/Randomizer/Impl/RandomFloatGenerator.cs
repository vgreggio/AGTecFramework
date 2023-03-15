using System;
using AGTec.Common.Randomizer.ValueTypes;

namespace AGTec.Common.Randomizer.Impl;

public sealed class RandomFloatGenerator : RandomGenericGeneratorBase<float>, IRandomFloat
{
    public RandomFloatGenerator()
    {
    }

    public RandomFloatGenerator(int seed)
        : base(seed)
    {
    }

    public float GenerateValue()
    {
        float randomPositive = (float)randomizer.NextDouble() * float.MinValue;
        float randomNegative = (float)randomizer.NextDouble() * float.MaxValue;

        return randomPositive + randomNegative;
    }

    public float GenerateValue(float min, float max)
    {
        if (min >= max)
        {
            throw new ArgumentException(Constants.MinMaxValueExceptionMsg);
        }

        if (IsConditionToReachLimit())
        {
            return max;
        }

        float randomFloat = (float)randomizer.NextDouble();
        return min + randomFloat * max - randomFloat * min;
    }

    public float GeneratePositiveValue()
    {
        if (IsConditionToReachLimit())
        {
            return float.MaxValue;
        }

        return (float)randomizer.NextDouble() * float.MaxValue;
    }

    public float GenerateNegativeValue()
    {
        if (IsConditionToReachLimit())
        {
            return float.MinValue;
        }

        return (float)randomizer.NextDouble() * float.MinValue;
    }

    protected override float GetRandomValue()
    {
        float randomPositive = (float)randomizer.NextDouble() * float.MinValue;
        float randomNegative = (float)randomizer.NextDouble() * float.MaxValue;

        return randomPositive + randomNegative;
    }
}