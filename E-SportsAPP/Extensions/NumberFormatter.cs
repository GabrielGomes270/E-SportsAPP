namespace E_SportsAPP.Extensions
{
    public static class NumberFormatter
    {

        public static string ToReadableFormat(this int number)
        {
            if (number >= 1_000_000)
                return (number / 1_000_000D).ToString("0.#") + "M";
            if (number >= 1_000)
                return (number / 1_000D).ToString("0.#") + "K";
            return number.ToString();
        }
        public static string ToReadableFormat(this long number)
        {
            if (number >= 1_000_000)
                return (number / 1_000_000D).ToString("0.#") + "M";
            if (number >= 1_000)
                return (number / 1_000D).ToString("0.#") + "K";
            return number.ToString();
        }

        public static string ToReadableFormat(this decimal number)
        {
            if (number >= 1_000_000)
                return (number / 1_000_000M).ToString("0.#") + "M";
            if (number >= 1_000)
                return (number / 1_000M).ToString("0.#") + "K";
            return number.ToString("0.##");
        }
    }
}
