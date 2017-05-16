using System;
using System.Text.RegularExpressions;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// Data source column header information.
    /// </summary>
    public class ColumnHeader
    {
        #region Private Properties

        private string _name;
        private Type _type;
        private bool _isPrimaryKey;

        #endregion

        #region Constructor

        /// <summary>
        /// Data source column header information.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public ColumnHeader(string name)
        {
            _name = name;
            _type = typeof(string);
        }

        /// <summary>
        /// Data source column header information.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="type">The type of data in the column.</param>
        public ColumnHeader(string name, Type type)
        {
            _name = name;
            _type = type;
        }

        /// <summary>
        /// Data source column header information.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="type">The type of data in the column.</param>
        /// <param name="isPrimaryKey">A boolean indicating if the column is a primary key or not.</param>
        public ColumnHeader(string name, Type type, bool isPrimaryKey)
        {
            _name = name;
            _type = type;
            _isPrimaryKey = isPrimaryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the column.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (!IsValidName(value))
                {
                    throw new ArgumentException("Invalid column name specified.");
                }

                _name = value;
            }
        }
         
        /// <summary>
        /// The type of data in the column.
        /// </summary>
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// A boolean indicating if the column is a primary key or not.
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the Edm type name of the column.
        /// </summary>
        /// <returns>The Edm type name of the column.</returns>
        public string GetEdmTypeName()
        {
            if (_type == typeof(string) || _type == typeof(String))
            {
                return "Edm.String";
            }
            else if (_type == typeof(int) || _type == typeof(Int16) || _type == typeof(Int32) || _type == typeof(Int64) || _type == typeof(long))
            {
                return "Edm.Int64";
            }
            else if (_type == typeof(bool) || _type == typeof(Boolean))
            {
                return "Edm.Boolean";
            }
            else if (_type == typeof(double) || _type == typeof(Double))
            {
                return "Edm.Double";
            }
            else if (_type == typeof(DateTime))
            {
                return "Edm.dateTime";
            }
            else if (_type == typeof(Geography))
            {
                return "Edm.Geography";
            }

            return "Edm.String";
        }

        /// <summary>
        /// Gets the XML type name of the column.
        /// </summary>
        /// <returns>the XML type name of the column.</returns>
        public string GetXmlTypeName()
        {
            if (_type == typeof(string) || _type == typeof(String))
            {
                return "xs:string";
            }
            else if (_type == typeof(int) || _type == typeof(Int16) || _type == typeof(Int32) || _type == typeof(Int64) || _type == typeof(long))
            {
                return "xs:long";
            }
            else if (_type == typeof(bool) || _type == typeof(Boolean))
            {
                return "xs:boolean";
            }
            else if (_type == typeof(double) || _type == typeof(Double))
            {
                return "xs:double";
            }
            else if (_type == typeof(DateTime))
            {
                return "xs:dateTime";
            }
            else if (_type == typeof(Geography))
            {
                return "xs:anyType";
            }

            return "xs:string";
        }

        /// <summary>
        /// A string representing a ColumnHeader in the format Name(EdmTypeName) or Name(EdmTypeName,primaryKey) 
        /// </summary>
        /// <returns>Returns a string in the format Name(EdmTypeName) or  Name(EdmTypeName,primaryKey) </returns>
        public override string ToString()
        {
            return string.Format("{0}({1}{2})", _name, GetEdmTypeName(), (_isPrimaryKey) ? ",primaryKey" : "");
        }

        public override int GetHashCode()
        {
            int hash = 0;

            if (!string.IsNullOrWhiteSpace(_name))
            {
                hash ^= Name.GetHashCode();
            }

            if (_type != null)
            {
                hash ^= _type.GetHashCode();
            }

            hash ^= _isPrimaryKey.GetHashCode();
            
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is ColumnHeader)
            {
                return base.GetHashCode() == (obj as ColumnHeader).GetHashCode();
            }

            return false;
        }

        public static bool operator ==(ColumnHeader x, object y)
        {
            if (y != null)
            {
                return x.GetHashCode() == y.GetHashCode();
            }

            return false;
        }

        public static bool operator !=(ColumnHeader x, object y)
        {
            return !(x == y);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks the name of the colum to verify it is valid.
        /// Column property name can have up to 50 characters.
        /// The property name must contain alphanumeric characters and underscores (_) only.
        /// The first character of the property name must be a letter or an underscore.
        /// The property name cannot start with two underscores (__). Exceptions "__deleteEntity" and "_distance".
        /// Property names are case-insensitive.
        /// </summary>
        /// <returns></returns>
        private bool IsValidName(string name)
        {
            if (string.Compare(name, "__deleteEntity", StringComparison.Ordinal) == 0 ||
               string.Compare(name, "__distance", StringComparison.Ordinal) == 0)
            {
                return true;
            }

            var propertyNameRegex = new Regex("^([a-zA-Z_][a-zA-Z0-9][a-zA-Z0-9_]*|[a-zA-Z][a-zA-Z0-9_]*)$");
            return (!string.IsNullOrWhiteSpace(name) && name.Length <= 50 && propertyNameRegex.IsMatch(name));
        }

        #endregion
    }
}
