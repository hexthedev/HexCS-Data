using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// Generic version of a data array
    /// </summary>
    public abstract class DataArray : IData
    {
        #region Instance API
        /// <summary>
        /// Name of elemnt type used for IData
        /// </summary>
        public string ElementName;

        /// <summary>
        /// Length of the data array
        /// </summary>
        public int Length => GenericData == null ? 0 : GenericData.Length;

        /// <summary>
        /// Data in the data array
        /// </summary>
        public abstract object[] GenericData { get; }

        /// <inheritdoc />
        public abstract EDataType[] DataLayout { get; protected set; }

        /// <summary>
        /// The Type of the elements in the list
        /// </summary>
        public EDataType ArrayType => DataLayout[0];

        /// <inheritdoc />
        public abstract InterObject ConvertToIntermediate();

        /// <inheritdoc />
        public abstract bool TryConstructFromIntermediate(InterObject intermediate);
        #endregion

        #region Static API
        /// <summary>
        /// Return a String DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<string> StringArray(params string[] array) => new DataArray<string>(array, EDataType.String);

        /// <summary>
        /// Return a int DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<int> IntArray(params int[] array) => new DataArray<int>(array, EDataType.Int);

        /// <summary>
        /// Return a float DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<float> FloatArray(params float[] array) => new DataArray<float>(array, EDataType.Float);

        /// <summary>
        /// Return a bool DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<bool> BoolArray(params bool[] array) => new DataArray<bool>(array, EDataType.Bool);

        /// <summary>
        /// Return a bool DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<object> AutoArray(params object[] array) => new DataArray<object>(array, EDataType.Auto);

        /// <summary>
        /// Return a IData DataArray Object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataArray<InterObject> IDataArray<T>(T[] array) where T:IData
        {
            return new DataArray<InterObject>(
                UTArray.ConstructArray<InterObject>(
                    array.Length,
                    (i, e) => array[i].ConvertToIntermediate()
                ),
                EDataType.Data,
                array == null || array.Length == 0 ? "" : array[0].GetType().Name 
            );
        }
        #endregion
    }


    /// <summary>
    /// Used to convert list into intemediate type and back from it
    /// </summary>
    public class DataArray<T> : DataArray
    {
        private T[] _data;

        #region Instance API
        /// <inheritdoc/>
        public override object[] GenericData => _data == null ? new object[0] : UTArray.ConstructArray<object>(_data.Length, (i, e) => _data[i]);

        /// <summary>
        /// Data in the data array
        /// </summary>
        public T[] Data
        {
            get => _data == null ? new T[0] : _data;
            set => _data = value;
        }

        /// <inheritdoc />
        public override EDataType[] DataLayout { get; protected set; }

        /// <summary>
        /// The Type of the elements in the list
        /// </summary>
        public EDataType ListType => DataLayout[0];

        /// <summary>
        /// Can be used to construct data array. Should use DataArray.Array calls instead.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public DataArray(T[] data, EDataType type, string elementName = "")
        {
            Data = data;
            DataLayout = new EDataType[] { type };
            ElementName = elementName;
        }

        /// <inheritdoc />
        public override InterObject ConvertToIntermediate()
        {
            // size
            InterField[] obj = UTArray.ConstructArray<InterField>(
                Length, 
                (i, e) => 
                {
                    return InterField.Custom(
                        GenericData[i],
                        ListType,
                        ElementName
                    );
                }
            );

            return new InterObject() { Fields = obj, Name = ElementName };
        }

        /// <inheritdoc />
        public override bool TryConstructFromIntermediate(InterObject intermediate)
        {
            InterField[] fields = intermediate.Fields;
            
            Data = UTArray.ConstructArray<T>(fields.Length, (i, e) => (T)fields[i].Object);
            return true;
        }
        #endregion
    }
}
