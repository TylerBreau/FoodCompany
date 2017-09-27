/*
 * Author : Tyler Breau
 * Creation Date : September 27, 2017
 * Last Updated : September 27, 2017
 * Description:
 *  Holds classes and methods to connect, retrieve, update, and any other form of interaction with the database.
 **/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheFoodCompany
{
    public class DataAccess
    {
        public struct Parameter
        {
            public string name;
            public object value;

            public Parameter(string _name, object _value)
            {
                name = _name;
                value = _value;
            }
        }


        public static class SQLAccess
        {
            private static readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            
            //Abstract the SQL Client from the rest of the code by creating a CommandType Wrapper
            public enum SqlCommandType
            {
                Text = CommandType.Text,
                StoredProcedure = CommandType.StoredProcedure,
                TableDirect = CommandType.TableDirect
            }

            /// <summary>
            /// Retrieves data from database and returns DataReader
            /// </summary>
            /// <param name="query">The query to run</param>
            /// <param name="_commandType">The type of query that is to be run. Defaults to Stored Procedure</param>
            /// <returns>DataReader with the data retrieved</returns>
            public static SqlDataReader GetData(string query, SqlCommandType _commandType = SqlCommandType.StoredProcedure, List<Parameter> parameters = null) 
            {
                //We append "_" to the "commandType" argument to differeniate between the argument and SqlCommand.CommandType.

                SqlCommand comm = new SqlCommand()
                {
                    Connection = conn,
                    CommandText = query,
                    CommandType = (CommandType)_commandType
                };

                if(parameters != null)
                {
                    LoadParameters(ref comm, parameters);
                }

                using (comm)
                {
                    comm.Connection.Open();
                    return comm.ExecuteReader(CommandBehavior.CloseConnection); //Returning inside a using statement will cause the Connection to close automatically.
                }
            }

            private static void LoadParameters(ref SqlCommand comm, List<Parameter> parameters)
            {
                foreach(Parameter p in parameters)
                {
                    comm.Parameters.AddWithValue(p.name, p.value);
                }
            }
        }
    }
}