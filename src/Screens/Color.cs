using System;

namespace ModernMUD
{
    /// <summary>
    /// Color: Supports using the &amp;+ color codes to set ANSI color.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Resets Color (&amp;n)
        /// </summary>
        public static string MOD_CLEAR = "[0m";
        /// <summary>
        /// Bolds Color (bright colors)
        /// </summary>
        public static string MOD_BOLD = "[1m";
        /// <summary>
        /// Fades color (not used)
        /// </summary>
        public static string MOD_FAINT = "[2m";
        /// <summary>
        /// Underlines text (not used)
        /// </summary>
        public static string MOD_UNDERLINE = "[4m";
        /// <summary>
        /// Blinks text (not used)
        /// </summary>
        public static string MOD_BLINK = "[5m";
        /// <summary>
        /// Reverses text (not used)
        /// </summary>
        public static string MOD_REVERSE = "[7m";
        // Foreground Colors
        /// <summary>
        /// Black
        /// </summary>
        public static string FG_BLACK = "[0;30m";  /* (&+l) */
        /// <summary>
        /// Red
        /// </summary>
        public static string FG_RED = "[0;31m";  /* (&+r) */
        /// <summary>
        /// Green
        /// </summary>
        public static string FG_GREEN = "[0;32m";  /* (&+g) */
        /// <summary>
        /// Dark Yellow (orange)
        /// </summary>
        public static string FG_YELLOW = "[0;33m";  /* (&+y) */
        /// <summary>
        /// Blue
        /// </summary>
        public static string FG_BLUE = "[0;34m";  /* (&+b) */
        /// <summary>
        /// Dark Magenta (purple)
        /// </summary>
        public static string FG_MAGENTA = "[0;35m";  /* (&+m) */
        /// <summary>
        /// Cyan
        /// </summary>
        public static string FG_CYAN = "[0;36m";  /* (&+c) */
        /// <summary>
        /// Dark White (light grey)
        /// </summary>
        public static string FG_WHITE = "[0;37m";  /* (&+w) */
        // Bold Foreground Colors
        /// <summary>
        /// Bright black (dark grey)
        /// </summary>
        public static string FG_B_BLACK = "[1;30m"; /* (&+L) */
        /// <summary>
        /// Bright red (light red)
        /// </summary>
        public static string FG_B_RED = "[1;31m"; /* (&+R) */
        /// <summary>
        /// Bright green
        /// </summary>
        public static string FG_B_GREEN = "[1;32m"; /* (&+G) */
        /// <summary>
        /// Bright yellow (regular yellow)
        /// </summary>
        public static string FG_B_YELLOW = "[1;33m"; /* (&+Y) */
        /// <summary>
        /// Bright blue (light blue)
        /// </summary>
        public static string FG_B_BLUE = "[1;34m"; /* (&+B) */
        /// <summary>
        /// Bright magenta (pink)
        /// </summary>
        public static string FG_B_MAGENTA = "[1;35m"; /* (&+M) */
        /// <summary>
        /// Bright cyan
        /// </summary>
        public static string FG_B_CYAN = "[1;36m"; /* (&+C) */
        /// <summary>
        /// Bright white
        /// </summary>
        public static string FG_B_WHITE = "[1;37m"; /* (&+W) */
        // Background Colors
        /// <summary>
        /// Black (normal background)
        /// </summary>
        public static string BG_BLACK = "[40m";
        /// <summary>
        /// Red
        /// </summary>
        public static string BG_RED = "[41m";
        /// <summary>
        /// Green
        /// </summary>
        public static string BG_GREEN = "[42m";
        /// <summary>
        /// Yellow
        /// </summary>
        public static string BG_YELLOW = "[43m";
        /// <summary>
        /// Blue 
        /// </summary>
        public static string BG_BLUE = "[44m";
        /// <summary>
        /// Magenta
        /// </summary>
        public static string BG_MAGENTA = "[45m";
        /// <summary>
        /// Cyan
        /// </summary>
        public static string BG_CYAN = "[46m";
        /// <summary>
        /// White
        /// </summary>
        public static string BG_WHITE = "[47m";


        /// <summary>
        /// Uses &amp;+ color codes - Xangis
        /// </summary>
        /// <param name="buffer">The text to strip color codes from</param>
        public static string RemoveColorCodes(string buffer)
        {
            if (String.IsNullOrEmpty(buffer))
                return String.Empty;

            String newstr = String.Empty;
            bool ampersand = false;
            int point;

            // Convert to black and white by stripping out all color control codes.
            for (point = 0; point < buffer.Length; point++)
            {
                if (ampersand == false && buffer[point] == '&')
                {
                    ampersand = true;
                    continue;
                }
                if (ampersand && buffer[point] == '&')
                {
                    // Double ampersand.  That means show a single ampersand.
                    ampersand = false;
                }
                else if (ampersand)
                {
                    // Ampersand is true, that means gobble this character.  If it's a plus, then
                    // gobble the next one too.
                    if (buffer[point] != '+')
                    {
                        ampersand = false;
                    }
                    continue;
                }
                newstr += buffer[point];
            }
            return newstr;
        }
    }
}