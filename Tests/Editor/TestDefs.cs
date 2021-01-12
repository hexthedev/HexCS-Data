using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;
using HexCS.Data.Runtime;

namespace HexCSTests.Data
{
    public static class TestDefs
    {
        #region Primatives
        public const string PrimativeYaml = @"%YAML 1.2
---
Bool: True

Int: 18

Float: 6.7

String: hello

***";

        public static readonly Primatives PrimativeRuntime = new Primatives()
        {
            Bool = true,
            Int = 18,
            Float = 6.7f,
            String = "hello"
        };
        #endregion

        #region PrimativeLists
        public const string PrimativeListYaml = @"%YAML 1.2
---
Bools:
- True
- False

Ints:
- 1
- 2

Floats:
- 6.7
- 7.8

Strings:
- Hey
- Bye

***";

        public static readonly PrimativeLists PrimativeListRuntime = new PrimativeLists()
        {
            Bools = new List<bool>() { true, false },
            Ints = new List<int>() { 1, 2 },
            Floats = new List<float>() { 6.7f, 7.8f },
            Strings = new List<string>() { "Hey", "Bye" }
        };
        #endregion

        #region Objects
        public const string ObjectsYaml = @"%YAML 1.2
---
Primatives:
  Bool: True
  Int: 18
  Float: 6.7
  String: hello

PrimativeLists:
  Bools:
  - True
  - False
  Ints:
  - 1
  - 2
  Floats:
  - 6.7
  - 7.8
  Strings:
  - Hey
  - Bye

***";

        public static readonly Objects ObjectsRuntime = new Objects()
        {
            Primatives = PrimativeRuntime,
            PrimativeLists = PrimativeListRuntime

        };
        #endregion

        #region ObjectList
        public const string ObjectListYaml = @"%YAML 1.2
---
ObjectList:
- Objects:
    Primatives:
      Bool: True
      Int: 18
      Float: 6.7
      String: hello
    PrimativeLists:
      Bools:
      - True
      - False
      Ints:
      - 1
      - 2
      Floats:
      - 6.7
      - 7.8
      Strings:
      - Hey
      - Bye
- Objects:
    Primatives:
      Bool: True
      Int: 18
      Float: 6.7
      String: hello
    PrimativeLists:
      Bools:
      - True
      - False
      Ints:
      - 1
      - 2
      Floats:
      - 6.7
      - 7.8
      Strings:
      - Hey
      - Bye

***";

        public static readonly ObjectLists ObjectListRuntime = new ObjectLists()
        {
            ObjectList = new List<Objects>() { ObjectsRuntime, ObjectsRuntime }
        };
        #endregion

        #region ObjectListList
        public const string ObjectListListYaml = @"%YAML 1.2
---
ObjectListList:
- ObjectLists:
    ObjectList:
    - Objects:
        Primatives:
          Bool: True
          Int: 18
          Float: 6.7
          String: hello
        PrimativeLists:
          Bools:
          - True
          - False
          Ints:
          - 1
          - 2
          Floats:
          - 6.7
          - 7.8
          Strings:
          - Hey
          - Bye
    - Objects:
        Primatives:
          Bool: True
          Int: 18
          Float: 6.7
          String: hello
        PrimativeLists:
          Bools:
          - True
          - False
          Ints:
          - 1
          - 2
          Floats:
          - 6.7
          - 7.8
          Strings:
          - Hey
          - Bye
- ObjectLists:
    ObjectList:
    - Objects:
        Primatives:
          Bool: True
          Int: 18
          Float: 6.7
          String: hello
        PrimativeLists:
          Bools:
          - True
          - False
          Ints:
          - 1
          - 2
          Floats:
          - 6.7
          - 7.8
          Strings:
          - Hey
          - Bye
    - Objects:
        Primatives:
          Bool: True
          Int: 18
          Float: 6.7
          String: hello
        PrimativeLists:
          Bools:
          - True
          - False
          Ints:
          - 1
          - 2
          Floats:
          - 6.7
          - 7.8
          Strings:
          - Hey
          - Bye

***";

        public static readonly ObjectListLists ObjectListListRuntime = new ObjectListLists()
        {
            ObjectListList = new List<ObjectLists>() { ObjectListRuntime, ObjectListRuntime }
        };
        #endregion
    }

