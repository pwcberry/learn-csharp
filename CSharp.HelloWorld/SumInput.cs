using System.Globalization;

namespace CSharp.HelloWorld
{
    internal static class SumInput
    {
        public static string Sum(string[] args, string multiplier = "1", string precision = "0")
        {
            if (args.Any(x => x.Contains('.')) || multiplier.Contains('.'))
            {
                var m = float.Parse(multiplier);
                var sum = args.ToList().Select(float.Parse).Sum() * m;
                return sum.ToString($"F{precision}", CultureInfo.InvariantCulture);
            }
            else
            {
                var m = int.Parse(multiplier);
                var sum = args.ToList().Select(int.Parse).Sum() * m;
                return sum.ToString();
            }
        }
    }
}
