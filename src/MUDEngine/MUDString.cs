using System;
using System.Text;

namespace MUDEngine
{
    /// <summary>
    /// String utility functions.
    /// </summary>
    public class MUDString
    {
        /// <summary>
        /// Gets lowercase initial.  Otherwise we could just do str[0].
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char LowercaseInitial( string str )
        {
            return Char.ToLower( str[ 0 ] );
        }

        /// <summary>
        /// This is meant for long-form edit mode - description editing and the like.
        /// 
        /// TODO: Make this work.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="pString"></param>
        public static void StringAppend( CharData ch, ref string pString )
        {
            ch.SendText( "Begin entering your text now (.h = help .s = show .c = clear @ = save)\r\n" );
            ch.SendText( "-----------------------------------------------------------------------\r\n" );
            ch.SendText( "* * String Editing Is Temporarily Disabled * *\r\n" );
            return;
        }

        /// <summary>
        /// Old-style clunky long-form text editing.
        /// 
        /// TODO: Make this work better.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        public static void StringAdd( CharData ch, string argument )
        {
            string text = String.Empty;
            int buflen = ch._socket._stringEditing.Length;
            int arglen = argument.Length;

            if( argument[ 0 ] == '.' )
            {
                string arg1 = String.Empty;
                string arg2 = String.Empty;
                string arg3 = String.Empty;

                argument = OneArgument( argument, ref arg1 );
                argument = OneArgument( argument, ref arg2 );
                argument = OneArgument( argument, ref arg3 );

                if( !StringsNotEqual( arg1, ".c" ) )
                {
                    ch.SendText( "String cleared.\r\n" );
                    ch._socket._stringEditing = String.Empty;
                    return;
                }

                if( !StringsNotEqual( arg1, ".s" ) )
                {
                    ch.SendText( "String so far:\r\n" );
                    ch.SendText( ch._socket._stringEditing );
                    ch.SendText( String.Empty );
                    return;
                }

                if( !StringsNotEqual( arg1, ".r" ) )
                {
                    if( String.IsNullOrEmpty(arg2) )
                    {
                        ch.SendText( "Usage: .r \"old string\" \"new string\"\r\n" );
                        return;
                    }

                    ch._socket._stringEditing = ch._socket._stringEditing.Replace( arg2, arg3 );
                    text += "'" + arg2 + "' replaced with '" + arg3 + "'.\r\n";
                    ch.SendText( text );
                    return;
                }

                if( !StringsNotEqual( arg1, ".h" ) )
                {
                    ch.SendText( "Sedit help (commands on blank line):\r\n" );
                    ch.SendText( ".r 'old' 'new'     Replace a subpublic string (requires '', \"\").\r\n" );
                    ch.SendText( ".h                 Get help (this info).\r\n" );
                    ch.SendText( ".s                 Show public string so far.\r\n" );
                    ch.SendText( ".c                 Clear public string so far.\r\n" );
                    ch.SendText( "@                  End public string.\r\n" );
                    return;
                }

                ch.SendText( "StringAdd:  Invalid dot command.\r\n" );
                return;
            }

            if( argument[ 0 ] == '@' )
            {
                ch._socket._stringEditing = String.Empty;
                return;
            }

            // Truncate strings to 4096.
            if( buflen + arglen >= ( 4096 - 4 ) )
            {
                string buf1 = ch._name;

                ch.SendText( "The string was too long, the last line has been skipped.\r\n" );

                buf1 += " is trying to write a description that's too long.";
                Log.Trace( buf1 );

                // Force character out of editing mode.
                ch._socket._stringEditing = String.Empty;
                return;
            }
            if( ch.IsImmortal() )
            {
                string message = String.Format( "\r\n&+gAdding {0} chars to {1} chars leaving {2} left.&n\r\n",
                                                arglen, buflen, ( 4096 - 4 - buflen - arglen ) );
                ch.SendText( message );
            }

            text += ch._socket._stringEditing + argument + "\r\n";
            ch._socket._stringEditing = text;
            return;
        }

