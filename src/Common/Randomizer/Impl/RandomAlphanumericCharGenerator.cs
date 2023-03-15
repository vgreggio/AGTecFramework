using System;
using AGTec.Common.Randomizer.ReferenceTypes;

namespace AGTec.Common.Randomizer.Impl;

public class RandomAlphanumericCharGenerator : RandomGenericGeneratorBase<char>, IRandomCharacter
{
    public RandomAlphanumericCharGenerator()
    {
            
    }
    public RandomAlphanumericCharGenerator(int seed)
        :base(seed)
    {
    }

    public char GenerateValue()
    {
        return GetRandomValue();
    }

    public char GenerateValue(char min, char max)
    {
        if (IsConditionToReachLimit())
        {
            return max;
        }

        int firstIndex = Constants.AlphanumericCharacters.IndexOf(min);
        int lastIndex = Constants.AlphanumericCharacters.IndexOf(max);

        if (firstIndex >= lastIndex)
        {
            throw new ArgumentException(Constants.MinMaxValueExceptionMsg);
        }

        int randomIndex = randomizer.Next(firstIndex, lastIndex);
        return Constants.AlphanumericCharArray[randomIndex];
    }

    protected override char GetRandomValue()
    {
        int indexofCharacter = randomizer.Next(0, Constants.AlphanumericCharacters.Length - 1);
        return Constants.AlphanumericCharArray[indexofCharacter];
    }
}