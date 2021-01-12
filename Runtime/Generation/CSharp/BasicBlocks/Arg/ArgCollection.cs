using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A Collection of CSharpParamters that can eaisly be converted into a string
    /// </summary>
    public class ArgCollection
    {
        private List<IArg> _args = new List<IArg>();

        #region API
        /// <summary>
        /// Number of args in the collection
        /// </summary>
        public int Count => _args.Count;

        /// <summary>
        /// The paramaters in the collection
        /// </summary>
        public IArg[] Args => _args.ToArray();

        /// <summary>
        /// Add paramaters to the collection
        /// </summary>
        /// <param name="parameters"></param>
        public void AddArgs(params IArg[] args)
        {
            _args.AddRange(args);
        }

        /// <summary>
        /// Returns the paramters as if they are function inputs
        /// </summary>
        /// <returns></returns>
        public string ToArgString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendCharacterSeparatedCollection(
                _args, (s,a) => s.Append(a.ToArgumentString()), ", "
            );

            return sb.ToString();
        }
        #endregion
    }
}
