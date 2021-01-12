using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// IData base class. Simplify implementation of IData objects
    /// </summary>
    public abstract class AData : IData
    {
        /// <inheritdoc />
        public abstract EDataType[] DataLayout { get; }

        /// <inheritdoc />
        public InterObject ConvertToIntermediate()
        {
            InterObject intermediate = new InterObject(GetIntermediateFields());
            intermediate.Name = GetType().Name;
            return intermediate;
        }

        /// <summary>
        /// Return the interfields required to convert this IData to an
        /// intermediate InterObject
        /// </summary>
        /// <returns></returns>
        protected abstract InterField[] GetIntermediateFields();

        /// <inheritdoc />
        public bool TryConstructFromIntermediate(InterObject intermediate)
        {
            if (intermediate == null) return false;
            if (!this.ValidateDataLayout(intermediate.Fields)) return false;
            if (!TryConstructAfterValidation(intermediate.Fields)) return false;

            return true;
        }

        /// <summary>
        /// Validation is applied automatically, by AData. Children need
        /// to attempt to construct themselves from the provided fields
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected abstract bool TryConstructAfterValidation(InterField[] fields);
    }
}
