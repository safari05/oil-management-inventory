using Dapper;
using Oil.Management.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Oil.Management.Shared
{
    /*
  *
  * Common Class For App
  * 
  * Contents :
  * 1. Conection To DB
  * 2. Current Date
  * 3. Encrypt n Decrypt password
  * 4. Error Message for Services
  * Version 1.0.0
  * Author : Eri Safari
  * copyright : Eri Safari
  */
    public class Common
    {
        public Common()
        {

        }

        private readonly string KeyString = AppMgtSetting.Secret;
        private static Regex sUserNameAllowedRegEx = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$", RegexOptions.Compiled);
        private static Regex sUserNameIllegalEndingRegEx = new Regex(@"(\.|\-|\._|\-_)$", RegexOptions.Compiled);
        public IDbConnection DbConnection
        {
            get
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
                return new SqlConnection(AppMgtSetting.ConnectionString);
            }
        }

        public string GetErrorMessage(string MethodeName, Exception ex)
        {
            string errMessage = ex.Message;
            if (ex.InnerException != null)
            {
                errMessage = ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                {
                    errMessage = ex.InnerException.InnerException.Message;
                    if (ex.InnerException.InnerException.InnerException != null)
                    {
                        errMessage = ex.InnerException.InnerException.InnerException.Message;
                    }
                }
            }
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return MethodeName + ", line: " + lineNumber + Environment.NewLine + "Error Message: " + errMessage;
        }
    }
}
