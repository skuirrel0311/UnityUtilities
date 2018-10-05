using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HyperCasual
{
    public static class Calculator
    {
        public enum CompareOperation
        {
            // Inspectorのポップアップで使うのでIDを指定せずに連番であって欲しいです
            Equal,
            NotEqual,
            Greater,
            Less,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo,
        }
        public static string ToShortString (this CompareOperation operation)
        {
            switch (operation) {
            case CompareOperation.Equal:
                return "EQ";
            case CompareOperation.NotEqual:
                return "NOT";
            case CompareOperation.Greater:
                return "GT";
            case CompareOperation.Less:
                return "LT";
            case CompareOperation.GreaterThanOrEqualTo:
                return "GTE";
            case CompareOperation.LessThanOrEqualTo:
                return "LTE";
            default:
                Debug.LogErrorFormat ("unexpected operation type {0}", operation);
                return "";
            }
        }
        public static string ToMarkString (this CompareOperation operation)
        {
            switch (operation) {
            case CompareOperation.Equal:
                return "==";
            case CompareOperation.NotEqual:
                return "!=";
            case CompareOperation.Greater:
                return ">";
            case CompareOperation.Less:
                return "<";
            case CompareOperation.GreaterThanOrEqualTo:
                return ">=";
            case CompareOperation.LessThanOrEqualTo:
                return "<=";
            default:
                Debug.LogErrorFormat ("unexpected operation type {0}", operation);
                return "";
            }
        }

        public enum BooleanOperation
        {
            And,
            Or,
            Xor
        }

        public static bool Calculate<T> (T value1, CompareOperation operation, T value2)
            where T : IComparable<T>, IEquatable<T>
        {
            //OPTIMIZE: box化,unbox化が走るので、気になるならTをstructにするなど
            switch (operation) {
            case CompareOperation.Equal:
                return value1.Equals (value2);
            case CompareOperation.NotEqual:
                return !value1.Equals (value2);
            case CompareOperation.Greater:
                return value1.CompareTo (value2) > 0;
            case CompareOperation.Less:
                return value1.CompareTo (value2) < 0;
            case CompareOperation.GreaterThanOrEqualTo:
                return value1.CompareTo (value2) >= 0;
            case CompareOperation.LessThanOrEqualTo:
                return value1.CompareTo (value2) <= 0;
            default:
                Debug.LogErrorFormat ("unexpected operation type {0}", operation);
                return true;
            }
        }

        public static bool Calculate (bool value1, BooleanOperation operation, bool value2)
        {
            switch (operation) {
            case BooleanOperation.And:
                return value1 && value2;
            case BooleanOperation.Or:
                return value1 || value2;
            case BooleanOperation.Xor:
                return value1 ^ value2;
            default:
                Debug.LogErrorFormat ("unexpected operation type {0}", operation);
                return true;
            }
        }

        static string[] markStringList;
        public static string[] MarkStringListOfCompareOp{
            get{
                if(markStringList == null){
                    var list = new List<string>();
                    foreach(var e in Enum.GetValues(typeof(CompareOperation)))
                    {
                        list.Add(((CompareOperation)e).ToMarkString());
					}
                    markStringList = list.ToArray();
                }
                return markStringList;
            }
        }
    }
}