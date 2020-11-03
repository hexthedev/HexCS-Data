using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A Collection of CSharpParamters that can eaisly be converted into a string
    /// </summary>
    public class ParameterCollection
    {
        private List<IParameterBlock> _parameters = new List<IParameterBlock>();

        #region API
        /// <summary>
        /// The paramaters in the collection
        /// </summary>
        public IEnumerable<IParameterBlock> Paramaters => _parameters;

        /// <summary>
        /// Add paramaters to the collection
        /// </summary>
        /// <param name="parameters"></param>
        public void AddParameters(params IParameterBlock[] parameters)
        {
            _parameters.AddRange(parameters);
        }

        /// <summary>
        /// Returns the paramters as if they are function inputs
        /// </summary>
        /// <returns></returns>
        public string ToFunctionInputString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendCharacterSeparatedCollection(
                _parameters, (s, e) => sb.Append(e.ToParameterString()), ", "
            );

            return sb.ToString();
        }
        #endregion
    }
}
