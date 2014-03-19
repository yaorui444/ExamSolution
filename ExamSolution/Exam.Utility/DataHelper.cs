using System;
using System.Text;
using System.Data;

namespace Exam.Utility
{
    /// <summary>
    /// Static helper class used by the factories when getting 
    /// data from ADO.NET objects (i.e. IDataReader)
    /// </summary>
    public static class DataHelper
    {
        #region Static Data Helper Methods

        public static DateTime GetDateTime(object value)
        {
            DateTime dateValue = DateTime.MinValue;
            if ((value != null) && (value != DBNull.Value))
            {
                dateValue = (DateTime)value;
            }
            return dateValue;
        }

        public static DateTime? GetNullableDateTime(object value)
        {
            DateTime? dateTimeValue = null;
            DateTime dbDateTimeValue;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (DateTime.TryParse(value.ToString(), out dbDateTimeValue))
                {
                    dateTimeValue = dbDateTimeValue;
                }
            }
            return dateTimeValue;
        }

        public static int GetInteger(object value)
        {
            int integerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                double doubleValue = 0;
                if (double.TryParse(value.ToString(), out doubleValue))
                    integerValue = (int)System.Math.Round(doubleValue);
            }
            return integerValue;
        }

        public static int? GetNullableInteger(object value)
        {
            int? integerValue = null;
            int parseIntegerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (int.TryParse(value.ToString(), out parseIntegerValue))
                {
                    integerValue = parseIntegerValue;
                }
            }
            return integerValue;
        }

        public static decimal GetDecimal(object value)
        {
            decimal decimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                decimal.TryParse(value.ToString(), out decimalValue);
            }
            return decimalValue;
        }

        public static decimal? GetNullableDecimal(object value)
        {
            decimal? decimalValue = null;
            decimal parseDecimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (decimal.TryParse(value.ToString(), out parseDecimalValue))
                {
                    decimalValue = parseDecimalValue;
                }
            }
            return decimalValue;
        }

        public static double GetDouble(object value)
        {
            double doubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                double.TryParse(value.ToString(), out doubleValue);
            }
            return doubleValue;
        }

        public static double? GetNullableDouble(object value)
        {
            double? doubleValue = null;
            double parseDoubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (double.TryParse(value.ToString(), out parseDoubleValue))
                {
                    doubleValue = parseDoubleValue;
                }
            }

            return doubleValue;
        }

        public static Guid GetGuid(object value)
        {
            Guid guidValue = Guid.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch (FormatException ex)
                {
                    // really do nothing, because we want to return a value for the guid = Guid.Empty;
                }
            }
            return guidValue;
        }

        public static Guid? GetNullableGuid(object value)
        {
            Guid? guidValue = null;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch (FormatException ex)
                {
                    // really do nothing, because we want to return a value for the guid = null;
                }
            }
            return guidValue;
        }

        public static string GetString(object value)
        {
            string stringValue = string.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                stringValue = value.ToString().Trim();
            }
            return stringValue;
        }

        public static bool GetBoolean(object value)
        {
            bool bReturn = false;
            if (value != null && value != DBNull.Value)
            {
                bReturn = Convert.ToBoolean(value);
            }
            return bReturn;
        }

        public static bool? GetNullableBoolean(object value)
        {
            bool? bReturn = null;
            if (value != null && value != DBNull.Value)
            {
                bReturn = (bool)value;
            }

            return bReturn;
        }

        public static T GetEnumValue<T>(string databaseValue) where T : struct
        {
            T enumValue = default(T);

            object parsedValue = Enum.Parse(typeof(T), databaseValue);
            if (parsedValue != null)
            {
                enumValue = (T)parsedValue;
            }

            return enumValue;
        }

        public static bool ReaderContainsColumnName(DataTable schemaTable, string columnName)
        {
            bool containsColumnName = false;
            foreach (DataRow row in schemaTable.Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                {
                    containsColumnName = true;
                    break;
                }
            }
            return containsColumnName;
        }

        public static object GetSqlValue(object value)
        {
            if (value != null)
            {
                if (value is Guid)
                {
                    return GetSqlValue((Guid)value);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(string value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value);
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(DateTime value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(Guid value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        #endregion
    }
}
