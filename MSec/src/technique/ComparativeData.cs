﻿/*******************************************************************************************************************************************************************
	File	:	ComparativeData.cs
	Project	:	MSec
	Author	:	Byron Worms
*******************************************************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*******************************************************************************************************************************************************************
	Interface: ComparativeData
*******************************************************************************************************************************************************************/
namespace MSec
{
    public interface ComparativeData
    {
        // Returns the type of the hash data
        Type getDataType();

        // Returns true if the result is accpeted (depending on the threshold and technique!)
        bool isAccepted();

        // Returns the match rate
        double? getMatchRate();

        // Converts the hash into a readable format
        string convertToString();
    }
}

/*******************************************************************************************************************************************************************
	Class: ComparativeData<_T>
*******************************************************************************************************************************************************************/
namespace MSec
{
    public class ComparativeData<_T> : ComparativeData
        where _T : new()
    {
        // Delegate for the hashing function
        public delegate string delegate_convertToString(_T _data);
        public delegate double delegate_convertToDouble(_T _data);

        // The hash data
        private _T m_data = default(_T);
        public _T Data
        {
            get { return m_data; }
            private set { }
        }

        // True if the "same" (depending on the threshold and technique!)
        private bool m_isAccepted = false;

        // The convert to string function
        private delegate_convertToString m_funcConv = null;
        public delegate_convertToString FuncConvertToString
        {
            private get { return m_funcConv; }
            set { m_funcConv = value; }
        }

        // The convert to double function
        private delegate_convertToDouble m_funcConvDouble = null;
        public delegate_convertToDouble FuncConvertToDouble
        {
            private get { return m_funcConvDouble; }
            set { m_funcConvDouble = value; }
        }

        // Constructor
        public ComparativeData(_T _data, bool _accepted, delegate_convertToString _funcConv = null, delegate_convertToDouble _funcConvDouble = null)
        {
            // Copy
            m_data = _data;
            m_isAccepted = _accepted;
            m_funcConv = _funcConv;
            m_funcConvDouble = _funcConvDouble;
        }

        // Override: ComparativeData::getDataType
        public Type getDataType()
        {
            return typeof(_T);
        }

        // Override: ComparativeData::getDataType
        public bool isAccepted()
        {
            return m_isAccepted;
        }

        // Override: ComparativeData::getMatchRate
        public double? getMatchRate()
        {
            if (m_funcConvDouble != null)
                return m_funcConvDouble(m_data);
            return null;
        }

        // Override: ComparativeData::convertToString
        public string convertToString()
        {
            if (m_funcConv != null)
                return m_funcConv(m_data);
            return m_data.ToString();
        }
    }
}