        /// <summary>
        /// Grabs and argument from the supplied string and returns it. Does not modify the original
        /// argument.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static string FirstArgument( string argument )
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < argument.Length; i++)
            {
                if (!Char.IsWhiteSpace(argument[i]))
                {
                    sb.Append(argument[i]);
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return true if an argument is completely numeric.  This is a generally redundant function since
        /// users can easily call Int32.TryParse themselves.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsNumber( string arg )
        {
            int whatever;
            return Int32.TryParse( arg, out whatever );
        }

        /// <summary>
        /// Given a string like 8.objectname, return 8 and 'objectname'.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static int NumberArgument( string argument, ref string arg )
        {
            for( int position = 0; position < argument.Length; ++position )
            {
                if( ( argument )[ position ] == '.' )
                {
                    // Convert everything before the period.
                    int number;
                    Int32.TryParse(argument.Substring(0, position), out number);
                    // Return everything after the period.
                    arg = argument.Substring( position + 1 );
                    return number;
                }
            }

            arg = argument;
            return 1;
        }

        /// <summary>
        /// Get the first argument in a string and return it.  Set the argFirst string to whatever is left.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argFirst"></param>
        /// <returns></returns>
        public static string OneArgument( string argument, ref string argFirst )
        {
            argument = argument.Trim();
            string[] arguments = argument.Split( " ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries );
            if( arguments.Length == 0 )
            {
                argFirst = String.Empty;
                return String.Empty;
            }
            int length = arguments[ 0 ].Length;
            argFirst = argument.Substring( length );
            return arguments[0];
        }

        /// <summary>
        /// Pick off last argument from a string and return the rest.  Understands quotes.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static string LastArgument( string argument )
        {
            string[] splitstring = argument.Split( ' ' );
            int size = splitstring.Length;
            if( size == 0 )
            {
                return string.Empty;
            }
            return splitstring[ size - 1 ];
        }

        /// <summary>
        /// Reimplementation of String.Equals( str, StringComparison.CurrentCultureIgnoreCase ).
        /// Should be removed and all references replaced with String.Equals.
        /// </summary>
        /// <param name="astr"></param>
        /// <param name="bstr"></param>
        /// <returns></returns>
        //[Obsolete]
        public static bool StringsNotEqual( string astr, string bstr )
        {
            return !astr.Equals( bstr, StringComparison.CurrentCultureIgnoreCase );
        }

        /// <summary>
        /// Reimplementation of String.StartsWith for compatibility with old functions.
        /// Should be removed and all references replaced with String.StartsWith.
        /// </summary>
        /// <param name="astr"></param>
        /// <param name="bstr"></param>
        /// <returns></returns>
        //[Obsolete]
        public static bool IsPrefixOf( string astr, string bstr )
        {
            if (bstr.StartsWith(astr, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reimplementation of String.EndsWith for compatbility with old crap.  Should be removed and
        /// all references replaced with String.EndsWith.
        /// </summary>
        /// <param name="astr"></param>
        /// <param name="bstr"></param>
        /// <returns></returns>
        public static bool IsSuffixOf( string astr, string bstr )
        {
            if( bstr.EndsWith( astr ) )
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the provided string is one of the names/keywords provided.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="namelist"></param>
        /// <returns></returns>
        public static bool NameContainedIn(string str, string namelist)
        {
            if (String.IsNullOrEmpty(namelist) || String.IsNullOrEmpty(str))
            {
                return false;
            }
            string[] names = namelist.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String name in names)
            {
                if (name.Equals(str, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// See if a string is a prefix of one of the names of an object. For non-
        /// string versions, strings are first converted to string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="namelist"></param>
        /// <returns></returns>
        public static bool NameIsPrefixOfContents( string str, string namelist )
        {
            if( str.Length == 0 || namelist.Length == 0 )
            {
                return false;
            }
            string name = String.Empty;
            for( ; ; )
            {
                namelist = OneArgument( namelist, ref name );
                if( name.Length == 0 )
                {
                    return false;
                }
                if( !IsPrefixOf( str, name ) )
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Returns a string containing an integer padded to the number of spaces
        /// requested.  Used during string conversion in a stream.  For integers greater
        /// than the requested number of spaces, it returns the whole integer and skews
        /// the  string - bad formatting is better than misinformation.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="spaces"></param>
        /// <returns></returns>
        public static string PadInt( int value, int spaces )
        {
            int addspaces = spaces - 1; // All numbers require a minimum one space.

            string output = value.ToString();

            if( value > 0 )
            {
                while( ( value /= 10 ) > 0 )
                {
                    --addspaces;
                }
            }
            else
            {
                // Negative number, add 1 for the sign.
                --addspaces;
                while( ( value /= 10 ) < 0 )
                {
                    --addspaces;
                }
            }
            while( addspaces > 0 )
            {
                output += " ";
                --addspaces;
            }
            return output;
        }

        /// <summary>
        /// Returns a string containing an integer padded to the number of spaces
        /// requested.  Used during string conversion in a stream.  For integers greater
        /// than the requested number of spaces, it returns the whole integer and skews
        /// the string - bad formatting is better than misinformation.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="spaces"></param>
        /// <returns></returns>
        public static string PadUint( uint value, int spaces )
        {
            int addspaces = spaces - 1; // All numbers require a minimum one space.

            string output = value.ToString();

            if( value > 0 )
            {
                while( ( value /= 10 ) > 0 )
                {
                    --addspaces;
                }
            }
            else
            {
                // Negative number, add 1 for the sign.
                --addspaces;
                while( ( value /= 10 ) < 0 )
                {
                    --addspaces;
                }
            }
            while( addspaces > 0 )
            {
                output += " ";
                --addspaces;
            }
            return output;
        }

        /// <summary>
        /// Pads a string to X spaces and outputs the whole thing.  Returns more than that
        /// if the public string is longer than the allowed area - bad formatting is better than
        /// misinformation.
        /// </summary>
        /// <param name="instring"></param>
        /// <param name="spaces"></param>
        /// <returns></returns>
        public static string PadStr( string instring, uint spaces )
        {
            if (instring == null)
            {
                instring = String.Empty;
            }
            if( instring.Length >= spaces )
            {
                return instring;
            }
            uint addspaces = spaces - (uint)instring.Length;
            while( addspaces > 0 )
            {
                instring += " ";
                --addspaces;
            }
            return instring;
        }

        /// <summary>
        /// Returns "is" or "are" depending on the previous word.  Used for building sentences.
        /// </summary>
        /// <param name="previousWord"></param>
        /// <returns></returns>
        public static String IsAre(string previousWord)
        {
            if (String.IsNullOrEmpty(previousWord))
                return String.Empty;
            else if (previousWord.EndsWith("s"))
            {
                return "are";
            }
            else
            {
                return "is";
            }
        }

        /// <summary>
        /// Checks whether a string starts with a vowel, and is color-code-aware.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsVowel( string arg )
        {
            int point = 0;

            // Deal with ANSI color codes.
            while( arg[ point ] == '&' )
            {
                ++point;
                if( arg[ point ] == 'n' || arg[ point ] == 'N' )
                {
                    ++point;
                }
                else
                {
                    if( arg[ point ] == '+' || arg[ point ] == '-' )
                    {
                        point += 2;
                    }
                    // If it legitimately begins with an "&"  then it's not a vowel.
                    else
                    {
                        return false;
                    }
                }
            }

            if( arg[ point ] == 'a' || arg[ point ] == 'A' || arg[ point ] == 'e' || arg[ point ] == 'E' ||
                    arg[ point ] == 'i' || arg[ point ] == 'I' || arg[ point ] == 'o' || arg[ point ] == 'O' ||
                    arg[ point ] == 'u' || arg[ point ] == 'U' )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the appropriate number suffix for a string. 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumberSuffix( int number )
        {
            // 11 to 13 are a special case...
            if (number > 10 && number < 14)
            {
                return "th";
            }
            switch( number % 10 )
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

        /// <summary>
        /// Takes a string and automatically inserts line breaks where needed if the string is
        /// longer than the specified wrap length.  This function is color-code aware, so a line
        /// with a lot of color codes will not be broken up prematurely.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineLength"></param>
        /// <returns></returns>
        public static string InsertLineBreaks(string text, int lineLength)
        {
            if (String.IsNullOrEmpty(text) || text.Length < lineLength)
            {
                return text;
            }

            bool escState = false;
            String outputText = String.Empty;
            int numChars = 0;

            for (int pos = 0; pos < text.Length; pos++)
            {
                // Plain text, not a color code.  Add it to the string and move on.
                if (escState == false && text[pos] != '')
                {
                    outputText += text[pos];
                    if (text[pos] != '\n')
                    {
                        ++numChars;
                    }
                    else
                    {
                        numChars = 0;
                    }
                }
                // Ampersand.  Add it to the text string without incrementing the number
                // of characters we have seen.
                else if (escState == false && text[pos] == '')
                {
                    escState = true;
                    outputText += text[pos];
                }
                else if (escState)
                {
                    // Plus and minus mean another character is coming.
                    if (text[pos] != 'm')
                    {
                        outputText += text[pos];
                        continue;
                    }
                    // Assume any character we receive is going to be the end of a color code string.
                    escState = false;
                    outputText += text[pos];
                }

                if (numChars >= lineLength)
                {
                    outputText += "\r\n";
                    numChars = 0;
                    // Remove a space before a non-space if we just wrapped a line.  This prevents this kind of
                    // goofy justification:
                    // This is a line
                    //  that continues
                    //  on to the next
                    // line.
                    if (pos < (text.Length - 2) && text[pos + 1] == ' ')
                    {
                        ++pos;
                    }
                }
                // Basic word breaks, nothing complex or special -- this can be improved.
                // What we do here is prematurely break if there's a space followed by a character
                // in the last two chars of a line.
                //
                // Does not handle long words well.
                else if (numChars >= lineLength - 3)
                {
                    if (pos < (text.Length - 2) && text[pos] == ' ' && text[pos + 1] != ' ' && text[pos + 2] != ' ')
                    {
                        outputText += "\r\n";
                        numChars = 0;
                    }
                    // Handle single-character words at the end of a line.
                    else if (pos < (text.Length - 2) && text[pos] == ' ' && text[pos + 1] != ' ' && text[pos + 2] == ' ')
                    {
                        outputText += text[pos + 1];
                        outputText += "\r\n";
                        pos += 2;
                        numChars = 0;
                    }
                    else if (pos < (text.Length - 1) && text[pos] == ' ' && text[pos + 1] != ' ')
                    {
                        outputText += "\r\n";
                        numChars = 0;
                    }
                }
            }

             return outputText;
        }

        /// <summary>
        /// Takes a string that may or may not be ANSI and capitalizes the first non-ANSI-code character.  Returns the modified string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String CapitalizeANSIString(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }

            string output = String.Empty;

            if (input.Length > 0)
            {
                int capPos = 0;
                foreach (char ch in input)
                {
                    if (char.IsLetter(ch) && (capPos == 0 || (capPos > 0 && input[capPos-1] != '+')))
                        break;
                    capPos++;
                }
                if (capPos < input.Length)
                {
                    if( capPos > 0 )
                    {
                        output = input.Substring(0, capPos) + Char.ToUpper(input[capPos]) + input.Substring(capPos + 1);
                    }
                    else
                    {
                        output = Char.ToUpper(input[capPos]) + input.Substring(capPos + 1);
                    }
                }
                else
                {
                    output = input;
                }
            }
            return output;
        }
    }
}