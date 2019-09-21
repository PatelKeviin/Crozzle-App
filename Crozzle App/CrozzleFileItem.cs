using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrozzleApplication
{
    class CrozzleFileItem
    {
        #region constants - symbols
        const String NoCrozzleItem = "NO_CROZZLE_ITEM";
        const String ConfigurationFileSymbol = "CONFIGURATION_FILE";
        const String WordListFileSymbol = "WORDLIST_FILE";
        const String RowsSymbol = "ROWS";
        const String ColumnsSymbol = "COLUMNS";
        const String RowSymbol = "ROW";
        const String ColumnSymbol = "COLUMN";
        const String ColonSymbol = ":";
        const String AtoZ = @"^[A-Z]$";
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region properties
        private String OriginalItem { get; set; }
        public Boolean Valid { get; set; } = false;
        public String Name { get; set; }
        public KeyValue KeyValue { get; set; }
        #endregion

        #region properties - testing the type of the crozzle file item
        public Boolean IsConfigurationFile
        {
            get { return (Regex.IsMatch(Name, @"^" + ConfigurationFileSymbol + @"$")); }
        }

        public Boolean IsWordListFile
        {
            get { return (Regex.IsMatch(Name, @"^" + WordListFileSymbol + @"$")); }
        }

        public Boolean IsRows
        {
            get { return (Regex.IsMatch(Name, @"^" + RowsSymbol + @"$")); }
        }

        public Boolean IsColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + ColumnsSymbol + @"$")); }
        }

        public Boolean IsRow
        {
            get { return (Regex.IsMatch(Name, @"^" + RowSymbol + @"$")); }
        }

        public Boolean IsColumn
        {
            get { return (Regex.IsMatch(Name, @"^" + ColumnSymbol + @"$")); }
        }
        #endregion

        #region constructors
        public CrozzleFileItem(String originalItemData)
        {
            OriginalItem = originalItemData;
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String crozzleFileItem, out CrozzleFileItem aCrozzleFileItem)
        {
            Errors = new List<String>();
            aCrozzleFileItem = new CrozzleFileItem(crozzleFileItem);

            // Discard comment.
            if (crozzleFileItem.Contains("//"))
            {
                int index = crozzleFileItem.IndexOf("//");
                if (crozzleFileItem.Contains("http://"))
                {
                    Char[] httpChar = new Char[] { 'h', 't', 't', 'p', ':', '/', '/' };
                    int indexHttp = crozzleFileItem.IndexOfAny(httpChar);

                    if(!(indexHttp + 5 == index))
                    {
                        crozzleFileItem = crozzleFileItem.Remove(index);
                        crozzleFileItem = crozzleFileItem.Trim();
                    }
                }
                else
                {
                    crozzleFileItem = crozzleFileItem.Remove(index);
                    crozzleFileItem = crozzleFileItem.Trim();
                }
            }

            if (Regex.IsMatch(crozzleFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aCrozzleFileItem.Name = NoCrozzleItem;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ConfigurationFileSymbol + @".*"))
            {
                // Get the CONFIGURATION_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ConfigurationFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ConfigurationFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + WordListFileSymbol + @".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, WordListFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = WordListFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + RowsSymbol + @".*"))
            {
                // Get the number of rows for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, RowsSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = RowsSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ColumnsSymbol + @".*"))
            {
                // Get the number of columns for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ColumnsSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ColumnsSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + RowSymbol + @".*"))
            {
                // Get data for a horizontal word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, RowSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = RowSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ColumnSymbol + @".*"))
            {
                // Get data for a vertical word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ColumnSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ColumnSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else
                Errors.Add(String.Format(CrozzleFileItemErrors.SymbolError, crozzleFileItem));

            aCrozzleFileItem.Valid = Errors.Count == 0;
            return (aCrozzleFileItem.Valid);
        }
        #endregion
    }
}