
using System;

namespace DummyInputProject
{
    /// <summary>
    /// Summary tags are used to provide a description of a type's purpose and basic functionality. They are used
    /// to provide IntelliSense information for the type.
    /// </summary>
    ///     <remarks>
    ///         Remarks tags are used to provide additional information about a type that is either too long or too
    ///         detailed to be put in a summary tag.
    ///         <para>
    ///             The &lt;para&gt; tag can be used to split text into multiple paragraphs.
    ///         </para>
    ///         <para>
    ///         &lt;list&gt; tags can be used to display a variety of list types.
    ///         </para>
    ///         <list type="bullet">
    ///             <listheader>
    ///                 <term>Bulleted List</term>
    ///                 <description>A list separated by bullet points.</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>Element A</term>
    ///                 <description>Element A description.</description>
    ///             </item>
    ///             <item>
    ///                 <term>Element B</term>
    ///                 <description>Element B description.</description>
    ///             </item>
    ///         </list>
    ///         <list type="number">
    ///             <listheader>
    ///                 <term>Numbered List</term>
    ///                 <description>An enumerated list of items.</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>Element 1</term>
    ///                 <description>Element 1 description.</description>
    ///             </item>
    ///             <item>
    ///                 <term>Element 2</term>
    ///                 <description>Element 2 description.</description>
    ///             </item>
    ///         </list>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Column A</term>
    ///                 <term>Column B</term>
    ///                 <description>A table.</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>Row 1, Column A</term>
    ///                 <term>Row 1, Column B</term>
    ///             </item>
    ///             <item>
    ///                 <term>Row 2, Column A</term>
    ///                 <term>Row 2, Column B</term>
    ///             </item>
    ///         </list>
    ///         <see cref="ExampleClass.ExampleGetter"/>
    ///         <seealso cref="DescendantClass"/>
    ///     </remarks>
    /// <seealso cref="this is some serious bullshit"/>
    /// <example>
    /// Example tags are used to give examples of how a type is to be used. They commonly contain &lt;c&gt; and &lt;code&gt; tags.
    /// &lt;c&gt; tags are used to mark some in-line text as code. For example, <c>MyClass</c> can be marked as code in-line.
    /// For multiple lines of code, the &lt;code&gt; tag is used.
    ///     <code>
    ///     Example code line 1
    ///     Example code line 2
    ///     </code>
    /// </example>
    /// <seealso cref="ExampleClass.ExampleGetter"/>
    public class ExampleClass
    {
        /// <summary>
        /// Gets and Sets the TestString property.
        /// </summary>
        /// <value>The &lt;value&gt; tag is used to document the value a property represents.</value>
        public string TestString { get; set; }

        /// <summary>
        /// The days of the week starting with Sunday.
        /// </summary>
        public enum DaysOfTheWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        };

        /// <summary>
        /// Constructor summary here.
        /// </summary>
        public ExampleClass()
        {
            
        }

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="foo">
        ///     Parameter description here.
        ///     The &lt;paramref&gt; tag can be used to reference parameters by name.
        ///     Example: The <paramref name="foo"/> parameter is the only argument of this constructor.
        /// </param>
        public ExampleClass(int foo)
        {
            
        }

        /// <summary>
        /// A method with no parameters that returns an integer.
        /// </summary>
        /// <returns>
        ///     The &lt;returns&gt; tag is used to describe the object returned by a method.
        /// </returns>
        /// <exception cref="NotImplementedException">
        ///     The &lt;exception&gt; tag is used to document the possible exceptions that may be thrown be a method.
        /// </exception>
        public int ExampleGetter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">
        ///     The &lt;typeparam&gt; tag is used to document the type and purpose of generic parameters.
        /// </typeparam>
        /// <returns>
        ///     An instance of type <typeparamref name="T"/>
        ///     The &lt;typeparamref&gt; tag is used to reference type parameters by name.
        /// </returns>
        public T GenericFunction<T>() where T : ExampleClass
        {
            return null;
        }
    }

    /// <summary>
    /// Class summary here.
    /// </summary>
    public class DescendantClass : ExampleClass
    {
        
    }

    /// <summary>
    /// Class summary here.
    /// </summary>
    /// <typeparam name="T">Type description here.</typeparam>
    public class GenericClass<T>
    {
        
    }
}

