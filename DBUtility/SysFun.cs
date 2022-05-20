using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

public static class SysFun {

    #region "���ֺ���"

    /// <summary>
    /// ȷ��������Ĳ�����ֵתΪ 0 �� 1 �����֡����� True ��תΪ 1��False ��תΪ 0 ��
    /// </summary>
    /// <param name="value">��ת�������ֵĲ���ֵ</param>
    /// <returns>0 �� 1 ������</returns>
    public static byte BoolToByte(object value) {
        try {
            if (Convert.ToBoolean(value) == true) {
                return 1;
            }
        } catch { }
        return 0;
    }

    /// <summary>
    /// ȷ���� 0 �� 1 �����ֻ��� True �� False ���ַ���������ֵתΪ Boolean ��ֵ������ 0 ��תΪ False��1 ��תΪ True ��
    /// </summary>
    /// <param name="value">0��1��"true"��"false"��True��False</param>
    /// <returns>True �� False �Ĳ���ֵ</returns>
    public static bool ToBool(object value) {
        try {
            if (Convert.ToBoolean(value)) {
                return true;
            }
        } catch {
            try {
                if (Convert.ToByte(value) == 1) {
                    return true;
                }
            } catch { }
        }
        return false;
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Byte ������
    /// </summary>
    /// <param name="value">0 �� 255���޷��ţ�������С�����֡�</param>
    /// <returns>0 - 255 ֮������֣���������쳣���ء�0��</returns>
    public static byte ToByte(object value) {
        try {
            return Convert.ToByte(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Integer ������
    /// </summary>
    /// <param name="value">-2,147,483,648 �� 2,147,483,647������С�����֡�</param>
    /// <returns>-2,147,483,648 �� 2,147,483,647 ֮������֣���������쳣���ء�0��</returns>
    public static int ToInt(object value) {
        try {
            return Convert.ToInt32(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Long ������
    /// </summary>
    /// <param name="value">-9,223,372,036,854,775,808 �� 9,223,372,036,854,775,807������С�����֡�</param>
    /// <returns>-9,223,372,036,854,775,808 �� 9,223,372,036,854,775,807 ֮������֣���������쳣���ء�0��</returns>
    public static long ToLong(object value) {
        try {
            return Convert.ToInt64(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Float ������
    /// </summary>
    public static float ToFloat(object value) {
        try {
            return Convert.ToSingle(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Decimal ������
    /// </summary>
    /// <param name="value">����������ֵ������С��λ��ֵ��Ϊ +/-79,228,162,514,264,337,593,543,950,335�����ھ��� 28 λС��λ�����֣���Χ�� +/-7.9228162514164337593543950335����С�Ŀ��÷������� 0.0000000000000000000000000001 (+/-1E-28)��</param>
    /// <returns>Decimal �����֣���������쳣���ء�0��</returns>
    public static decimal ToDec(object value) {
        try {
            return Convert.ToDecimal(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ double ������
    /// </summary>
    /// <param name="value">����������ֵ������С��λ��ֵ��Ϊ +/-79,228,162,514,264,337,593,543,950,335�����ھ��� 28 λС��λ�����֣���Χ�� +/-7.9228162514164337593543950335����С�Ŀ��÷������� 0.0000000000000000000000000001 (+/-1E-28)��</param>
    /// <returns>double �����֣���������쳣���ء�0��</returns>
    public static double ToDouble(object value) {
        try {
            return Convert.ToDouble(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// �жϴ���Ķ����Ƿ����תΪ����
    /// </summary>
    /// <param name="value">���֡��ַ�������һ����</param>
    /// <returns>��������ַ��� True�����򷵻� False</returns>
    public static bool IsNumeric(object value) {
        try {
            decimal dec = Convert.ToDecimal(value);
            return true;
        } catch {
            return false;
        }
    }

    /// <summary>
    /// ȷ��������Ĳ���תΪ Float ������ ,����0ʱ����false
    /// </summary>
    public static bool ToFloatNumeric(object value) {
        try {
            if (Convert.ToDouble(value.ToString()) == 0.0)//����۸�Ϊ0ʱ����false
            {
                return false;
            } else {
                float dec = Convert.ToSingle(value);
                return true;
            }
        } catch {
            return false;
        }
    }

    /// <summary>
    /// ��������ʾ��ҳ���ؼ���
    /// </summary>
    /// <param name="value">���������</param>
    /// <returns></returns>
    public static string OutNum(object value) {
        return OutNum(value, -1, 0, "", null, "", "");
    }

    /// <summary>
    /// ��������ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <returns></returns>
    public static string OutNum(object value, string replaceNull) {
        return OutNum(value, -1, 0, replaceNull, null, "", "");
    }

    /// <summary>
    /// ָ�����ֱ�����С��λ����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits) {
        return OutNum(value, decimalDigits, 0, "", null, "", "");
    }

    /// <summary>
    /// ָ�����ֱ�����С��λ����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, "", "");
    }

    /// <summary>
    /// ָ�����ֱ�����С��λ�������Ϻ�׺����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="postfix">��׺���磨��������234.56��������С����Ϊ��׺��</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull, string postfix) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, "", postfix);
    }

    /// <summary>
    /// ָ�����ֱ�����С��λ��������ǰ׺����׺����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="prefix">ǰ׺���磨��������234.56��������С���������Ϊǰ׺��</param>
    /// <param name="postfix">��׺���磨��������234.56��������С����Ϊ��׺��</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull, string prefix, string postfix) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, prefix, postfix);
    }

    /// <summary>
    /// �����ֳ���һ�����ʺ���ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="multiple">���ʣ��������� value ������Ч���� multiple ������ 0�������ء�value * multiple���Ľ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull) {
        return OutNum(value, decimalDigits, multiple, replaceNull, null, "", "");
    }

    /// <summary>
    /// �����ֳ���һ�����ʲ����Ϻ�׺����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="multiple">���ʣ��������� value ������Ч���� multiple ������ 0�������ء�value * multiple���Ľ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="postfix">��׺���磨��������234.56��������С����Ϊ��׺��</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull, string postfix) {
        return OutNum(value, decimalDigits, multiple, replaceNull, null, "", postfix);
    }

    /// <summary>
    /// ��������ʾ��ҳ���ؼ��ϡ��������������ǿջ�����������ֵ��� replaceValue ���趨ֵ���򷵻� replaceNull �ַ���
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="replaceValue">�滻��ֵ����� value ���ڸ�ֵ���򷵻� replaceNull �趨���ַ���</param>
    /// <param name="prefix">ǰ׺���磨��������234.56��������С���������Ϊǰ׺��</param>
    /// <param name="postfix">��׺���磨��������234.56��������С����Ϊ��׺��</param>
    /// <returns>�����Ŀ�������ʾ��ҳ���ؼ��ϵ��ַ���</returns>
    public static string OutNum(object value, string replaceNull, double? replaceValue, string prefix, string postfix) {
        return OutNum(value, 0, 0, replaceNull, replaceValue, prefix, postfix);
    }

    /// <summary>
    /// �����ֳ���һ�����ʲ�����ǰ׺����׺����ʾ��ҳ���ؼ��ϣ�����Ϊ�ٷֱ�������硰98.94%����
    /// </summary>
    /// <param name="value">���������</param>
    /// <param name="decimalDigits">������С��λ���������ֵΪ��-1���򲻴���С��λ�����ж���λ����ʾ����λ��</param>
    /// <param name="multiple">���ʣ��������� value ������Ч���� multiple ������ 0�������ء�value * multiple���Ľ��</param>
    /// <param name="replaceNull">�������Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="replaceValue">�滻��ֵ����� value ���ڸ�ֵ���򷵻� replaceNull �趨���ַ���</param>
    /// <param name="prefix">ǰ׺���磨��������234.56��������С���������Ϊǰ׺��</param>
    /// <param name="postfix">��׺���磨��������234.56��������С����Ϊ��׺��</param>
    /// <returns>�����Ŀ�������ʾ��ҳ���ؼ��ϵ��ַ���</returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull, double? replaceValue, string prefix, string postfix) {
        try {
            if (Convert.IsDBNull(value)) {
                return replaceNull;
            }

            decimal decValue = Convert.ToDecimal(value);

            if (null != replaceValue) {
                if (decValue == (decimal)replaceValue) {
                    return replaceNull;                                             //�滻��ֵ
                }
            }

            if (multiple != 0) {
                decValue = decValue * (decimal)multiple;
            }

            if (decValue % 1 == 0)                                                  //û��С��λ��
            {
                return prefix + (long)decValue + postfix;
            } else {
                if (decimalDigits < 0) {
                    return prefix + decValue + postfix;
                } else {
                    return prefix + Math.Round(decValue, decimalDigits) + postfix;
                }
            }
        } catch {
            return replaceNull;
        }
    }

    #endregion "���ֺ���"

    #region ��ѧ���㺯��

    /// <summary>
    /// ������� params ���󼯺�תΪ�ַ������飬���ж����� doNull ����
    /// </summary>
    private class ParamsToStringArray {
        private bool m_DoNull;

        public bool DoNull {
            get { return m_DoNull; }
            set { m_DoNull = value; }
        }

        private string[] m_Value;

        public string[] Value {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// ������� params ���󼯺�תΪ�ַ������飬���ж����� doNull ����
        /// </summary>
        /// <param name="doNull">�������Ĳ�����û����ȷָ������Ϊ��ʱ�Ƿ�ִ�����㣬���Դ˱�־���档</param>
        /// <param name="value">�������ֵ��ַ�������</param>
        public ParamsToStringArray(bool doNull, params object[] value) {
            int intLength = value.Length;
            this.DoNull = doNull;

            try {
                string strBoolean = Convert.ToString(value[value.Length - 1]).ToLower();
                if (strBoolean == "true" || strBoolean == "false") {
                    this.DoNull = Convert.ToBoolean(value[value.Length - 1]);
                    intLength = value.Length - 1;
                }
            } catch { }

            this.Value = new string[intLength];
            for (int intIndex = 0; intIndex < intLength; intIndex++) {
                try {
                    this.Value[intIndex] = Convert.ToString(value[intIndex]);
                } catch { }
            }
        }
    }

    /// <summary>
    /// �ɿ����͵�Ĭ��ֵ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int? Parse(int? value) {
        if (value != null) {
            return value;
        }

        return 0;
    }

    /// <summary>
    /// ������ĸ��������мӷ����� (Value(0) + Value(1) + Value(2))��������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ��Ӻ�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAdd(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoAdd(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ������мӷ����㡣(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��Ӻ�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAdd(string value, bool doNull) {
        return DoAdd(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// �������ڸ����ֽ��мӷ����㡣(Value(0) + Value(1) + Value(2))
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��Ӻ�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAdd(string[] value, bool doNull) {
        try {
            decimal decValue = 0;

            if (doNull) {
                bool bolValue = false;
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        bolValue = true;
                        decValue += ToDec(var);
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        decValue += ToDec(var);
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ������ĸ��������м������� (Value(0) - Value(1) - Value(2)) �õ�һ������������Ĳ�����������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoSub(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoSub(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ������м������㡣(Value1 - Value2 - Value3) �õ�һ������������Ĳ���
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoSub(string value, bool doNull) {
        return DoSub(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// �������ڸ����ֽ��м������㡣(Value(0) - Value(1) - Value(2)) �õ�һ������������Ĳ���
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoSub(string[] value, bool doNull) {
        try {
            decimal decValue = 0;

            if (doNull) {
                bool bolValue = false;
                for (int intIndex = 0; intIndex < value.Length; intIndex++) {
                    if (IsNumeric(value[intIndex])) {
                        bolValue = true;
                        if (intIndex == 0) {
                            decValue = ToDec(value[intIndex]);
                        } else {
                            decValue -= ToDec(value[intIndex]);
                        }
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                for (int intIndex = 0; intIndex < value.Length; intIndex++) {
                    if (IsNumeric(value[intIndex])) {
                        if (intIndex == 0) {
                            decValue = ToDec(value[intIndex]);
                        } else {
                            decValue -= ToDec(value[intIndex]);
                        }
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ������ĸ��������г˷����� (Value(0) * Value(1) * Value(2))��������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ��˺�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMul(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMul(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ������г˷����㡣(Value1 * Value2 * Value3)
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��˺�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMul(string value, bool doNull) {
        return DoMul(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// �������ڸ����ֽ��г˷����㡣(Value(0) * Value(1) * Value(2))
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��˺�����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMul(string[] value, bool doNull) {
        try {
            decimal decValue = 0;

            if (doNull) {
                bool bolValue = false;
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            decValue *= ToDec(var);
                        } else {
                            bolValue = true;
                            decValue = ToDec(var);
                        }
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                bool bolValue = false;
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            decValue *= ToDec(var);
                        } else {
                            bolValue = true;
                            decValue = ToDec(var);
                        }
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ������ĸ��������г������� (Value(0) / Value(1) / Value(2)) �õ�һ������������Ĳ�����������һ�������� bool ֵ������Ϊ doNull ����������� Value1��Value2 ��Щ��ĸ���֡�0��ֵ����������ֹͣ�����ؿ��ַ�����
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoDiv(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoDiv(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ������г������㡣(Value1 / Value2 / Value3) �õ�һ������������Ĳ�������� Value1��Value2 ��Щ��ĸ���֡�0��ֵ����������ֹͣ�����ؿ��ַ�����
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoDiv(string value, bool doNull) {
        return DoDiv(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// �������ڸ����ֽ��г������㡣(Value(0) / Value(1) / Value(2)) �õ�һ������������Ĳ�������� Value1��Value2 ��Щ��ĸ���֡�0��ֵ����������ֹͣ�����ؿ��ַ�����
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ���������������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoDiv(string[] value, bool doNull) {
        try {
            decimal decValue = 0;

            if (doNull) {
                bool bolValue = false;
                for (int intIndex = 0; intIndex < value.Length; intIndex++) {
                    if (IsNumeric(value[intIndex])) {
                        bolValue = true;
                        if (intIndex == 0) {
                            decValue = ToDec(value[intIndex]);
                        } else {
                            decValue /= ToDec(value[intIndex]);
                        }
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                for (int intIndex = 0; intIndex < value.Length; intIndex++) {
                    if (IsNumeric(value[intIndex])) {
                        if (intIndex == 0) {
                            decValue = ToDec(value[intIndex]);
                        } else {
                            decValue /= ToDec(value[intIndex]);
                        }
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ��������������������( (Value1 - Value2) / Value2 ) ( (���� - ȥ��) / ȥ�� )�����2��ֵ�����κ�һ��ֵ��Ч��������ֹͣ���ؿ��ַ�����
    /// </summary>
    /// <param name="value1">ֵ1��һ��Ϊ����</param>
    /// <param name="value2">ֵ2��һ��Ϊȥ��</param>
    /// <returns>���� Value1 �� Value2 �������ʣ��������������Ч���򷵻� "" �ַ�����</returns>
    public static string DoIncrease(object value1, object value2) {
        return DoIncrease(value1, value2, false);
    }

    /// <summary>
    /// ��������������������( (Value1 - Value2) / Value2 ) ( (���� - ȥ��) / ȥ�� )
    /// </summary>
    /// <param name="value1">ֵ1��һ��Ϊ����</param>
    /// <param name="value2">ֵ2��һ��Ϊȥ��</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>���� Value1 �� Value2 �������ʣ��������������Ч���򷵻� "" �ַ�����</returns>
    public static string DoIncrease(object value1, object value2, bool doNull) {
        try {
            bool bolValue1Flag = IsNumeric(value1);
            bool bolValue2Flag = IsNumeric(value2);

            if (doNull) {
                if (bolValue1Flag || bolValue2Flag) {
                    return Convert.ToString((ToDec(value1) - ToDec(value2)) / ToDec(value2));
                } else {
                    return "";
                }
            } else {
                if (bolValue1Flag && bolValue2Flag) {
                    return Convert.ToString((ToDec(value1) - ToDec(value2)) / ToDec(value2));
                } else {
                    return "";
                }
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ���㴫���������ƽ������������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ��ƽ�������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAvg(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoAvg(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ�������ƽ�������㡣(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��ƽ�������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAvg(string value, bool doNull) {
        return DoAvg(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// ���㴫���������ƽ������
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ��ƽ�������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoAvg(string[] value, bool doNull) {
        try {
            decimal decValue = 0;
            int intValueNum = 0;

            if (doNull) {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        intValueNum++;
                        decValue += ToDec(var);
                    }
                }
                if (intValueNum > 0) {
                    return (decValue / intValueNum).ToString();
                } else {
                    return "";
                }
            } else {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        intValueNum++;
                        decValue += ToDec(var);
                    } else {
                        return "";
                    }
                }
                return (decValue / intValueNum).ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ���㴫����������������������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMax(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMax(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ�������ȡ��������㡣(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMax(string value, bool doNull) {
        return DoMax(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// ���㴫����������������
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ����������������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMax(string[] value, bool doNull) {
        try {
            decimal decValue = 0;
            bool bolValue = false;

            if (doNull) {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            if (ToDec(var) > decValue) {
                                decValue = ToDec(var);
                            }
                        } else {
                            decValue = ToDec(var);
                            bolValue = true;
                        }
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            if (ToDec(var) > decValue) {
                                decValue = ToDec(var);
                            }
                        } else {
                            decValue = ToDec(var);
                            bolValue = true;
                        }
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ���㴫�����������С����������һ�������� bool ֵ������Ϊ doNull ��������
    /// </summary>
    /// <param name="value">�����ֲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
    /// <returns>������ֵ����С�����������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMin(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMin(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ�������ȡ��С�����㡣(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ����С�����������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMin(string value, bool doNull) {
        return DoMin(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// ���㴫�����������С����
    /// </summary>
    /// <param name="value">������ֵ�����</param>
    /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
    /// <returns>������ֵ����С�����������������Ч���򷵻� "" ���ַ�����</returns>
    public static string DoMin(string[] value, bool doNull) {
        try {
            decimal decValue = 0;
            bool bolValue = false;

            if (doNull) {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            if (ToDec(var) < decValue) {
                                decValue = ToDec(var);
                            }
                        } else {
                            decValue = ToDec(var);
                            bolValue = true;
                        }
                    }
                }
                if (bolValue) {
                    return decValue.ToString();
                } else {
                    return "";
                }
            } else {
                foreach (string var in value) {
                    if (IsNumeric(var)) {
                        if (bolValue) {
                            if (ToDec(var) < decValue) {
                                decValue = ToDec(var);
                            }
                        } else {
                            decValue = ToDec(var);
                            bolValue = true;
                        }
                    } else {
                        return "";
                    }
                }
                return decValue.ToString();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// �Դ�������ݽ��������С��ƽ������Ȳ�����ʵȵ����ݷ�����
    /// </summary>
    public class DoAnalysis {

        /// <summary>
        /// �Դ�������ݽ��������С��ƽ������Ȳ�����ʵȵ����ݷ������� Value �� DoNull ��ֵ������ Do ������ʼ���ݷ�����
        /// </summary>
        public DoAnalysis() {
        }

        /// <summary>
        /// �Դ�������ݽ��������С��ƽ������Ȳ�����ʵȵ����ݷ�����������һ�������� bool ֵ������Ϊ doNull ��������
        /// </summary>
        /// <param name="value">��Ҫ���������ݲ�����������һ�������� bool ֵ������Ϊ doNull ��������</param>
        /// <returns></returns>
        public DoAnalysis(params object[] value) {
            ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
            this.Value = ptsa.Value;
            this.DoNull = ptsa.DoNull;
            this.Do();
        }

        /// <summary>
        /// ���������ԡ�,���� " " �ո�ָ����ӳɵ��ַ������������С��ƽ������Ȳ�����ʵȵ����ݷ�����
        /// </summary>
        /// <param name="value">�ԡ�,�����Ż� " " �ո�ָ����ַ���</param>
        /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
        public DoAnalysis(string value, bool doNull) {
            this.Value = value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            this.DoNull = doNull;
            this.Do();
        }

        /// <summary>
        /// �Դ�����ַ����������ݽ��������С��ƽ������Ȳ�����ʵȵ����ݷ�����
        /// </summary>
        /// <param name="value">������ֵ��ַ�������</param>
        /// <param name="doNull">��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����</param>
        public DoAnalysis(string[] value, bool doNull) {
            this.Value = value;
            this.DoNull = doNull;
            this.Do();
        }

        /// <summary>
        /// ��ʼ�������ݷ���������ʼ�� DoAnalysis ��������κβ���ʱ����Ҫ�ֹ��� Value �� DoNull ��ֵ������ Do ������ʼ���ݷ����������ʼ���ಢ������������Զ����ô˷�����
        /// </summary>
        public void Do() {
            this.Availability = false;
            this.MaxIndex = -1;
            this.MinIndex = -1;

            try {
                int intIndex;
                decimal decValueMax = 0;                                    //���ֵ
                decimal decValueMin = 0;                                    //��Сֵ
                decimal decValueSum = 0;                                    //�ϼ�ֵ

                this.Count = Value.Length;

                for (intIndex = 0; intIndex < this.Count; intIndex++) {
                    if (IsNumeric(this.Value[intIndex])) {
                        decimal decValue = ToDec(this.Value[intIndex]);

                        if (this.Availability == false) {
                            decValueMax = decValue;
                            decValueMin = decValue;
                            decValueSum = decValue;
                            this.Availability = true;
                            this.MaxIndex = intIndex;
                            this.MinIndex = intIndex;
                        } else {
                            if (decValue > decValueMax) {
                                decValueMax = decValue;
                                this.MaxIndex = intIndex;
                            }

                            if (decValue < decValueMin) {
                                decValueMin = decValue;
                                this.MinIndex = intIndex;
                            }

                            decValueSum += decValue;
                        }
                        this.CountValid++;
                    } else {
                        if (this.DoNull == false) {
                            throw new System.Exception();
                        }
                    }
                }

                if (this.Availability) {
                    this.MaxValue = decValueMax.ToString();                                             //���ֵ
                    this.MinValue = decValueMin.ToString();                                             //��Сֵ
                    this.SumValue = decValueSum.ToString();                                             //�ϼ�ֵ
                    this.AvgValue = Convert.ToString(decValueSum / this.CountValid);                    //ƽ��ֵ
                    this.Loadfact = Convert.ToString((decValueSum / this.CountValid) / decValueMax);    //������
                    this.MaxMinDiff = Convert.ToString(decValueMax - decValueMin);                      //��Ȳ�
                    this.MaxMinRate = Convert.ToString((decValueMax - decValueMin) / decValueMax);      //��Ȳ���

                    decimal decTempValueMax = decValueMax;
                    decimal decTempValueMin = decValueMin;
                    decimal decDiscrepant = (decValueMax - decValueMin) / 10;                           //�����Сֵ��������ķ�Χ
                    decimal decDiscrepantMax = decValueMax + decDiscrepant;
                    decimal decDiscrepantMin = decValueMin - decDiscrepant;
                    decimal decAddValue = 1;                                                            //����ÿ�γ������ӵ�ֵ�������С����Ӧ�ô���Сλ����ʼ

                    while ((decTempValueMax % 1) != 0) {
                        decTempValueMax *= 10;
                        decAddValue /= 10;
                    }
                    while ((decValueMax + decAddValue) < decDiscrepantMax)                              //������ֵ��������ֵС����������ֵ
                    {
                        decValueMax = (int)(decValueMax / decAddValue) * decAddValue;                   //������ֵ������Ĳ��ָ�Ϊ0
                        decTempValueMax = decValueMax + decAddValue;
                        decAddValue = decAddValue * 10;
                    }
                    this.MaxLimit = decTempValueMax.ToString();

                    decAddValue = 1;                                                                    //����ÿ�γ��Լ��ٵ�ֵ�������С����Ӧ�ô���Сλ����ʼ
                    while ((decTempValueMin % 1) != 0) {
                        decTempValueMin *= 10;
                        decAddValue /= 10;
                    }
                    while ((decValueMin - decAddValue) > decDiscrepantMin)                              //������ֵ��������ֵС����������ֵ
                    {
                        decValueMin = (int)(decValueMin / decAddValue) * decAddValue;                   //������ֵ������Ĳ��ָ�Ϊ0
                        decTempValueMin = decValueMin - decAddValue;
                        decAddValue = decAddValue * 10;
                    }
                    this.MinLimit = decTempValueMin.ToString();

                    if (decTempValueMin < 0 && decValueMax >= 0 && decValueMin >= 0) {
                        this.MinLimit = "0";
                    }

                    if (decValueMax == decValueMin) {
                        this.MaxValue = Convert.ToString(decValueMax + decValueMax * Convert.ToDecimal(0.1));
                        this.MinValue = Convert.ToString(decValueMin - decValueMin * Convert.ToDecimal(0.1));
                    }
                }
            } catch {
                this.Availability = false;
                this.AvgValue = "";
                this.CountValid = 0;
                this.Loadfact = "";
                this.MaxIndex = -1;
                this.MaxLimit = "";
                this.MaxMinDiff = "";
                this.MaxMinRate = "";
                this.MaxValue = "";
                this.MinIndex = -1;
                this.MinLimit = "";
                this.MinValue = "";
                this.SumValue = "";
            }
        }

        private string[] m_Value;

        /// <summary>
        /// �������ݷ����������ַ�������
        /// </summary>
        public string[] Value {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private bool m_DoNull;

        /// <summary>
        /// ��������ĳ��ֵ�� DBNull ���� "" ���߲������֣��������Ƿ���Ч��True��������Ч�����㽫�������У����Ӵ���Ч����Ϊ��0����ֻ�е����в��������ֵ���������֣����߽���ֵ��Ϊ��0���������������ʱ���ŷ��� "" ���ַ�����False��������Ч��ֻҪһ�����������ֵ�ֵ������������ "" �ַ�����
        /// </summary>
        public bool DoNull {
            get { return m_DoNull; }
            set { m_DoNull = value; }
        }

        private bool m_Availability;

        /// <summary>
        /// ���صĽ���Ƿ���Ч��True��������Ч��False��������Ч��
        /// </summary>
        public bool Availability {
            get { return m_Availability; }
            set { m_Availability = value; }
        }

        private string m_MaxValue;

        /// <summary>
        /// ����һ����ֵ�е����ֵ
        /// </summary>
        public string MaxValue {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        private string m_MinValue;

        /// <summary>
        /// ����һ����ֵ�е���Сֵ
        /// </summary>
        public string MinValue {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        private string m_MaxLimit;

        /// <summary>
        /// ����һ����ֵ�е����ֵ�ı߽�ֵ���������ֵ��Ҫ��Ľӽ�������ֵ��
        /// </summary>
        public string MaxLimit {
            get { return m_MaxLimit; }
            set { m_MaxLimit = value; }
        }

        private string m_MinLimit;

        /// <summary>
        /// ����һ����ֵ�е���Сֵ�ı߽�ֵ������Сֵ��ҪС�Ľӽ�������ֵ��
        /// </summary>
        public string MinLimit {
            get { return m_MinLimit; }
            set { m_MinLimit = value; }
        }

        private int m_MaxIndex;

        /// <summary>
        /// ���ֵ�������е�λ�ã���0��ʼ������ Availability Ϊ False ʱ����ֵ��ЧΪ��-1����
        /// </summary>
        public int MaxIndex {
            get { return m_MaxIndex; }
            set { m_MaxIndex = value; }
        }

        private int m_MinIndex;

        /// <summary>
        /// ��Сֵ�������е�λ�ã���0��ʼ������ Availability Ϊ False ʱ����ֵ��ЧΪ��-1����
        /// </summary>
        public int MinIndex {
            get { return m_MinIndex; }
            set { m_MinIndex = value; }
        }

        private string m_SumValue;

        /// <summary>
        /// ����һ����ֵ�еĺϼ�ֵ��
        /// </summary>
        public string SumValue {
            get { return m_SumValue; }
            set { m_SumValue = value; }
        }

        private string m_AvgValue;

        /// <summary>
        /// ����һ����ֵ�е�ƽ��ֵ��
        /// </summary>
        public string AvgValue {
            get { return m_AvgValue; }
            set { m_AvgValue = value; }
        }

        private string m_MaxMinDiff;

        /// <summary>
        /// ���ֵ����Сֵ�Ĳ�ֵ����������Ȳ
        /// </summary>
        public string MaxMinDiff {
            get { return m_MaxMinDiff; }
            set { m_MaxMinDiff = value; }
        }

        private string m_MaxMinRate;

        /// <summary>
        /// ���ֵ����Сֵ�Ĳ�ֵ���ʡ���������Ȳ��ʣ����㷽������Ȳ� / ��ֵ��
        /// </summary>
        public string MaxMinRate {
            get { return m_MaxMinRate; }
            set { m_MaxMinRate = value; }
        }

        private string m_Loadfact;

        /// <summary>
        /// �����ʣ����㷽����ƽ��ֵ / ���ֵ��
        /// </summary>
        public string Loadfact {
            get { return m_Loadfact; }
            set { m_Loadfact = value; }
        }

        private int m_Count;

        /// <summary>
        /// ���ش����һ�����֡��������ַ������ֵĸ������� Availability Ϊ False ʱ����ֵ��Ч��
        /// </summary>
        public int Count {
            get { return m_Count; }
            set { m_Count = value; }
        }

        private int m_CountValid;

        /// <summary>
        /// ����һ����ֵ�����ֵĸ������ı���"" �ա�Null�Ȳ���תΪ���ֵĺ��Բ��ƣ����� Availability Ϊ False ʱ����ֵ��Ч��
        /// </summary>
        public int CountValid {
            get { return m_CountValid; }
            set { m_CountValid = value; }
        }
    }

    #endregion ��ѧ���㺯��
    
    #region ͳ�Ʋ�ѯ����

    public static string AreaIdWhere(string strAreaId) {
        try {
            int _intAreaid = SysFun.ToInt(strAreaId), _intAreaIdMax = 0;
            string _strWhere = "";
            if (_intAreaid % 10000 == 0) //ʡ��
            {
                _intAreaIdMax = _intAreaid + 10000;
                _strWhere += " (areaid>=" + _intAreaid.ToString() + " And AreaId< " + _intAreaIdMax.ToString() + ") ";
            } else if (_intAreaid % 100 == 0) {
                _intAreaIdMax = _intAreaid + 100;
                _strWhere += " (areaid>=" + _intAreaid.ToString() + " And AreaId< " + _intAreaIdMax.ToString() + ") ";
            } else {
                _strWhere += "   areaid=" + _intAreaid.ToString() + " ";
            }
            //if (_intAreaid != 410000)
            //{
            //    if (_intAreaid % 10000 > 0)
            //    {
            //        _strWhere += "   areaid=" + _intAreaid.ToString() + " and";
            //    }
            //    else
            //    {
            //        _intAreaIdMax = _intAreaid + 100;
            //        _strWhere += " areaid>=" + _intAreaid.ToString() + " And AreaId< " + _intAreaIdMax.ToString() + " and";
            //    }
            //}
            return _strWhere;
        } catch {
            return "";
        }
    }

    public static string AreaIdWhere1(string strAreaId) {
        try {
            int _intAreaid = SysFun.ToInt(strAreaId), _intAreaIdMax = 0;
            string _strWhere = "";
            if (_intAreaid != 410000) {
                if (_intAreaid % 100 > 0) {
                    _strWhere += "   Hospital.areaid=" + _intAreaid.ToString() + " and";
                } else {
                    _intAreaIdMax = _intAreaid + 100;
                    _strWhere += " Hospital.areaid>=" + _intAreaid.ToString() + " And Hospital.AreaId< " + _intAreaIdMax.ToString() + " and";
                }
            }
            return _strWhere;
        } catch {
            return "";
        }
    }

    public static string AreaIdWhere2(string strAreaId) {
        try {
            int _intAreaid = SysFun.ToInt(strAreaId), _intAreaIdMax = 0;
            string _strWhere = "";
            if (_intAreaid != 410000) {
                if (_intAreaid % 100 > 0) {
                    _strWhere += "   tra_GoodsId.areaid=" + _intAreaid.ToString() + " and";
                } else {
                    _intAreaIdMax = _intAreaid + 100;
                    _strWhere += " tra_GoodsId.areaid>=" + _intAreaid.ToString() + " And tra_GoodsId.AreaId< " + _intAreaIdMax.ToString() + " and";
                }
            }
            return _strWhere;
        } catch {
            return "";
        }
    }

    public static string AreaIdWhere3(string strAreaId) {
        try {
            int _intAreaid = SysFun.ToInt(strAreaId), _intAreaIdMax = 0;
            string _strWhere = "";
            if (_intAreaid != 410000) {
                if (_intAreaid % 100 > 0) {
                    _strWhere += "   traPayment.areaid=" + _intAreaid.ToString() + " and";
                } else {
                    _intAreaIdMax = _intAreaid + 100;
                    _strWhere += " traPayment.areaid>=" + _intAreaid.ToString() + " And traPayment.AreaId< " + _intAreaIdMax.ToString() + " and";
                }
            }
            return _strWhere;
        } catch {
            return "";
        }
    }

    #endregion ͳ�Ʋ�ѯ����

    #region �ַ�������

    /// <summary>
    /// �ж��Ƿ����ִ��
    /// </summary>
    /// <param name="_strCheckUp"></param>
    /// <returns></returns>
    public static Boolean CheckUp(string _strCheckUp) {
        try {
            //anoia
            string _str1 = _strCheckUp.Substring(0, 13);
            if (_str1 == "Fzr5ghsdw6K8N") {
                return true;
            } else {
                return false;
            }
        } catch {
            return false;
        }
    }

    /// <summary>
    /// ȷ��������Ķ���תΪ�ַ���������ȥ��ͷ�ո�
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToTrim(object value) {
        try {
            if (value == null) {
                return "";
            } else {
                return Convert.ToString(value).Trim();
            }
        } catch {
            return "";
        }
    }

    /// <summary>
    /// �ַ����Ƚϡ����2���ַ���һ���򷵻ء�true��������һ�������ݻ���2���ַ�������0�����ַ������߶��� null ֵ���Ƚ�ʱ�������ַ�����ͷ�Ŀո񣩣����򷵻ء�false��
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool StringEquals(object value1, object value2) {
        try {
            if (value1 == null && value2 == null) {
                return true;
            } else if (Convert.ToString(value1).Trim() == Convert.ToString(value2).Trim()) {
                return true;
            } else {
                return false;
            }
        } catch {
            return false;
        }
    }

    /// <summary>
    /// ���㴫����ַ������ֽڳ���
    /// </summary>
    /// <param name="value">�������ֽڳ��ȵ��ַ���</param>
    /// <returns></returns>
    public static int GetByteLength(object value) {
        int intLength = 0;
        try {
            char[] charValue = Convert.ToString(value).ToCharArray();

            for (int intIndex = 0; intIndex < charValue.Length; intIndex++) {
                byte[] bytChar = System.Text.Encoding.Default.GetBytes(charValue, intIndex, 1);
                intLength += bytChar.Length;
            }
        } catch {
        }
        return intLength;
    }

    /// <summary>
    /// ������ĺ���תΪ����ƴ������ĸ���ӵ��ַ���
    /// </summary>
    /// <param name="value">��ת�ɺ���ƴ������ĸ�ĺ����ַ���</param>
    /// <param name="isCapital">�Ƿ��д��true����д��false��Сд</param>
    /// <param name="notChineseDisplay">�����ĺ����Ƿ���ʾ��true����ʾ��false������ʾ</param>
    /// <param name="replaceNotChineseDisplay"></param>
    /// <returns></returns>
    public static string GetChineseSpell(object value, bool isCapital, bool notChineseDisplay) {
        string strSpell = "";
        try {
            char[] charValue = Convert.ToString(value).ToCharArray();

            for (int intIndex = 0; intIndex < charValue.Length; intIndex++) {
                byte[] bytChar = System.Text.Encoding.Default.GetBytes(charValue, intIndex, 1);

                if (bytChar.Length > 1) {
                    //��ʱ�������Ĵ����ٺúÿ���
                    int intChar0 = (short)bytChar[0];
                    int intChar1 = (short)bytChar[1];
                    int intCode = (intChar0 << 8) + intChar1;
                    int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                    for (int i = 0; i < 26; i++) {
                        int max = 55290;
                        if (i != 25) {
                            max = areacode[i + 1];
                        }

                        if (areacode[i] <= intCode && intCode < max) {
                            if (isCapital) {
                                strSpell += Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                            } else {
                                strSpell += Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                            }
                            break;
                        }
                    }
                } else {
                    if (notChineseDisplay) {
                        strSpell += charValue[intIndex];
                    }
                }
            }
            return strSpell;
        } catch {
            return strSpell;
        }
    }

    /// <summary>
    /// �ַ�����ȡ�����
    /// </summary>
    /// <param name="value">��������ַ���</param>
    /// <param name="interceptLength">��ȡ���ȡ����ص��ַ����ֽڳ���һ����������ֵ�������ֵΪ��0���򲻽��н�ȡ����</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int interceptLength) {
        return StringInterceptFill(value, interceptLength, 0, "", false);
    }

    /// <summary>
    /// �ַ�����ȡ�����
    /// </summary>
    /// <param name="value">��������ַ���</param>
    /// <param name="fillLength">��䳤�ȡ����ַ������ֽڳ���û�дﵽ��ֵ��ʹ�� fillValue ������䣬ʹ�����ַ������ֽڳ���С�ڵ���ָ������䳤�ȡ������ֵΪ��0���򲻽���������</param>
    /// <param name="fillValue">���ֵ�������ֵΪ��ʱ��ʹ�ÿո����</param>
    /// <param name="fillRight">���λ�á�true��������ַ������Ҳࣻ false��������ַ��������</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int fillLength, string fillValue, bool fillRight) {
        return StringInterceptFill(value, 0, fillLength, fillValue, fillRight);
    }

    /// <summary>
    /// �ַ�����ȡ����䡣�ú�������ִ�н�ȡ������Ȼ����ִ����������
    /// </summary>
    /// <param name="value">��������ַ���</param>
    /// <param name="interceptLength">��ȡ���ȡ����ص��ַ����ֽڳ���һ����������ֵ�������ֵΪ��0���򲻽��н�ȡ����</param>
    /// <param name="fillLength">��䳤�ȡ����ַ������ֽڳ���û�дﵽ��ֵ��ʹ�� fillValue ������䣬ʹ�����ַ������ֽڳ���С�ڵ���ָ������䳤�ȡ������ֵΪ��0���򲻽���������</param>
    /// <param name="fillValue">���ֵ�������ֵΪ��ʱ��ʹ�ÿո����</param>
    /// <param name="fillRight">���λ�á�true��������ַ������Ҳࣻ false��������ַ��������</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int interceptLength, int fillLength, string fillValue, bool fillRight) {
        string strVaue = "";
        try {
            if (value == null) {
                strVaue = "";
            } else {
                strVaue = Convert.ToString(value);
            }

            //��ȡ����
            if (interceptLength > 0) {
                int intLength = 0;
                string strInterceptVaue = "";

                for (int intIndex = 0; intIndex < strVaue.Length; intIndex++) {
                    string strChar = strVaue.Substring(intIndex, 1);
                    int intCharLength = Encoding.Default.GetBytes(strChar).Length;
                    if ((intLength + intCharLength) <= interceptLength) {
                        strInterceptVaue += strChar;
                        intLength += intCharLength;
                    } else {
                        break;
                    }
                }

                strVaue = strInterceptVaue;
            }

            //������
            if (fillLength > 0) {
                int intLength = 0;

                for (int intIndex = 0; intIndex < strVaue.Length; intIndex++) {
                    intLength += Encoding.Default.GetBytes(strVaue.Substring(intIndex, 1)).Length;
                }

                if (fillValue == null) {
                    fillValue = " ";
                } else if (fillValue.Length == 0) {
                    fillValue = " ";
                }

                int intFillValueLength = GetByteLength(fillValue);

                while ((intLength + intFillValueLength) <= fillLength) {
                    if (fillRight) {
                        strVaue = strVaue + fillValue;
                    } else {
                        strVaue = fillValue + strVaue;
                    }
                    intLength += intFillValueLength;
                }
            }
        } catch {
        }
        return strVaue;
    }

    /// <summary>
    /// ����תΪ���ֺ����ʽ
    /// </summary>
    public enum CharacterStyle {

        /// <summary>
        /// ���ֵġ�һ������
        /// </summary>
        Character = 0,

        /// <summary>
        /// ��д�ġ�Ҽ������
        /// </summary>
        Capitalization = 1
    }

    public static string ToCharacter(string value) {
        return ToCharacter(value, CharacterStyle.Character);
    }

    public static string ToCharacter(int value, CharacterStyle characterStyle) {
        return ToCharacter(value.ToString(), CharacterStyle.Character);
    }

    public static string ToCharacter(object value, CharacterStyle characterStyle) {
        if (IsNumeric(value)) {
            string strValue = "";
            string[] strChar = new string[0];

            switch (characterStyle) {
                case CharacterStyle.Character:
                    strChar = new string[] { "��", "һ", "��", "��", "��", "��", "��", "��", "��", "��" };
                    break;

                case CharacterStyle.Capitalization:
                    strChar = new string[] { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
                    break;
            }

            foreach (System.Char chrValue in (string)value) {
                if (chrValue == 45) {
                    strValue += "��";
                } else if (chrValue == 46) {
                    strValue += "��";
                } else {
                    strValue += strChar[chrValue - 48];
                }
            }

            return strValue;
        } else {
            return "";
        }
    }

    /// <summary>
    /// **********�ú���δ����**********�����������תΪ26������Ӣ����ĸ��ʾ���ַ�������1��ʼ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string IntToAlphabet(int value) {
        //**********�ú���δ����**********
        string[] strAlphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string strValue = "";
        int intValue = value + 1;

        if ((intValue % 26) > 0) {
            strValue = strAlphabet[(intValue % 26) - 1];
        } else {
            strValue = "Z";
        }

        if ((intValue / 26) > 0) {
            strValue += strAlphabet[(intValue / 26) - 1];
        }
        return strValue;
    }

    /// <summary>
    /// ����ַ���ҳ���ؼ���
    /// </summary>
    /// <param name="value">������ַ���</param>
    /// <returns>�������ַ���</returns>
    public static string OutStr(object value) {
        return OutStr(value, "", "", "");
    }

    /// <summary>
    /// ����ַ���ҳ���ؼ���
    /// </summary>
    /// <param name="value">������ַ���</param>
    /// <param name="replaceNull">���������ַ���Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <returns>�������ַ���</returns>
    public static string OutStr(object value, string replaceNull) {
        return OutStr(value, replaceNull, "", "");
    }

    /// <summary>
    /// ����ַ���ҳ���ؼ���
    /// </summary>
    /// <param name="value">������ַ���</param>
    /// <param name="replaceNull">���������ַ���Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�������ַ���</returns>
    public static string OutStr(object value, string replaceNull, string postfix) {
        return OutStr(value, replaceNull, "", postfix);
    }

    /// <summary>
    /// ����ַ���ҳ���ؼ���
    /// </summary>
    /// <param name="value">������ַ���</param>
    /// <param name="replaceNull">���������ַ���Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�������ַ���</returns>
    public static string OutStr(object value, string replaceNull, string prefix, string postfix) {
        try {
            if (value == null) {
                return replaceNull;
            } else {
                if (Convert.ToString(value).TrimEnd(new char[] { ' ', '��', '\t' }).Length > 0) {
                    return prefix + Convert.ToString(value).TrimEnd(new Char[] { ' ', '��', '\t' }) + postfix;
                } else {
                    return replaceNull;
                }
            }
        } catch {
            return replaceNull;
        }
    }

    /// <summary>
    /// ��������ַ���תΪ�ʺ� HTML ҳ����ʾ������
    /// </summary>
    /// <param name="value">HTML ����</param>
    /// <returns></returns>
    public static string OutHtml(object value) {
        return OutHtml(value, "", "", "");
    }

    /// <summary>
    /// ��������ַ���תΪ�ʺ� HTML ҳ����ʾ������
    /// </summary>
    /// <param name="value">HTML ����</param>
    /// <param name="replaceNull">���������ַ���Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <returns></returns>
    public static string OutHtml(object value, string replaceNull) {
        return OutHtml(value, replaceNull, "", "");
    }

    /// <summary>
    /// ��������ַ�������ǰ׺����׺��תΪ�ʺ� HTML ҳ����ʾ������
    /// </summary>
    /// <param name="value">HTML ����</param>
    /// <param name="replaceNull">���������ַ���Ϊ DBNull �򡰿ա�ʱ���ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns></returns>
    public static string OutHtml(object value, string replaceNull, string prefix, string postfix) {
        try {
            if (value == null) {
                return replaceNull;
            } else {
                string strValue = Convert.ToString(value).TrimEnd(new Char[] { ' ' });
                strValue = strValue.Replace("&", "&amp;");
                strValue = strValue.Replace("<", "&lt;");
                strValue = strValue.Replace(">", "&gt;");
                strValue = strValue.Replace("\"", "&quot;");
                strValue = strValue.Replace(" ", "&nbsp;");
                strValue = strValue.Replace("\r", "<br>\r");
                return prefix + strValue + postfix;
            }
        } catch {
            return replaceNull;
        }
    }

    /// <summary>
    /// �Ƴ����� HTML ��ǡ����硰&lt;p align=&quot;center&quot;&gt;����&lt;/p&gt;�������ء����С�
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string RemoveHTML(object value) {
        try {
            string strHtml = Convert.ToString(value);
            strHtml = System.Text.RegularExpressions.Regex.Replace(strHtml, "<(.|\n)+?>", "");
            strHtml = strHtml.Replace("&nbsp;", " ");
            return strHtml;
        } catch {
            return "";
        }
    }

    /// <summary>
    /// �� URL �ַ������б��롣
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string UrlEncode(object value) {
        try {
            string strUrl = Convert.ToString(value);
            strUrl = strUrl.Replace("%", "%25");
            strUrl = strUrl.Replace("`", "%60");
            strUrl = strUrl.Replace("~", "%7e");
            strUrl = strUrl.Replace("@", "%40");
            strUrl = strUrl.Replace("#", "%23");
            strUrl = strUrl.Replace("$", "%24");
            strUrl = strUrl.Replace("^", "%5e");
            strUrl = strUrl.Replace("&", "%26");
            strUrl = strUrl.Replace("=", "%3d");
            strUrl = strUrl.Replace("+", "%2b");
            strUrl = strUrl.Replace("[", "%5b");
            strUrl = strUrl.Replace("]", "%5d");
            strUrl = strUrl.Replace("{", "%7b");
            strUrl = strUrl.Replace("}", "%7d");
            strUrl = strUrl.Replace(";", "%3b");
            strUrl = strUrl.Replace(":", "%3a");
            strUrl = strUrl.Replace("\"", "%22");
            strUrl = strUrl.Replace(",", "%2c");
            strUrl = strUrl.Replace("<", "%3c");
            strUrl = strUrl.Replace(">", "%3e");
            strUrl = strUrl.Replace("/", "%2f");
            strUrl = strUrl.Replace("?", "%3f");

            return strUrl;
        } catch {
            return "";
        }
    }

    /// <summary>
    /// �� URL �ַ������н���
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string UrlDecode(object value) {
        try {
            string strUrl = Convert.ToString(value);
            strUrl = strUrl.Replace("%3F", "?");
            strUrl = strUrl.Replace("%3f", "?");
            strUrl = strUrl.Replace("%2F", "/");
            strUrl = strUrl.Replace("%2f", "/");
            strUrl = strUrl.Replace("%3E", ">");
            strUrl = strUrl.Replace("%3e", ">");
            strUrl = strUrl.Replace("%3C", "<");
            strUrl = strUrl.Replace("%3c", "<");
            strUrl = strUrl.Replace("%2C", ",");
            strUrl = strUrl.Replace("%2c", ",");
            strUrl = strUrl.Replace("%22", "\"");
            strUrl = strUrl.Replace("%3A", ":");
            strUrl = strUrl.Replace("%3a", ":");
            strUrl = strUrl.Replace("%3B", ";");
            strUrl = strUrl.Replace("%3b", ";");
            strUrl = strUrl.Replace("%7D", "}");
            strUrl = strUrl.Replace("%7d", "}");
            strUrl = strUrl.Replace("%7B", "{");
            strUrl = strUrl.Replace("%7b", "{");
            strUrl = strUrl.Replace("%5D", "]");
            strUrl = strUrl.Replace("%5d", "]");
            strUrl = strUrl.Replace("%5B", "[");
            strUrl = strUrl.Replace("%5b", "[");
            strUrl = strUrl.Replace("%2B", "+");
            strUrl = strUrl.Replace("%2b", "+");
            strUrl = strUrl.Replace("%3D", "=");
            strUrl = strUrl.Replace("%3d", "=");
            strUrl = strUrl.Replace("%26", "&");
            strUrl = strUrl.Replace("%5E", "^");
            strUrl = strUrl.Replace("%5e", "^");
            strUrl = strUrl.Replace("%24", "$");
            strUrl = strUrl.Replace("%23", "#");
            strUrl = strUrl.Replace("%40", "@");
            strUrl = strUrl.Replace("%7E", "~");
            strUrl = strUrl.Replace("%7e", "~");
            strUrl = strUrl.Replace("%60", "`");
            strUrl = strUrl.Replace("%25", "%");

            return strUrl;
        } catch {
            return "";
        }
    }

    /// <summary>
    /// ��ȡ HTML ���������ֵ�����硰&lt;input name='key' value='123'&gt;��ָ����������Ϊ��value�����Ի�÷���ֵ��123��
    /// </summary>
    /// <param name="htmlTag">һ�� HTML �����������ǩ</param>
    /// <param name="attributeName">����ȡ����������</param>
    /// <returns></returns>
    public static string GetHtmlTagAttributeValue(object htmlTag, string attributeName) {
        try {
            string value = Convert.ToString(htmlTag);

            System.Text.RegularExpressions.Match matSign = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //����ֵ���õ����š�˫���Ż�û��������������

            string strSign = matSign.Value.Substring(matSign.Value.Length - 1, 1);                              //���ҡ�value = ������ĵ�һ���ַ�
            if (strSign == "\"" || strSign == "\'") {
                System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                int intStartIndex = matValue.Value.IndexOf(strSign) + 1;
                return matValue.Value.Substring(intStartIndex, matValue.Value.LastIndexOf(strSign) - intStartIndex);
            } else {
                System.Text.RegularExpressions.Match matState = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);       //ȡǰ���ַ����Ա��滻�����ݿ�ͷ�Ĳ���
                System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*\\S*\\s*?[^>]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return matValue.Value.Substring(matState.Length).TrimEnd();
            }
        } catch {
            return "";
        }
    }

    #endregion �ַ�������

    #region SQL ����

    /// <summary>
    /// �����ַ�����Ϊ���ݿ�� Insert �� UpDate ��׼��
    /// </summary>
    /// <param name="value">׼�������ַ����ֶε��ַ���</param>
    /// <returns></returns>
    public static string SqlStr(object value) {
        return SqlStr(value, 0, null);
    }

    /// <summary>
    /// �����ַ�������ָ�����Ƚ��н�ȡ��Ϊ���ݿ�� Insert �� UpDate ��׼��
    /// </summary>
    /// <param name="value">׼�������ַ����ֶε��ַ���</param>
    /// <param name="length">���ַ������޶����ȣ��������Ϊ��0�����ʾ���޶����ȣ���������ַ������ֽڳ��ȳ�����ֵ����н�ȡ��</param>
    /// <returns></returns>
    public static string SqlStr(object value, int length) {
        return SqlStr(value, length, null);
    }

    /// <summary>
    /// �����ַ�������� value ����Ϊ����ʹ�� replaceNull �������Ϊ���ݿ�� Insert �� UpDate ��׼��
    /// </summary>
    /// <param name="value">׼�������ַ����ֶε��ַ���</param>
    /// <param name="replaceNull">���׼���������ݿ���ַ����ǿգ���ʹ�ô�ֵ���</param>
    /// <returns></returns>
    public static string SqlStr(object value, string replaceNull) {
        return SqlStr(value, 0, replaceNull);
    }

    /// <summary>
    /// �����ַ�������ָ�����Ƚ��н�ȡ����� value ����Ϊ����ʹ�� replaceNull �������Ϊ���ݿ�� Insert �� UpDate ��׼��
    /// </summary>
    /// <param name="value">׼�������ַ����ֶε��ַ���</param>
    /// <param name="length">���ַ������޶����ȣ��������Ϊ��0�����ʾ���޶����ȣ���������ַ������ֽڳ��ȳ�����ֵ����н�ȡ��</param>
    /// <param name="replaceNull">���׼���������ݿ���ַ����ǿգ���ʹ�ô�ֵ���</param>
    /// <returns></returns>
    public static string SqlStr(object value, int length, string replaceNull) {
        try {
            if (value != null) {
                string strSql = Convert.ToString(value);
                if (strSql.Trim().Length > 0) {
                    if (length == 0) {
                        return "'" + strSql.Replace("'", "''") + "'";
                    } else {
                        return "'" + StringInterceptFill(strSql, length).Replace("'", "''") + "'";
                    }
                }
            }
        } catch {
        }
        if (replaceNull == null) {
            return "Null";
        } else {
            return "'" + replaceNull + "'";
        }
    }

    /// <summary>
    /// ��ʽ������Ϊ Int �ͣ������Ա��ʣ��������������Ч�ԡ�Null�������ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <returns></returns>
    public static string SqlNum(object value) {
        return SqlNum(value, 0, SqlNumericType.Int, 0, 0, null);
    }

    /// <summary>
    /// ��ʽ������Ϊ Int �Ͳ��������ֳ���һ�����ʣ��������������Ч�ԡ�Null�������ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="multiple">���ʣ���������������Чʱ�� value ���Դ����֡�����˲������롰0�����ʾ�����б������㡣</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple) {
        return SqlNum(value, multiple, SqlNumericType.Int, 0, 0, null);
    }

    /// <summary>
    /// ��ʽ�����ֲ��������ֳ���һ�����ʣ��������������Ч�ԡ�Null�������ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="multiple">���ʣ���������������Чʱ�� value ���Դ����֡�����˲������롰0�����ʾ�����б������㡣</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple, SqlNumericType numericType) {
        return SqlNum(value, multiple, numericType, 0, 0, null);
    }

    /// <summary>
    /// ��ʽ������Ϊָ�����������Ͳ��������ֳ���һ�����ʣ�ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="multiple">���ʣ���������������Чʱ�� value ���Դ����֡�����˲������롰0�����ʾ�����б������㡣</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <param name="replaceNull">��������������Ч��������ת��ʱ�������󣬽�ʹ�ø�ֵ�����Ĭ������¸�ֵӦΪ��Null�����Ա�ʾ�����ݿ��д���һ����ֵ��</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple, SqlNumericType numericType, string replaceNull) {
        return SqlNum(value, multiple, numericType, 0, 0, replaceNull);
    }

    /// <summary>
    /// ��ʽ������Ϊָ�����������ͣ��������������Ч�ԡ�Null�������ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType) {
        return SqlNum(value, 0, numericType, 0, 0, null);
    }

    /// <summary>
    /// ��ʽ������Ϊָ�����������ͣ�ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <param name="replaceNull">��������������Ч��������ת��ʱ�������󣬽�ʹ�ø�ֵ�����Ĭ������¸�ֵӦΪ��Null�����Ա�ʾ�����ݿ��д���һ����ֵ��</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType, string replaceNull) {
        return SqlNum(value, 0, numericType, 0, 0, replaceNull);
    }

    /// <summary>
    /// ��ʽ������Ϊָ�����������ͣ��������������Ч�ԡ�Null�������ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <param name="numericLength">�� numericType = Numeric ʱ����ʾ��Ҫ�����������תΪ Decimal �ͣ�������ָ�� Value * multiple �����󳤶ȣ���С�����֣�</param>
    /// <param name="decimalLength">�� numericType = Numeric ʱ��������ָ��С�����ֵĳ��ȣ����������ֳ���Ϊ numericLength - decimalLength</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType, int numericLength, int decimalLength) {
        return SqlNum(value, 0, numericType, numericLength, decimalLength, null);
    }

    /// <summary>
    /// ��ʽ�����ֲ��������ֳ���һ�����ʣ�ΪInsert��UpDate��׼����
    /// </summary>
    /// <param name="value">׼���������ݿ��</param>
    /// <param name="multiple">���ʣ���������������Чʱ�� value ���Դ����֡�����˲������롰0�����ʾ�����б������㡣</param>
    /// <param name="numericType">�������͡���ʾ���ֶζ�Ӧ���ݿ��ڵ��������͡��ڴ������ݿ�ǰ�ú�����������ת���������޷�ת�������ֽ�ʹ�� replaceNull �滻</param>
    /// <param name="numericLength">�� numericType = Numeric ʱ����ʾ��Ҫ�����������תΪ Decimal �ͣ�������ָ�� Value * multiple �����󳤶ȣ���С�����֣�</param>
    /// <param name="decimalLength">�� numericType = Numeric ʱ��������ָ��С�����ֵĳ��ȣ����������ֳ���Ϊ numericLength - decimalLength</param>
    /// <param name="replaceNull">��������������Ч��������ת��ʱ�������󣬽�ʹ�ø�ֵ�����Ĭ������¸�ֵӦΪ��Null�����Ա�ʾ�����ݿ��д���һ����ֵ��</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple, SqlNumericType numericType, int numericLength, int decimalLength, string replaceNull) {
        try {
            if (value != null) {
                decimal decMultiple = ToDec(multiple);

                switch (numericType) {
                    case SqlNumericType.None:
                        if (decMultiple == 0) {
                            return Convert.ToDecimal(value).ToString();
                        } else {
                            return (Convert.ToDecimal(value) * decMultiple).ToString();
                        }
                    case SqlNumericType.TinyInt:
                        if (decMultiple == 0) {
                            return Convert.ToByte(value).ToString();
                        } else {
                            return Convert.ToByte(Convert.ToDecimal(value) * decMultiple).ToString();
                        }
                    case SqlNumericType.SmallInt:
                        if (decMultiple == 0) {
                            return Convert.ToInt16(value).ToString();
                        } else {
                            return Convert.ToInt16(Convert.ToDecimal(value) * decMultiple).ToString();
                        }
                    case SqlNumericType.Int:
                        if (decMultiple == 0) {
                            return Convert.ToInt32(value).ToString();
                        } else {
                            return Convert.ToInt32(Convert.ToDecimal(value) * decMultiple).ToString();
                        }
                    case SqlNumericType.BigInt:
                        if (decMultiple == 0) {
                            return Convert.ToInt64(value).ToString();
                        } else {
                            return Convert.ToInt64(Convert.ToDecimal(value) * decMultiple).ToString();
                        }
                    case SqlNumericType.Numeric:
                        decimal decValue = Convert.ToDecimal(value);

                        if (decMultiple != 0) {
                            value = decValue * decMultiple;
                        }

                        if (decValue >= (decimal)Math.Pow(10, (numericLength - decimalLength))) {
                            //�����������е��������ġ�return������
                        } else {
                            return Convert.ToString(Math.Round(decValue, decimalLength));
                        }
                        break;
                }
            }
        } catch {
        }

        if (replaceNull != null) {
            return replaceNull;
        } else {
            return "Null";
        }
    }

    public enum SqlNumericType {

        /// <summary>
        /// ���ж���������
        /// </summary>
        None = 0,

        /// <summary>
        /// �� 0 �� 255 ���������ݡ���Byte��
        /// </summary>
        TinyInt = 1,

        /// <summary>
        /// �� -2^15 (-32,768) �� 2^15 - 1 (32,767) ���������ݡ���Short��Int16��
        /// </summary>
        SmallInt = 2,

        /// <summary>
        /// �� -2^31 (-2,147,483,648) �� 2^31 - 1 (2,147,483,647) ���������ݡ���Int��Integer��Int32��
        /// </summary>
        Int = 3,

        /// <summary>
        /// �� -2^63 (-9,223,372,036,854,775,808) �� 2^63-1 (9,223,372,036,854,775,807) ���������ݡ���Long��Int64��
        /// </summary>
        BigInt = 4,

        /// <summary>
        /// �� -10^38 +1 �� 10^38 �C1 �Ĺ̶����Ⱥ�С��λ���������ݡ���Decimal��
        /// </summary>
        Numeric = 5
    }

    #endregion SQL ����

    #region ����ʱ�亯��

    /// <summary>
    /// ϵͳ�ڵ����ڸ�ʽ
    /// </summary>
    public enum DateStyle {

        /// <summary>
        /// �Զ�ʶ������ڸ�ʽ
        /// </summary>
        Automatism = 0,

        /// <summary>
        /// 4λ�꣬2λ�£�2λ�ա����磺20010310��2001��3��10�գ�
        /// </summary>
        YYYYMMDD = 1,

        /// <summary>
        /// 4λ�꣬2λ�£�2λ�㡣���磺20010300��2001��3�£�
        /// </summary>
        YYYYMM00 = 2,

        /// <summary>
        /// 4λ�꣬2λ�ܣ�2λ�㡣���磺20010300��2001������ܣ�
        /// </summary>
        YYYYWW00 = 3,

        /// <summary>
        /// 4λ�꣬2λ���ȣ�2λ�㡣���磺20010300��2001��������ȣ�
        /// </summary>
        YYYYQQ00 = 4,

        /// <summary>
        /// 4λ�꣬4λ�㡣���磺20010000��2001�꣩
        /// </summary>
        YYYY0000 = 5
    }

    /// <summary>
    /// �����������ʽ
    /// </summary>
    public enum OutDateStyle {

        /// <summary>
        /// �����ʽ��YYYY-MM-DD
        /// </summary>
        /// <remarks></remarks>
        YMD_Sign = 1,

        /// <summary>
        /// �����ʽ��YYYY��MM��DD��
        /// </summary>
        /// <remarks></remarks>
        YMD_Num = 2,

        /// <summary>
        /// �����ʽ���٣٣٣���ͣ��£ģ��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YMD_Char = 3,

        /// <summary>
        /// �����ʽ��YYYY��MM��DD�� ���ڣ� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YMDW_Num = 4,

        /// <summary>
        /// �����ʽ���٣٣٣���ͣ��£ģ��� ���ڣ� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YMDW_Char = 5,

        /// <summary>
        /// �����ʽ��YYYY�� ��N��
        /// </summary>
        /// <remarks></remarks>
        YW_Num = 6,

        /// <summary>
        /// �����ʽ���٣٣٣��� �ڣ��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YW_Char = 7,

        /// <summary>
        /// �����ʽ��YYYY�� ��N��(MM��DD�� - MM��DD��)
        /// </summary>
        /// <remarks></remarks>
        YWmd_Num = 8,

        /// <summary>
        /// �����ʽ���٣٣٣��� �ڣ���(MM��DD�� - MM��DD��) ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YWmd_Char = 9,

        /// <summary>
        /// �����ʽ��YYYY-MM
        /// </summary>
        /// <remarks></remarks>
        YM_Sign = 10,

        /// <summary>
        /// �����ʽ��YYYY��MM��
        /// </summary>
        /// <remarks></remarks>
        YM_Num = 11,

        /// <summary>
        /// �����ʽ���٣٣٣���ͣ��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YM_Char = 12,

        /// <summary>
        /// �����ʽ��YYYY�� ��Q����
        /// </summary>
        /// <remarks></remarks>
        YQ_Num = 13,

        /// <summary>
        /// �����ʽ���٣٣٣��� �ڣѼ��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        YQ_Char = 14,

        /// <summary>
        /// �����ʽ��YYYY��
        /// </summary>
        /// <remarks></remarks>
        Y_Num = 15,

        /// <summary>
        /// �����ʽ���٣٣٣��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        Y_Char = 16,

        /// <summary>
        /// �����ʽ��MM��DD��
        /// </summary>
        /// <remarks></remarks>
        MD_Num = 17,

        /// <summary>
        /// �����ʽ���ͣ��£ģ��� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        MD_Char = 18,

        /// <summary>
        /// �����ʽ�����ڣ� ������ȫ�ǵ���ĸ��ʾ�������ֵ����֣�
        /// </summary>
        /// <remarks></remarks>
        W_Char = 19,
    }

    /// <summary>
    /// �����ʱ����ʽ
    /// </summary>
    public enum OutTimeStyle {

        /// <summary>
        /// �����ʽ��HH:MM:SS
        /// </summary>
        HMS_Sign = 1,

        /// <summary>
        /// �����ʽ��HHʱMM��SS��
        /// </summary>
        HMS_Num = 2,

        /// <summary>
        /// �����ʽ�� �ȣ�ʱ�ͣͷ֣ӣ���
        /// </summary>
        HMS_Char = 3,

        /// <summary>
        /// �����ʽ��AM/PM HH:MM:SS
        /// </summary>
        THMS_Sign = 4,

        /// <summary>
        /// �����ʽ��AM/PM HHʱMM��SS��
        /// </summary>
        THMS_Num = 5,

        /// <summary>
        /// �����ʽ������/���� �ȣ�ʱ�ͣͷ֣ӣ���
        /// </summary>
        THMS_Char = 6,

        /// <summary>
        /// �����ʽ��HH:MM
        /// </summary>
        HM_Sign = 7,

        /// <summary>
        /// �����ʽ��HHʱMM��
        /// </summary>
        HM_Num = 8,

        /// <summary>
        /// �����ʽ���ȣ�ʱ�ͣͷ�
        /// </summary>
        HM_Char = 9,

        /// <summary>
        /// �����ʽ��AM/PM HH:MM
        /// </summary>
        THM_Sign = 10,

        /// <summary>
        /// �����ʽ��AM/PM HHʱMM��
        /// </summary>
        THM_Num = 11,

        /// <summary>
        /// �����ʽ������/���� �ȣ�ʱ�ͣͷ�
        /// </summary>
        THM_Char = 12,

        /// <summary>
        /// �����ʽ��HHʱ
        /// </summary>
        H_Num = 13,

        /// <summary>
        /// �����ʽ���ȣ�ʱ
        /// </summary>
        H_Char = 14,

        /// <summary>
        /// �����ʽ��AM/PM HHʱ
        /// </summary>
        TH_Num = 15,

        /// <summary>
        /// �����ʽ������/���� �ȣ�ʱ
        /// </summary>
        TH_Char = 16,
    }

    /// <summary>
    /// ָʾ�ڵ�����������صĺ���ʱ���ȷ�����ڼ�����������ڼ���ĸ�ʽ��
    /// </summary>
    public enum DateInterval {

        /// <summary>
        /// ��
        /// </summary>
        Second,

        /// <summary>
        /// ����
        /// </summary>
        Minute,

        /// <summary>
        /// Сʱ
        /// </summary>
        Hour,

        /// <summary>
        /// ��
        /// </summary>
        Day,

        /// <summary>
        /// ����
        /// </summary>
        Weekday,

        /// <summary>
        /// ��
        /// </summary>
        Month,

        /// <summary>
        /// ����
        /// </summary>
        Quarter,

        /// <summary>
        /// ��
        /// </summary>
        Year
    }

    /// <summary>
    /// ��ʾ 8 λ�������ں� 6 λ����ʱ��Ľṹ
    /// </summary>
    public struct IntDateTime {
        private int m_DateNum;

        /// <summary>
        /// 8λ����������
        /// </summary>
        public int DateNum {
            get { return m_DateNum; }
            set { m_DateNum = value; }
        }

        private int m_TimeNum;

        /// <summary>
        /// 6λ������ʱ��
        /// </summary>
        public int TimeNum {
            get { return m_TimeNum; }
            set { m_TimeNum = value; }
        }
    }

    /// <summary>
    /// ��ȡϵͳ��������ֵ
    /// </summary>
    /// <returns></returns>
    public static int GetIntDate() {
        DateTime dtmValue = DateTime.Today;
        return dtmValue.Year * 10000 + dtmValue.Month * 100 + dtmValue.Day;
    }

    /// <summary>
    /// ��ȡϵͳ����ʱ��ֵ
    /// </summary>
    /// <returns></returns>
    public static int GetIntTime() {
        DateTime dtmValue = DateTime.Now;
        return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
    }

    /// <summary>
    /// �����������תΪ YYYYMMDD ��ʽ���������ڡ��������Զ��жϴ���Ĳ����������ͻ��� 8 λ�������ڡ�
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToIntDate(object value) {
        try {
            if (IsNumeric(value))                                           //����תΪ������
            {
                return ToIntDate(Convert.ToInt32(value));
            } else {                                                               //����תΪ������
                DateTime dtmValue = Convert.ToDateTime(value);
                return ToIntDate(dtmValue, DateStyle.YYYYMMDD);
            }
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// �����������תΪ YYYYMMDD ��ʽ����������
    /// </summary>
    /// <param name="dateTime">DateTime �͵�����</param>
    /// <returns></returns>
    public static int ToIntDate(DateTime dateTime) {
        return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD);
    }

    /// <summary>
    /// �����������תΪָ����ʽ����������
    /// </summary>
    /// <param name="dateTime">DateTime ������ʱ��</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <returns></returns>
    public static int ToIntDate(DateTime dateTime, DateStyle outDateStyle) {
        return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, outDateStyle);
    }

    /// <summary>
    /// �Զ��жϴ�����������ڸ�ʽ��ת��Ϊ YYYYMMDD ��ʽ����������
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <returns></returns>
    public static int ToIntDate(int dateNum) {
        if ((dateNum % 100) != 0) {
            return ToIntDate(dateNum, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD);
        } else if ((dateNum % 10000) != 0) {
            if (((dateNum / 10000) % 100) > 12) {
                return ToIntDate(dateNum, DateStyle.YYYYWW00, DateStyle.YYYYMMDD);
            } else {
                return ToIntDate(dateNum, DateStyle.YYYYMM00, DateStyle.YYYYMMDD);
            }
        } else {
            return ToIntDate(dateNum, DateStyle.YYYY0000, DateStyle.YYYYMMDD);
        }
    }

    /// <summary>
    /// ָ��������������ڸ�ʽ��תΪ YYYYMMDD ��ʽ������
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">������������ڸ�ʽ</param>
    /// <returns></returns>
    public static int ToIntDate(int dateNum, DateStyle inDateStyle) {
        return ToIntDate(dateNum, inDateStyle, DateStyle.YYYYMMDD);
    }

    /// <summary>
    /// �������ָ����ʽ����������תΪָ����ʽ����������
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">������������ڸ�ʽ</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <returns></returns>
    public static int ToIntDate(int dateNum, DateStyle inDateStyle, DateStyle outDateStyle) {
        DateTime dtm = ToDateTime(dateNum, 0, inDateStyle);

        int intDateValue = 0;

        switch (outDateStyle) {
            case DateStyle.YYYYMMDD:
                intDateValue = dtm.Year * 10000 + dtm.Month * 100 + dtm.Day;
                break;

            case DateStyle.YYYYWW00:
                intDateValue = dtm.Year * 10000 + ((dtm.DayOfYear + (int)(new DateTime(dtm.Year, 1, 1).DayOfWeek) - 1) / 7 + 1) * 100;
                break;

            case DateStyle.YYYYMM00:
                intDateValue = dtm.Year * 10000 + dtm.Month * 100;
                break;

            case DateStyle.YYYYQQ00:
                intDateValue = dtm.Year * 10000 + ((dtm.Month + 2) / 3) * 100;
                break;

            case DateStyle.YYYY0000:
                intDateValue = dtm.Year * 10000;
                break;
        }

        return intDateValue;
    }

    /// <summary>
    /// �����������תΪ HHMMSS ��ʽ������ʱ�䡣�������Զ��жϴ���Ĳ����������ͻ��� 6 λ����ʱ�䣬����� 6 λ����ʱ��˺���������ʽ������ʱ�����ȷ�ԡ�
    /// </summary>
    /// <param name="value">8 λ�������ڻ��� DateTime �͵�����</param>
    /// <returns></returns>
    public static int ToIntTime(object value) {
        try {
            if (IsNumeric(value))                                           //����תΪ������
            {
                int intValue = Convert.ToInt32(value);
                DateTime dtmValue = new DateTime();
                dtmValue = dtmValue.AddSeconds(intValue % 100);
                dtmValue = dtmValue.AddMinutes((intValue / 100) % 100);
                dtmValue = dtmValue.AddHours(intValue / 10000);
                return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
            } else {                                                               //����תΪ������
                DateTime dtmValue = Convert.ToDateTime(value);
                return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
            }
        } catch {
            return 0;
        }
    }

    public static DateTime ToDateTime(string dateNum) {
        try {
            DateTime _dtmValue = Convert.ToDateTime(dateNum);
            if (_dtmValue < Convert.ToDateTime("1900-1-1")) {
                return Convert.ToDateTime("1900-1-1");
            } else {
                return Convert.ToDateTime(dateNum);
            }
        } catch {
            return Convert.ToDateTime("1900-1-1");
        }
    }

    /// <summary>
    /// ��ָ��������ʽ���������������תΪ����������
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum) {
        return ToDateTime(dateNum, 0, DateStyle.Automatism);
    }

    /// <summary>
    /// �Զ��ж�������ʽ������������������ڡ�ʱ��תΪ����������ʱ��
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum, int timeNum) {
        return ToDateTime(dateNum, timeNum, DateStyle.Automatism);
    }

    /// <summary>
    /// ��ָ��������ʽ���������������תΪ����������
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">������������ڸ�ʽ</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum, DateStyle inDateStyle) {
        return ToDateTime(dateNum, 0, inDateStyle);
    }

    /// <summary>
    /// ��ָ��������ʽ��������������ڡ�ʱ��תΪ����������ʱ��
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="inDateStyle">������������ڸ�ʽ</param>
    /// <returns>����������</returns>
    public static DateTime ToDateTime(int dateNum, int timeNum, DateStyle inDateStyle) {
        DateTime dtm = new DateTime();

        if (dateNum > 10000 && dateNum <= 99991231) {
            //�Զ��ж���������
            if (inDateStyle == DateStyle.Automatism) {
                if ((dateNum % 100) != 0) {
                    inDateStyle = DateStyle.YYYYMMDD;
                } else if ((dateNum % 10000) != 0) {
                    if (((dateNum / 10000) % 100) > 12) {
                        inDateStyle = DateStyle.YYYYWW00;
                    } else {
                        inDateStyle = DateStyle.YYYYMM00;
                    }
                } else {
                    inDateStyle = DateStyle.YYYY0000;
                }
            }

            switch (inDateStyle) {
                case DateStyle.YYYYMMDD:
                    dtm = dtm.AddYears((dateNum / 10000) - 1);
                    dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
                    dtm = dtm.AddDays((dateNum % 100) - 1);
                    break;

                case DateStyle.YYYYWW00:
                    dtm = new DateTime((dateNum / 10000), 1, 1);                                         //�����1��1�յ�����
                    int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1��1�վ��ܵ���ʼ������
                    dtm = dtm.AddDays(-intFirstDayOfWeek + (((dateNum / 100) % 100) - 1) * 7);           //�����ڸı��1��1�������ܵ���ʼ�յ����ڣ�������ָ��������
                    break;

                case DateStyle.YYYYMM00:
                    dtm = dtm.AddYears((dateNum / 10000) - 1);
                    dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
                    dtm = dtm.AddDays((dateNum % 100) - 1);

                    if (dtm.Day != 0) {
                        dtm = dtm.AddDays(1 - dtm.Day);
                    }
                    break;

                case DateStyle.YYYYQQ00:
                    dtm = new DateTime((dateNum / 10000), 1, 1);                                         //�����1��1�յ�����
                    dtm = dtm.AddMonths((((dateNum / 100) % 100) - 1) * 3);
                    break;

                case DateStyle.YYYY0000:
                    dtm = dtm.AddYears((dateNum / 10000) - 1);
                    dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
                    dtm = dtm.AddDays((dateNum % 100) - 1);

                    if (dtm.Day != 0) {
                        dtm = dtm.AddDays(1 - dtm.Day);
                    }

                    if (dtm.Month != 0) {
                        dtm = dtm.AddMonths(1 - dtm.Month);
                    }
                    break;
            }
        }

        if (timeNum > 0) {
            dtm = dtm.AddHours(timeNum / 10000);
            dtm = dtm.AddMinutes((timeNum / 100) % 100);
            dtm = dtm.AddSeconds(timeNum % 100);
        }
        return dtm;
    }

    /// <summary>
    /// �Զ��жϴ�����������ڸ�ʽ������������ָ��ֵ����������ڣ������ YYYYMMDD ��ʽ IntDateTime �ṹ���ء�
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="dateNum">8 λ��������</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum) {
        if ((dateNum % 100) != 0) {
            return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD, 0);
        } else if ((dateNum % 10000) != 0) {
            if (((dateNum / 10000) % 100) > 12) {
                return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYWW00, DateStyle.YYYYMMDD, 0);
            } else {
                return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYYMM00, DateStyle.YYYYMMDD, 0);
            }
        } else {
            return DoDateAdd(interval, addNumber, dateNum, DateStyle.YYYY0000, DateStyle.YYYYMMDD, 0);
        }
    }

    /// <summary>
    /// ����������������ָ��ֵ����������ڣ������ IntDateTime �ṹ���ء�
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="dateNum">8 λ��������</param>
    /// <param name="inDateStyle">������������ڸ�ʽ</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum, DateStyle inDateStyle, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, dateNum, inDateStyle, outDateStyle, 0);
    }

    /// <summary>
    /// ��������ʱ������ָ��ֵ�������ʱ�䣬����� IntDateTime �ṹ���ء��˺��������� dateNum ���������Դ��� null
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="dateNum">���� null</param>
    /// <param name="timeNum">6 λ����ʱ��</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int? dateNum, int timeNum) {
        return DoDateAdd(interval, addNumber, 0, DateStyle.YYYYMMDD, null, timeNum);
    }

    /// <summary>
    /// ������������ʱ������ָ��ֵ�����������ʱ�䣬����� IntDateTime �ṹ���ء�
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="dateTime">DateTime ��ʽ������ʱ��</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, DateTime dateTime, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, ToIntDate(dateTime, DateStyle.YYYYMMDD), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime));
    }

    /// <summary>
    /// ������������ʱ������ָ��ֵ�����������ʱ�䣬����� IntDateTime �ṹ���ء�
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="intDateTime">IntDateTime ��ʽ������ʱ��</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, IntDateTime intDateTime, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, intDateTime.DateNum, DateStyle.YYYYMMDD, outDateStyle, intDateTime.TimeNum);
    }

    /// <summary>
    /// ������������ʱ������ָ��ֵ�����������ʱ�䣬����� IntDateTime �ṹ���ء�
    /// </summary>
    /// <param name="interval">�������ڻ�ʱ�����ĸ�ʽ</param>
    /// <param name="addNumber">Ҫ���������ڻ�ʱ��ļ������</param>
    /// <param name="dateNum">8 λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">ϣ����������ڸ�ʽ</param>
    /// <param name="timeNum">6 λ����ʱ��</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum, DateStyle inDateStyle, DateStyle? outDateStyle, int timeNum) {
        DateTime dtm = ToDateTime(dateNum, timeNum, (DateStyle)inDateStyle);

        switch (interval) {
            case DateInterval.Second:
                dtm = dtm.AddSeconds(addNumber);
                break;

            case DateInterval.Minute:
                dtm = dtm.AddMinutes(addNumber);
                break;

            case DateInterval.Hour:
                dtm = dtm.AddHours(addNumber);
                break;

            case DateInterval.Day:
                dtm = dtm.AddDays(addNumber);
                break;

            case DateInterval.Weekday:
                dtm = dtm.AddDays(addNumber * 7);
                break;

            case DateInterval.Month:
                dtm = dtm.AddMonths(addNumber);
                break;

            case DateInterval.Quarter:
                dtm = dtm.AddMonths(addNumber * 3);
                break;

            case DateInterval.Year:
                dtm = dtm.AddYears(addNumber);
                break;
        }

        IntDateTime idt = new IntDateTime();

        if (null != outDateStyle) {
            idt.DateNum = ToIntDate(dtm, (DateStyle)outDateStyle);
        }

        idt.TimeNum = dtm.Hour * 10000 + dtm.Minute * 100 + dtm.Second;

        return idt;
    }

    /// <summary>
    /// �ж�2������֮������ʱ��ֵ����ʽ���� dateTime2 - dateTime1 ���м���
    /// </summary>
    /// <param name="interval">ָʾ�ڵ�����������صĺ���ʱ���ȷ�����ڼ�����������ڼ���ĸ�ʽ��</param>
    /// <param name="dateTime1">����1��ͨ��Ϊ֮ǰ������</param>
    /// <param name="dateTime2">����2��ͨ��Ϊ֮�������</param>
    /// <returns></returns>
    public static int DoDateDiff(DateInterval interval, DateTime dateTime1, DateTime dateTime2) {
        int intValue = 0;
        switch (interval) {
            case DateInterval.Second:
                TimeSpan timeSpanSecond = dateTime2 - dateTime1;
                intValue = (int)timeSpanSecond.TotalSeconds;
                break;

            case DateInterval.Minute:
                TimeSpan timeSpanMinute = dateTime2 - dateTime1;
                intValue = (int)timeSpanMinute.TotalMinutes;
                break;

            case DateInterval.Hour:
                TimeSpan timeSpanHour = dateTime2 - dateTime1;
                intValue = (int)timeSpanHour.TotalHours;
                break;

            case DateInterval.Day:
                TimeSpan timeSpanDay = dateTime2 - dateTime1;
                intValue = (int)timeSpanDay.TotalDays;
                break;

            case DateInterval.Weekday:
                break;

            case DateInterval.Month:
                int intMonty1 = dateTime1.Year * 12 + dateTime1.Month;
                int intMonty2 = dateTime2.Year * 12 + dateTime2.Month;
                intValue = intMonty2 - intMonty1;
                break;

            case DateInterval.Quarter:
                break;

            case DateInterval.Year:
                intValue = dateTime2.Year - dateTime1.Year;
                break;
        }
        return intValue;
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, "", "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, "", "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, "", "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������ں�ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// ����������ڰ�ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, "", "", "");
    }

    /// <summary>
    ///
    /// ����������ڰ�ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, "", "");
    }

    /// <summary>
    /// ����������ڰ�ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�������ַ���
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ�������ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// �������ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, "", "", "");
    }

    /// <summary>
    /// �������ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// �������ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ��ʱ���ַ���
    /// </summary>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ��ʱ���ַ���</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, object timeNum, OutTimeStyle outTimeStyle) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, "", "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle? inDateStyle, OutDateStyle? outDateStyle, object timeNum, OutTimeStyle? outTimeStyle, string replaceNull) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// ����������ڡ�ʱ�䰴ָ����ʽת��Ϊ�ʺ�ҳ����ʾ�����ڡ�ʱ���ַ���
    /// </summary>
    /// <param name="dateNum">8λ��������</param>
    /// <param name="inDateStyle">��������ڸ�ʽ</param>
    /// <param name="outDateStyle">�����������ʽ</param>
    /// <param name="timeNum">6λ����ʱ��</param>
    /// <param name="outTimeStyle">�����ʱ����ʽ</param>
    /// <param name="replaceNull">�������Ĳ���Ϊ���Ƿ��ص�ֵ</param>
    /// <param name="prefix">ǰ׺</param>
    /// <param name="postfix">��׺</param>
    /// <returns>�ʺ�ҳ����ʾ������ʱ���ַ���</returns>
    public static string OutDateTime(object dateNum, DateStyle? inDateStyle, OutDateStyle? outDateStyle, object timeNum, OutTimeStyle? outTimeStyle, string replaceNull, string prefix, string postfix) {
        try {
            string strValue = "";

            if (null != dateNum && null != outDateStyle) {
                if (null == inDateStyle) {
                    inDateStyle = DateStyle.Automatism;
                }
                DateTime dtm = ToDateTime((int)dateNum, (DateStyle)inDateStyle);

                switch (outDateStyle) {
                    case OutDateStyle.YMD_Sign:
                        strValue += dtm.Year + "-" + OutDateTimeNum(dtm.Month) + "-" + OutDateTimeNum(dtm.Day);
                        break;

                    case OutDateStyle.YMD_Num:
                        strValue += dtm.Year + "��" + dtm.Month + "��" + dtm.Day + "��";
                        break;

                    case OutDateStyle.YMD_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "��" + ToCharacter(dtm.Month, CharacterStyle.Character) + "��" + ToCharacter(dtm.Day, CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.YMDW_Num:
                        strValue += dtm.Year + "��" + dtm.Month + "��" + dtm.Day + "�� " + GetWeekdayName(dtm.DayOfWeek);
                        break;

                    case OutDateStyle.YMDW_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "��" + ToCharacter(dtm.Month, CharacterStyle.Character) + "��" + ToCharacter(dtm.Day, CharacterStyle.Character) + "�� " + GetWeekdayName(dtm.DayOfWeek);
                        break;

                    case OutDateStyle.YW_Num:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "�� ��" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.YW_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "�� ��" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.YWmd_Num:
                        DateTime dtmYWmd_Num = dtm.AddDays(-(int)dtm.DayOfWeek);
                        strValue += dtmYWmd_Num.Year + "�� ��" + GetWeekOfYear(dtmYWmd_Num) + "�� (" + dtmYWmd_Num.Month + "��" + dtmYWmd_Num.Day + "�գ�" + dtmYWmd_Num.AddDays(6).Month + "��" + dtmYWmd_Num.AddDays(6).Day + "��)";
                        break;

                    case OutDateStyle.YWmd_Char:
                        DateTime dtmYWmd_Char = dtm.AddDays(-(int)dtm.DayOfWeek);
                        strValue += ToCharacter(dtmYWmd_Char.Year, CharacterStyle.Character) + "�� ��" + ToCharacter(GetWeekOfYear(dtmYWmd_Char), CharacterStyle.Character) + "�� (" + dtmYWmd_Char.Month + "��" + dtmYWmd_Char.Day + "�գ�" + dtmYWmd_Char.AddDays(6).Month + "��" + dtmYWmd_Char.AddDays(6).Day + "��)";
                        break;

                    case OutDateStyle.YM_Sign:
                        strValue += dtm.Year + "-" + OutDateTimeNum(dtm.Month);
                        break;

                    case OutDateStyle.YM_Num:
                        strValue += dtm.Year + "��" + dtm.Month + "��";
                        break;

                    case OutDateStyle.YM_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "��" + ToCharacter(dtm.Month, CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.YQ_Num:
                        strValue += dtm.Year + "�� ��" + ((dtm.Month + 2) / 3) + "����";
                        break;

                    case OutDateStyle.YQ_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "�� ��" + ToCharacter(((dtm.Month + 2) / 3), CharacterStyle.Character) + "����";
                        break;

                    case OutDateStyle.Y_Num:
                        strValue += dtm.Year + "��";
                        break;

                    case OutDateStyle.Y_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.MD_Num:
                        strValue += dtm.Month + "��" + dtm.Day + "��";
                        break;

                    case OutDateStyle.MD_Char:
                        strValue += ToCharacter(dtm.Month, CharacterStyle.Character) + "��" + ToCharacter(dtm.Day, CharacterStyle.Character) + "��";
                        break;

                    case OutDateStyle.W_Char:
                        strValue += GetWeekdayName(dtm.DayOfWeek);
                        break;
                }
            }

            if (null != outDateStyle && null != outTimeStyle) {
                if (null == dateNum && null == timeNum) {
                    return replaceNull;
                }

                if (null != dateNum && null != timeNum) {
                    strValue += " ";
                }
            }

            if (null != timeNum && null != outTimeStyle) {
                DateTime dtm = ToDateTime(0, (int)timeNum);

                switch (outTimeStyle) {
                    case OutTimeStyle.HMS_Sign:
                        strValue += dtm.Hour + ":" + OutDateTimeNum(dtm.Minute) + ":" + OutDateTimeNum(dtm.Second);
                        break;

                    case OutTimeStyle.HMS_Num:
                        strValue += dtm.Hour + "ʱ" + OutDateTimeNum(dtm.Minute) + "��" + OutDateTimeNum(dtm.Second) + "��";
                        break;

                    case OutTimeStyle.HMS_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "ʱ" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "��" + ToCharacter(dtm.Second, CharacterStyle.Character) + "��";
                        break;

                    case OutTimeStyle.THMS_Sign:
                        if ((dtm.Hour) < 12) {
                            strValue += "AM ";
                        } else {
                            strValue += "PM ";
                        }
                        strValue += (dtm.Hour % 12) + ":" + OutDateTimeNum(dtm.Minute) + ":" + OutDateTimeNum(dtm.Second);
                        break;

                    case OutTimeStyle.THMS_Num:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += (dtm.Hour % 12) + "ʱ" + OutDateTimeNum(dtm.Minute) + "��" + OutDateTimeNum(dtm.Second) + "��";
                        break;

                    case OutTimeStyle.THMS_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "ʱ" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "��" + ToCharacter(dtm.Second, CharacterStyle.Character) + "��";
                        break;

                    case OutTimeStyle.HM_Sign:
                        strValue += dtm.Hour + ":" + OutDateTimeNum(dtm.Minute);
                        break;

                    case OutTimeStyle.HM_Num:
                        strValue += dtm.Hour + "ʱ" + OutDateTimeNum(dtm.Minute) + "��";
                        break;

                    case OutTimeStyle.HM_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "ʱ" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "��";
                        break;

                    case OutTimeStyle.THM_Sign:
                        if ((dtm.Hour) < 12) {
                            strValue += "AM ";
                        } else {
                            strValue += "PM ";
                        }
                        strValue += (dtm.Hour % 12) + ":" + OutDateTimeNum(dtm.Minute);
                        break;

                    case OutTimeStyle.THM_Num:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += (dtm.Hour % 12) + "ʱ" + OutDateTimeNum(dtm.Minute) + "��";
                        break;

                    case OutTimeStyle.THM_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "ʱ" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "��";
                        break;

                    case OutTimeStyle.H_Num:
                        strValue += dtm.Hour + "ʱ";
                        break;

                    case OutTimeStyle.H_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "ʱ";
                        break;

                    case OutTimeStyle.TH_Num:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += (dtm.Hour % 12) + "ʱ";
                        break;

                    case OutTimeStyle.TH_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "���� ";
                        } else {
                            strValue += "���� ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "ʱ";
                        break;
                }
            }

            return prefix + strValue + postfix;
        } catch {
            return replaceNull;
        }
    }

    /// <summary>
    /// ��С��10�����ּ���ǰ׺��0�������ڡ�OutDateTime���������ʱ��ʱʹ��
    /// </summary>
    /// <param name="num">����</param>
    /// <returns></returns>
    private static string OutDateTimeNum(int num) {
        if (num < 10) {
            return "0" + num;
        } else {
            return num.ToString();
        }
    }

    /// <summary>
    /// ��ô�������������Ǹ����еĵڼ���
    /// </summary>
    /// <param name="dateNum">��Ҫ������������������</param>
    /// <returns>�������Ǹ�����еĵڼ���</returns>
    public static int GetWeekOfYear(int dateNum) {
        DateTime dtm = new DateTime();
        dtm = dtm.AddYears((dateNum / 10000) - 1);
        dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
        dtm = dtm.AddDays((dateNum % 100) - 1);
        return GetWeekOfYear(dtm);
    }

    /// <summary>
    /// ��ô���������Ǹ����еĵڼ���
    /// </summary>
    /// <param name="dateTime">��Ҫ��������������</param>
    /// <returns>�������Ǹ�����еĵڼ���</returns>
    public static int GetWeekOfYear(DateTime dateTime) {
        DateTime dtm = new DateTime(dateTime.Year, 1, 1);                                                //�����1��1�յ�����
        int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1��1�վ��ܵ���ʼ������
        dtm = dtm.AddDays(-intFirstDayOfWeek);                                                  //�����ڸı��1��1�������ܵ���ʼ�յ�����
        return ((dateTime.Subtract(dtm).Days) / 7) + 1;
    }

    /// <summary>
    /// ���������������תΪ����������������0�����������գ���6������������
    /// </summary>
    /// <param name="weekday">���ڵ���������</param>
    /// <returns>���ֵ���������</returns>
    public static string GetWeekdayName(int weekday) {
        return GetWeekdayName((System.DayOfWeek)weekday);
    }

    /// <summary>
    /// ������� DayOfWeek ������תΪ������������
    /// </summary>
    /// <param name="weekday">DayOfWeek ������ö��</param>
    /// <returns>���ֵ���������</returns>
    public static string GetWeekdayName(System.DayOfWeek weekday) {
        switch (weekday) {
            case DayOfWeek.Monday:
                return "����һ";

            case DayOfWeek.Tuesday:
                return "���ڶ�";

            case DayOfWeek.Wednesday:
                return "������";

            case DayOfWeek.Thursday:
                return "������";

            case DayOfWeek.Friday:
                return "������";

            case DayOfWeek.Saturday:
                return "������";

            default:
                return "������";
        }
    }

    #endregion ����ʱ�亯��

    #region ��������

    /// <summary>
    /// ������Ķ��󱣴�Ϊ XML ��ʽ���ļ�
    /// </summary>
    /// <param name="obj">��Ҫ����鿴���������</param>
    /// <param name="fileName">XML �ļ���</param>
    public static void ObjectToXml(object obj, string fileName) {
        if (obj == null) {
            throw new Exception("��ǰ����Ķ���Ϊ��");
        } else {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            xs.Serialize(fs, obj);
            fs.Dispose();
            fs.Close();
        }
    }

    /// <summary>
    /// ��ö��ֵתΪ��ֵ����
    /// ���磺SortedList sl = SysFun.EnumToSortedList(System.Type.GetType("�����ռ�.ö��"));��ע�⣺�������ռ�.ö�١���Ҫ��������������Ϊ�ַ������룻�������������һ�� dll �ļ��ڣ���д�ɡ������ռ�.ö��,�����ռ䡱��
    /// </summary>
    /// <param name="enumType">��Ҫת����ö�����ͣ����磺SortedList sl = SysFun.EnumToSortedList(System.Type.GetType("�����ռ�.ö��"));��ע�⣺�������ռ�.ö�١���Ҫ��������������Ϊ�ַ������룻�������������һ�� dll �ļ��ڣ���д�ɡ������ռ�.ö��,�����ռ䡱��</param>
    /// <returns></returns>
    public static System.Collections.SortedList EnumToSortedList(System.Type enumType) {
        System.Collections.SortedList slEnum = new System.Collections.SortedList();
        int[] intValue = (int[])System.Enum.GetValues(enumType);
        string[] strValue = System.Enum.GetNames(enumType);

        for (int intIndex = 0; intIndex < intValue.Length; intIndex++) {
            slEnum.Add(intValue[intIndex], strValue[intIndex]);
        }

        return slEnum;
    }

    /// <summary>
    /// �ڴ�����ַ�����������***.***.***.***����ʽ�� IP ��ַ�������������� IP ��ַ����Ϊ��������
    /// </summary>
    /// <param name="ip">��***.***.***.***����ʽ�� IP ��ַ</param>
    /// <returns>�������ֱ�ʾ�� IP ��ַ</returns>
    public static int IpToInt(string ip) {
        long intIp = 0;
        string strIp = System.Text.RegularExpressions.Regex.Match(ip, "\\d+\\.\\d+\\.\\d+\\.\\d+").Value;

        if (strIp.Length > 0) {
            string[] arrIp = strIp.Split(new Char[] { '.' });

            for (int intIndex = 0; intIndex < arrIp.Length; intIndex++) {
                intIp += Convert.ToInt32(arrIp[intIndex]) * (long)System.Math.Pow(256, -(intIndex - 3));
            }
        }

        return (int)(intIp - 2147483648);
    }

    /// <summary>
    /// ������� Int32 ��������תΪ��***.***.***.***����ʽ�� IP ��ַ
    /// </summary>
    /// <param name="intIp">�������ֱ�ʾ�� IP ��ַ</param>
    /// <returns>��***.***.***.***����ʽ�� IP ��ַ</returns>
    public static string IntToIp(int intIp) {
        long lonIp = intIp + 2147483648;

        byte bytIp1 = (byte)((lonIp / 16777216) % 256);
        byte bytIp2 = (byte)((lonIp / 65536) % 256);
        byte bytIp3 = (byte)((lonIp / 256) % 256);
        byte bytIp4 = (byte)(lonIp % 256);

        return bytIp1.ToString() + "." + bytIp2.ToString() + "." + bytIp3.ToString() + "." + bytIp4.ToString();
    }

    /// <summary>
    /// �ж��Ƿ���SQLע�룬����з���""�ַ���������ԭ�ַ������ء�
    /// </summary>
    /// <param name="value">Ҫ�����ִ�</param>
    /// <returns>string</returns>
    public static string IsSQL(object value) {
        try {
            string InPut = Convert.ToString(value).Trim().ToLower();
            Regex reg = new Regex(@"\?|select%20|select\s+|insert%20|insert\s+|delete%20|delete\s+|count\(|drop%20|drop\s+|update%20|update\s+|exec%20|exec\s+|and%20|and\s+|or%20|or\s+|'|'\s+", RegexOptions.IgnoreCase);
            if (reg.IsMatch(InPut)) {
                return "";
            } else {
                return InPut.Trim();
            }
        } catch { return ""; }
    }

    #endregion ��������

    public static string DataTable2Json(DataTable dt) {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append(dt.TableName);
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++) {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++) {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString());
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }
}