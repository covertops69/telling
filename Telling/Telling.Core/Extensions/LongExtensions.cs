using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Extensions
{
    public static class LongExtensions
    {
        public enum DataSizes
        {
            B,
            KB,
            MB,
            GB,
            TB
        }

        public static string ToPrettyByteSize(this long fileSize, int decimalPlaces = 0, DataSizes startingSize = DataSizes.B, DataSizes maxEndingSize = DataSizes.TB)
        {
            var unitOfMeasure = startingSize;
            while (fileSize >= 1024 && unitOfMeasure < maxEndingSize)
            {
                fileSize /= 1024;
                unitOfMeasure++;
            }

            var dataSize = Math.Round((double)fileSize, decimalPlaces, MidpointRounding.AwayFromZero);

            return $"{dataSize} {unitOfMeasure}";
        }
    }
}
