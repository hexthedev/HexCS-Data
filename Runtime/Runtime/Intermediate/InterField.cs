using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// InterFields represent the fields of an object that you are parsing. 
    /// </summary>
    public class InterField
    {
        /// <summary>
        /// Name of the field. If null, then the name must be interered some how. 
        /// Normally name interfence happens during IData TryConstruct from intermediate
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of the inter object
        /// </summary>
        public EDataType Type;

        /// <summary>
        /// The object data
        /// </summary>
        public object Object;

        /// <summary>
        /// Is this an array field. If so, then the type is the type of
        /// every element in the field
        /// </summary>
        public bool IsArray;

        #region Bool
        /// <summary>
        /// Convert string to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField Bool(bool ob, string name = null) => new InterField() { Name = name, Type = EDataType.Bool, Object = ob };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAsBool(out bool value) => TryAsPrimative(out value, bool.Parse);

        /// <summary>
        /// Implicit cast string to interobject
        /// </summary>
        /// <param name="val"></param>
        public static implicit operator InterField(bool val) => Bool(val);

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField BoolList(List<bool> list, string name = null) => GenericDataArray(EDataType.Bool, DataArray.BoolArray(list.ToArray()), name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryAsBoolList(out List<bool> list) => TryAsPrimativeList(out list, bool.Parse);
        #endregion

        #region String
        /// <summary>
        /// Convert string to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField String(string ob, string name = null) => new InterField() {Name = name, Type = EDataType.String, Object = ob };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAsString(out string value) => TryAsPrimative(out value, s => s);

        /// <summary>
        /// Implicit cast string to interobject
        /// </summary>
        /// <param name="val"></param>
        public static implicit operator InterField(string val) => String(val);

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField StringList(List<string> list, string name = null) => GenericDataArray(EDataType.String, DataArray.StringArray(list.ToArray()), name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryAsStringList(out List<string> list) => TryAsPrimativeList(out list, s => s);
        #endregion

        #region Int
        /// <summary>
        /// Convert int to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField Int(int ob, string name = null) => new InterField() { Name = name, Type = EDataType.Int, Object = ob };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public bool TryAsInt(out int integer) => TryAsPrimative(out integer, int.Parse);

        /// <summary>
        /// Implicit cast str to interobject
        /// </summary>
        /// <param name="val"></param>
        public static implicit operator InterField(int val) => Int(val);

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField IntList(List<int> list, string name = null) => GenericDataArray(EDataType.Int, DataArray.IntArray(list.ToArray()), name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryAsIntList(out List<int> list) => TryAsPrimativeList(out list, int.Parse);
        #endregion

        #region Float
        /// <summary>
        /// Convert float to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField Float(float ob, string name = null) => new InterField() { Name = name, Type = EDataType.Float, Object = ob };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAsFloat(out float value) => TryAsPrimative(out value, float.Parse);

        /// <summary>
        /// Implicit cast float to interobject
        /// </summary>
        /// <param name="val"></param>
        public static implicit operator InterField(float val) => Float(val);

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField FloatList(List<float> list, string name = null) => GenericDataArray(EDataType.Float, DataArray.FloatArray(list.ToArray()), name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryAsFloatList(out List<float> list) => TryAsPrimativeList(out list, float.Parse);
        #endregion

        #region DataArray
        /// <summary>
        /// Generic Data array normally used by more concrete funcitons like IntList
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField GenericDataArray(EDataType type, DataArray data, string name = null)
        {
            return new InterField() { Name = name, Type = type, Object = data.ConvertToIntermediate(), IsArray = true };
        }
        #endregion

        #region Data
        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField Data(IData ob, string name = null) => new InterField() { Name = name, Type = EDataType.Data, Object = ob.ConvertToIntermediate() };

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField DataList<T>(List<T> list, string name = null) where T : IData
            => GenericDataArray(EDataType.Data, DataArray.IDataArray(list.ToArray()), name);

        /// <summary>
        /// Try return field object as an unpacked IData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TryAsIData<T>(out T data)
            where T : IData, new()
        {
            if (!(Object is InterObject))
            {
                data = default;
                return false;
            }

            T toReturn = new T();

            if (!toReturn.TryConstructFromIntermediate((InterObject)Object))
            {
                data = default;
                return false;
            }

            data = toReturn;
            return true;
        }
        #endregion

        #region Custom
        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static InterField Custom(object ob, EDataType type, string name = null) => new InterField() { Name = name, Type = type, Object = ob };

        /// <summary>
        /// Convert IData to InterObject
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="isArray"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterField Auto(object ob, bool isArray = false, string name = null) => new InterField() {
            Name = name,
            Type = EDataType.Auto,
            Object = ob,
            IsArray = isArray
        };
        #endregion

        #region List
        /// <summary>
        /// Try to interpret a field as a list. The Parse function is used
        /// to convert Auto type fields full of strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="parse"></param>
        /// <returns></returns>
        public bool TryAsPrimativeList<T>(out List<T> list, Func<string, T> parse = null)
        {
            if(!TryAsDataArray(out DataArray da))
            {
                list = null;
                return false;
            }

            List<T> toReturn = new List<T>();
            
            if(parse != null && Type == EDataType.Auto)
            {
                foreach (object o in da.GenericData)
                {
                    if (o is string)
                    {
                        toReturn.Add(parse((string)o));
                    }
                    else
                    {
                        list = null;
                        return false;
                    }
                }
            } 
            else
            {
                foreach(object o in da.GenericData)
                {
                    if(o is T)
                    {
                        toReturn.Add((T)o);
                    }
                    else
                    {
                        list = null;
                        return false;
                    }
                }
            }

            list = toReturn;
            return true; 
        }

        /// <summary>
        /// Try to interpret a field as a list of IData objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryAsIDataList<T>(out List<T> list)
            where T : IData, new()
        {
            if (!TryAsDataArray(out DataArray da))
            {
                list = null;
                return false;
            }

            List<T> toReturn = new List<T>();

            foreach(object o in da.GenericData)
            {
                if(!(o is InterField))
                {
                    list = null;
                    return false;
                }

                InterField field = (InterField)o;

                if(field.TryAsIData(out T data))
                {
                    toReturn.Add(data);
                } 
                else
                {
                    list = null;
                    return false;
                }
            }

            list = toReturn;
            return true;
        }
        #endregion


        #region InterObject
        /// <summary>
        /// Try to interpret this field as an InterObject. Mostly used internally.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryAsInterObject(out InterObject obj)
        {
            obj = Object as InterObject;
            return obj != null;
        }
        #endregion

        #region DataArray
        /// <summary>
        /// Try to interpret this field as an InterObject. Mostly used internally.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool TryAsDataArray(out DataArray array)
        {
            array = Object as DataArray;
            return array != null;
        }
        #endregion

        /// <summary>
        /// Convert InterField into Interfield[].
        /// </summary>
        /// <param name="obj"></param>
        public bool TryAsObject(out InterObject obj)
        {
            if (!(Object is InterObject))
            {
                obj = default;
                return false;
            }

            obj = (InterObject)Object;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>t
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is InterField)) return false;

            InterField other = (InterField)obj;

            object compareThis = Type == EDataType.Auto ? CastAuto(Object, other.Type, IsArray) : Object;
            object compareOther = other.Type == EDataType.Auto ? CastAuto(other.Object, Type, other.IsArray) : other.Object;

            if (compareThis == null || compareOther == null) return false;
            return compareThis.Equals(compareOther);
        }



        /// <summary>
        /// Try to get a field as some primatve values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primative"></param>
        /// <param name="parse"></param>
        /// <returns></returns>
        public bool TryAsPrimative<T>(out T primative, Func<string, T> parse = null)
        {
            if(Type == EDataType.Auto)
            {
                if (Object is string)
                {
                    if (parse == null)
                    {
                        primative = default;
                        return false;
                    }

                    primative = parse((string)Object);
                    return true;
                }
            }
            else
            {
                if(Object is T)
                {
                    primative = (T)Object;
                    return true;
                }
            }

            primative = default;
            return false;
        }


        private object CastAuto(object value, EDataType target, bool isArray = false)
        {
            if (target == EDataType.Auto) return value; // two auto values should compare
            if (target == EDataType.Data || isArray) return value as InterObject; // if data or array, value should be an InterObject

            string str = value as string;  // otherwise, an auto value is a string that needs parsing to primative
            if (str == null) return null;

            switch (target)
            {
                case EDataType.Bool: return bool.Parse(str);
                case EDataType.Float: return float.Parse(str);
                case EDataType.Int: return int.Parse(str);
                case EDataType.String: return str;
            }

            return null;
        }

        public override int GetHashCode()
        {
            int hashCode = 247454643;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Object);
            hashCode = hashCode * -1521134295 + IsArray.GetHashCode();
            return hashCode;
        }
    }
}