    /// <summary>
    /// IData containing list of list of objects
    /// </summary>
    public class ObjectListLists : AData
    {
        public List<ObjectLists> ObjectListList;

        #region IData
        /// <inheritdoc />
        public override EDataType[] DataLayout => new EDataType[] { EDataType.Data };

        /// <inheritdoc />
        protected override InterField[] GetIntermediateFields() => new InterField[] {
            InterField.DataList(ObjectListList, nameof(ObjectListList)),
        };

        /// <inheritdoc />
        protected override bool TryConstructAfterValidation(InterField[] fields)
        {
            return fields[0].TryAsIDataList(out ObjectListList);
        }
        #endregion
    }

    /// <summary>
    /// IData containing list of objects
    /// </summary>
    public class ObjectLists : AData
    {
        public List<Objects> ObjectList;

        #region IData
        /// <inheritdoc />
        public override EDataType[] DataLayout => new EDataType[] { EDataType.Data };

        /// <inheritdoc />
        protected override InterField[] GetIntermediateFields() => new InterField[] {
            InterField.DataList(ObjectList, nameof(ObjectList)),
        };

        /// <inheritdoc />
        protected override bool TryConstructAfterValidation(InterField[] fields)
        {
            return fields[0].TryAsIDataList(out ObjectList);
        }
        #endregion
    }

    /// <summary>
    /// IData containing lists of all primatives
    /// </summary>
    public class Objects : AData
    {
        public Primatives Primatives;
        public PrimativeLists PrimativeLists;

        #region IData
        /// <inheritdoc />
        public override EDataType[] DataLayout => new EDataType[]
        {
            EDataType.Data,
            EDataType.Data
        };

        /// <inheritdoc />
        protected override InterField[] GetIntermediateFields() => new InterField[] {
            InterField.Data(Primatives, nameof(Primatives)),
            InterField.Data(PrimativeLists, nameof(PrimativeLists)),
        };

        /// <inheritdoc />
        protected override bool TryConstructAfterValidation(InterField[] fields)
        {
            return fields[0].TryAsIData(out Primatives)
            && fields[1].TryAsIData(out PrimativeLists);
        }
        #endregion
    }

    /// <summary>
    /// IData containing lists of all primatives
    /// </summary>
    public class PrimativeLists : AData
    {
        public List<bool> Bools;
        public List<int> Ints;
        public List<float> Floats;
        public List<string> Strings;

        #region IData
        /// <inheritdoc />
        public override EDataType[] DataLayout => new EDataType[]
        {
            EDataType.Bool,
            EDataType.Int,
            EDataType.Float,
            EDataType.String
        };

        /// <inheritdoc />
        protected override InterField[] GetIntermediateFields() => new InterField[] {
            InterField.BoolList(Bools, nameof(Bools)),
            InterField.IntList(Ints, nameof(Ints)),
            InterField.FloatList(Floats, nameof(Floats)),
            InterField.StringList(Strings, nameof(Strings)),
        };

        /// <inheritdoc />
        protected override bool TryConstructAfterValidation(InterField[] fields)
        {
            return fields[0].TryAsBoolList(out Bools)
            && fields[1].TryAsIntList(out Ints)
            && fields[2].TryAsFloatList(out Floats)
            && fields[3].TryAsStringList(out Strings);
        }
        #endregion
    }

    /// <summary>
    /// I data containing instance of all primatives
    /// </summary>
    public class Primatives : AData
    {
        public bool Bool;
        public int Int;
        public float Float;
        public string String;

        #region IData
        /// <inheritdoc />
        public override EDataType[] DataLayout => new EDataType[]
        {
            EDataType.Bool,
            EDataType.Int,
            EDataType.Float,
            EDataType.String
        };

        /// <inheritdoc />
        protected override InterField[] GetIntermediateFields() => new InterField[] {
            InterField.Bool(Bool, nameof(Bool)),
            InterField.Int(Int, nameof(Int)),
            InterField.Float(Float, nameof(Float)),
            InterField.String(String, nameof(String))
        };

        /// <inheritdoc />
        protected override bool TryConstructAfterValidation(InterField[] fields)
        {
            return fields[0].TryAsBool(out Bool)
            && fields[1].TryAsInt(out Int)
            && fields[2].TryAsFloat(out Float)
            && fields[3].TryAsString(out String);
        }
        #endregion
    }
}
