using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation
{   
    /// <summary>
    /// Implements the disposable behaviour of a Generator class. 
    /// Generators hold an internal reference to an externally provided StringBuilder, 
    /// which is populated using the Generate function. The default behaviour of the class is
    /// for Generate to be called on Dispose(), allowing it to be used with using statements. 
    /// 
    /// NOTE: All base classes should be prefixed with GT
    /// </summary>
    public abstract class AGenerator : IGenerator
    {
        // Should the Generator call generate on Dispose?
        private bool _generateOnDispose = false; 

        #region Internal API
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        internal AGenerator() { }
        #endregion

        #region Public API
        /// <inheritdoc />
        public StringBuilder OutputBuilder { get; set; }

        /// <inheritdoc />
        public IndentProvider IndentProvider { get; set; }

        /// <summary>
        /// Construct generator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public AGenerator(StringBuilder output)
        {
            OutputBuilder = output;
            IndentProvider = new IndentProvider();
            _generateOnDispose = true;
        }

        /// <summary>
        /// Creates a generator instance of a particular type that will generate internal
        /// to this generator, meaning it will follow the indent. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInternalGenerator<T>() where T : IGenerator, new()
        {
            T generator = new T();
            generator.OutputBuilder = OutputBuilder;
            generator.IndentProvider = IndentProvider;
            return generator;
        }

        /// <inheritdoc />
        public abstract void Generate();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_generateOnDispose) Generate();
        }
        #endregion
    }
}
