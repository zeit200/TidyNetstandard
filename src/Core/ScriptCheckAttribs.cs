using System;

namespace Tidy.Core
{
    /// <summary>
    ///     Check attributes.
    ///     (c) 1998-2000 (W3C) MIT, INRIA, Keio University
    ///     See Tidy.cs for the copyright notice.
    ///     Derived from
    ///     <a href="http://www.w3.org/People/Raggett/tidy">
    ///         HTML Tidy Release 4 Aug 2000
    ///     </a>
    /// </summary>
    /// <author>Dave Raggett &lt;dsr@w3.org&gt;</author>
    /// <author>Andy Quick &lt;ac.quick@sympatico.ca&gt; (translation to Java)</author>
    /// <author>Seth Yates &lt;seth_yates@hotmail.com&gt; (translation to C#)</author>
    /// <version>1.0, 1999/05/22</version>
    /// <version>1.0.1, 1999/05/29</version>
    /// <version>1.1, 1999/06/18 Java Bean</version>
    /// <version>1.2, 1999/07/10 Tidy Release 7 Jul 1999</version>
    /// <version>1.3, 1999/07/30 Tidy Release 26 Jul 1999</version>
    /// <version>1.4, 1999/09/04 DOM support</version>
    /// <version>1.5, 1999/10/23 Tidy Release 27 Sep 1999</version>
    /// <version>1.6, 1999/11/01 Tidy Release 22 Oct 1999</version>
    /// <version>1.7, 1999/12/06 Tidy Release 30 Nov 1999</version>
    /// <version>1.8, 2000/01/22 Tidy Release 13 Jan 2000</version>
    /// <version>1.9, 2000/06/03 Tidy Release 30 Apr 2000</version>
    /// <version>1.10, 2000/07/22 Tidy Release 8 Jul 2000</version>
    /// <version>1.11, 2000/08/16 Tidy Release 4 Aug 2000</version>
    internal class ScriptCheckAttribs : ICheckAttribs
    {
        public virtual void Check(Lexer lexer, Node node)
        {
            node.CheckUniqueAttributes(lexer);

            AttVal lang = node.GetAttrByName("language");
            AttVal type = node.GetAttrByName("type");
            if (type == null)
            {
                Report.AttrError(lexer, node, "type", Report.MISSING_ATTRIBUTE);

                /* check for javascript */
                if (lang != null)
                {
                    string str = lang.Val;
                    if (str.Length > 10)
                    {
                        str = str.Substring(0, 10);
                    }

                    if ((String.CompareOrdinal(str, "javascript") == 0) || (String.CompareOrdinal(str, "jscript") == 0))
                    {
                        node.AddAttribute("type", "text/javascript");
                    }
                }
                else
                {
                    node.AddAttribute("type", "text/javascript");
                }
            }
        }
    }
}