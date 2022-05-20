using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

public static class SysFun {

    #region "数字函数"

    /// <summary>
    /// 确保将输入的布尔型值转为 0 或 1 的数字。例如 True 将转为 1；False 将转为 0 。
    /// </summary>
    /// <param name="value">欲转换成数字的布尔值</param>
    /// <returns>0 或 1 的数字</returns>
    public static byte BoolToByte(object value) {
        try {
            if (Convert.ToBoolean(value) == true) {
                return 1;
            }
        } catch { }
        return 0;
    }

    /// <summary>
    /// 确保将 0 或 1 的数字或者 True 或 False 的字符串、布尔值转为 Boolean 型值。例如 0 将转为 False；1 将转为 True 。
    /// </summary>
    /// <param name="value">0、1、"true"、"false"、True、False</param>
    /// <returns>True 或 False 的布尔值</returns>
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
    /// 确保将输入的参数转为 Byte 型数字
    /// </summary>
    /// <param name="value">0 到 255（无符号）；舍入小数部分。</param>
    /// <returns>0 - 255 之间的数字，如果遇到异常返回“0”</returns>
    public static byte ToByte(object value) {
        try {
            return Convert.ToByte(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 Integer 型数字
    /// </summary>
    /// <param name="value">-2,147,483,648 到 2,147,483,647；舍入小数部分。</param>
    /// <returns>-2,147,483,648 到 2,147,483,647 之间的数字，如果遇到异常返回“0”</returns>
    public static int ToInt(object value) {
        try {
            return Convert.ToInt32(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 Long 型数字
    /// </summary>
    /// <param name="value">-9,223,372,036,854,775,808 到 9,223,372,036,854,775,807；舍入小数部分。</param>
    /// <returns>-9,223,372,036,854,775,808 到 9,223,372,036,854,775,807 之间的数字，如果遇到异常返回“0”</returns>
    public static long ToLong(object value) {
        try {
            return Convert.ToInt64(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 Float 型数字
    /// </summary>
    public static float ToFloat(object value) {
        try {
            return Convert.ToSingle(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 Decimal 型数字
    /// </summary>
    /// <param name="value">对于零变比数值，即无小数位数值，为 +/-79,228,162,514,264,337,593,543,950,335。对于具有 28 位小数位的数字，范围是 +/-7.9228162514164337593543950335。最小的可用非零数是 0.0000000000000000000000000001 (+/-1E-28)。</param>
    /// <returns>Decimal 型数字，如果遇到异常返回“0”</returns>
    public static decimal ToDec(object value) {
        try {
            return Convert.ToDecimal(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 double 型数字
    /// </summary>
    /// <param name="value">对于零变比数值，即无小数位数值，为 +/-79,228,162,514,264,337,593,543,950,335。对于具有 28 位小数位的数字，范围是 +/-7.9228162514164337593543950335。最小的可用非零数是 0.0000000000000000000000000001 (+/-1E-28)。</param>
    /// <returns>double 型数字，如果遇到异常返回“0”</returns>
    public static double ToDouble(object value) {
        try {
            return Convert.ToDouble(value);
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 判断传入的对象是否可以转为数字
    /// </summary>
    /// <param name="value">数字、字符串或任一对象</param>
    /// <returns>如果是数字返回 True，否则返回 False</returns>
    public static bool IsNumeric(object value) {
        try {
            decimal dec = Convert.ToDecimal(value);
            return true;
        } catch {
            return false;
        }
    }

    /// <summary>
    /// 确保将输入的参数转为 Float 型数字 ,输入0时返回false
    /// </summary>
    public static bool ToFloatNumeric(object value) {
        try {
            if (Convert.ToDouble(value.ToString()) == 0.0)//输入价格为0时返回false
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
    /// 将数字显示在页面或控件上
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <returns></returns>
    public static string OutNum(object value) {
        return OutNum(value, -1, 0, "", null, "", "");
    }

    /// <summary>
    /// 将数字显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <returns></returns>
    public static string OutNum(object value, string replaceNull) {
        return OutNum(value, -1, 0, replaceNull, null, "", "");
    }

    /// <summary>
    /// 指定数字保留的小数位数显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits) {
        return OutNum(value, decimalDigits, 0, "", null, "", "");
    }

    /// <summary>
    /// 指定数字保留的小数位数显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, "", "");
    }

    /// <summary>
    /// 指定数字保留的小数位数并加上后缀后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull, string postfix) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, "", postfix);
    }

    /// <summary>
    /// 指定数字保留的小数位数并加上前缀、后缀后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
    /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, string replaceNull, string prefix, string postfix) {
        return OutNum(value, decimalDigits, 0, replaceNull, null, prefix, postfix);
    }

    /// <summary>
    /// 将数字乘以一个倍率后显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="multiple">倍率，如果输入的 value 数字有效并且 multiple 不等于 0，将返回“value * multiple”的结果</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull) {
        return OutNum(value, decimalDigits, multiple, replaceNull, null, "", "");
    }

    /// <summary>
    /// 将数字乘以一个倍率并加上后缀后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="multiple">倍率，如果输入的 value 数字有效并且 multiple 不等于 0，将返回“value * multiple”的结果</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
    /// <returns></returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull, string postfix) {
        return OutNum(value, decimalDigits, multiple, replaceNull, null, "", postfix);
    }

    /// <summary>
    /// 将数字显示在页面或控件上。如果输入的数字是空或者输入的数字等于 replaceValue 的设定值，则返回 replaceNull 字符串
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <param name="replaceValue">替换数值。如果 value 等于该值，则返回 replaceNull 设定的字符串</param>
    /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
    /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
    /// <returns>处理后的可用于显示在页面或控件上的字符串</returns>
    public static string OutNum(object value, string replaceNull, double? replaceValue, string prefix, string postfix) {
        return OutNum(value, 0, 0, replaceNull, replaceValue, prefix, postfix);
    }

    /// <summary>
    /// 将数字乘以一个倍率并加上前缀、后缀后，显示在页面或控件上（可作为百分比输出，如“98.94%”）
    /// </summary>
    /// <param name="value">输入的数字</param>
    /// <param name="decimalDigits">保留的小数位数。如果该值为“-1”则不处理小数位数，有多少位就显示多少位。</param>
    /// <param name="multiple">倍率，如果输入的 value 数字有效并且 multiple 不等于 0，将返回“value * multiple”的结果</param>
    /// <param name="replaceNull">如果输入为 DBNull 或“空”时返回的值</param>
    /// <param name="replaceValue">替换数值。如果 value 等于该值，则返回 replaceNull 设定的字符串</param>
    /// <param name="prefix">前缀。如（“重量：234.56公斤”，其中“重量：”为前缀）</param>
    /// <param name="postfix">后缀。如（“重量：234.56公斤”，其中“公斤”为后缀）</param>
    /// <returns>处理后的可用于显示在页面或控件上的字符串</returns>
    public static string OutNum(object value, int decimalDigits, double multiple, string replaceNull, double? replaceValue, string prefix, string postfix) {
        try {
            if (Convert.IsDBNull(value)) {
                return replaceNull;
            }

            decimal decValue = Convert.ToDecimal(value);

            if (null != replaceValue) {
                if (decValue == (decimal)replaceValue) {
                    return replaceNull;                                             //替换数值
                }
            }

            if (multiple != 0) {
                decValue = decValue * (decimal)multiple;
            }

            if (decValue % 1 == 0)                                                  //没有小数位数
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

    #endregion "数字函数"

    #region 数学运算函数

    /// <summary>
    /// 将传入的 params 对象集合转为字符串数组，并判断设置 doNull 参数
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
        /// 将传入的 params 对象集合转为字符串数组，并判断设置 doNull 参数
        /// </summary>
        /// <param name="doNull">如果传入的参数中没有明确指出数字为空时是否执行运算，则以此标志代替。</param>
        /// <param name="value">储存数字的字符串参数</param>
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
    /// 可空类型的默认值
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
    /// 将传入的各参数进行加法运算 (Value(0) + Value(1) + Value(2))。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值相加后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoAdd(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoAdd(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行加法运算。(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相加后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoAdd(string value, bool doNull) {
        return DoAdd(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 将数组内各数字进行加法运算。(Value(0) + Value(1) + Value(2))
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相加后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 将传入的各参数进行减法运算 (Value(0) - Value(1) - Value(2)) 用第一个参数减后面的参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值相减后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoSub(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoSub(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行减法运算。(Value1 - Value2 - Value3) 用第一个参数减后面的参数
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相减后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoSub(string value, bool doNull) {
        return DoSub(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 将数组内各数字进行减法运算。(Value(0) - Value(1) - Value(2)) 用第一个参数减后面的参数
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相减后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 将传入的各参数进行乘法运算 (Value(0) * Value(1) * Value(2))。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值相乘后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMul(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMul(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行乘法运算。(Value1 * Value2 * Value3)
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相乘后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMul(string value, bool doNull) {
        return DoMul(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 将数组内各数字进行乘法运算。(Value(0) * Value(1) * Value(2))
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相乘后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 将传入的各参数进行除法运算 (Value(0) / Value(1) / Value(2)) 用第一个参数除后面的参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理。如果 Value1、Value2 这些分母出现“0”值将导致运算停止并返回空字符串。
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值相除后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoDiv(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoDiv(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行除法运算。(Value1 / Value2 / Value3) 用第一个参数除后面的参数。如果 Value1、Value2 这些分母出现“0”值将导致运算停止并返回空字符串。
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相除后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoDiv(string value, bool doNull) {
        return DoDiv(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 将数组内各数字进行除法运算。(Value(0) / Value(1) / Value(2)) 用第一个参数除后面的参数。如果 Value1、Value2 这些分母出现“0”值将导致运算停止并返回空字符串。
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值相除后的运算结果，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 对两数进行增长率运算( (Value1 - Value2) / Value2 ) ( (今年 - 去年) / 去年 )。如果2个值中有任何一个值无效，则运算停止返回空字符串。
    /// </summary>
    /// <param name="value1">值1。一般为今年</param>
    /// <param name="value2">值2。一般为去年</param>
    /// <returns>返回 Value1 比 Value2 的增长率，如果运算数据无效，则返回 "" 字符串。</returns>
    public static string DoIncrease(object value1, object value2) {
        return DoIncrease(value1, value2, false);
    }

    /// <summary>
    /// 对两数进行增长率运算( (Value1 - Value2) / Value2 ) ( (今年 - 去年) / 去年 )
    /// </summary>
    /// <param name="value1">值1。一般为今年</param>
    /// <param name="value2">值2。一般为去年</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回 Value1 比 Value2 的增长率，如果运算数据无效，则返回 "" 字符串。</returns>
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
    /// 计算传入各参数的平均数。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值的平均数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoAvg(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoAvg(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行平均数运算。(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的平均数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoAvg(string value, bool doNull) {
        return DoAvg(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 计算传入各参数的平均数。
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的平均数，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 计算传入各参数的最大数。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值的最大数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMax(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMax(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行取最大数运算。(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的最大数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMax(string value, bool doNull) {
        return DoMax(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 计算传入各参数的最大数。
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的最大数，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 计算传入各参数的最小数。如果最后一个参数是 bool 值，则作为 doNull 变量处理
    /// </summary>
    /// <param name="value">各数字参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
    /// <returns>返回数值的最小数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMin(params object[] value) {
        ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
        return DoMin(ptsa.Value, ptsa.DoNull);
    }

    /// <summary>
    /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行取最小数运算。(Value1 + Value2 + Value3)
    /// </summary>
    /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的最小数，如果运算数据无效，则返回 "" 空字符串。</returns>
    public static string DoMin(string value, bool doNull) {
        return DoMin(value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), doNull);
    }

    /// <summary>
    /// 计算传入各参数的最小数。
    /// </summary>
    /// <param name="value">存放数字的数组</param>
    /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
    /// <returns>返回数值的最小数，如果运算数据无效，则返回 "" 空字符串。</returns>
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
    /// 对传入的数据进行最大、最小、平均、峰谷差、负荷率等的数据分析。
    /// </summary>
    public class DoAnalysis {

        /// <summary>
        /// 对传入的数据进行最大、最小、平均、峰谷差、负荷率等的数据分析。给 Value 及 DoNull 赋值并调用 Do 方法后开始数据分析。
        /// </summary>
        public DoAnalysis() {
        }

        /// <summary>
        /// 对传入的数据进行最大、最小、平均、峰谷差、负荷率等的数据分析。如果最后一个参数是 bool 值，则作为 doNull 变量处理
        /// </summary>
        /// <param name="value">需要分析的数据参数。如果最后一个参数是 bool 值，则作为 doNull 变量处理</param>
        /// <returns></returns>
        public DoAnalysis(params object[] value) {
            ParamsToStringArray ptsa = new ParamsToStringArray(true, value);
            this.Value = ptsa.Value;
            this.DoNull = ptsa.DoNull;
            this.Do();
        }

        /// <summary>
        /// 将各数字以“,”或 " " 空格分隔串接成的字符串进行最大、最小、平均、峰谷差、负荷率等的数据分析。
        /// </summary>
        /// <param name="value">以“,”逗号或 " " 空格分隔的字符串</param>
        /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
        public DoAnalysis(string value, bool doNull) {
            this.Value = value.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            this.DoNull = doNull;
            this.Do();
        }

        /// <summary>
        /// 对传入的字符串数组数据进行最大、最小、平均、峰谷差、负荷率等的数据分析。
        /// </summary>
        /// <param name="value">存放数字的字符串数组</param>
        /// <param name="doNull">如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。</param>
        public DoAnalysis(string[] value, bool doNull) {
            this.Value = value;
            this.DoNull = doNull;
            this.Do();
        }

        /// <summary>
        /// 开始进行数据分析。当初始化 DoAnalysis 类而不传任何参数时，需要手工给 Value 及 DoNull 赋值并调用 Do 方法开始数据分析；如果初始化类并传入参数，则自动调用此方法。
        /// </summary>
        public void Do() {
            this.Availability = false;
            this.MaxIndex = -1;
            this.MinIndex = -1;

            try {
                int intIndex;
                decimal decValueMax = 0;                                    //最大值
                decimal decValueMin = 0;                                    //最小值
                decimal decValueSum = 0;                                    //合计值

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
                    this.MaxValue = decValueMax.ToString();                                             //最大值
                    this.MinValue = decValueMin.ToString();                                             //最小值
                    this.SumValue = decValueSum.ToString();                                             //合计值
                    this.AvgValue = Convert.ToString(decValueSum / this.CountValid);                    //平均值
                    this.Loadfact = Convert.ToString((decValueSum / this.CountValid) / decValueMax);    //负荷率
                    this.MaxMinDiff = Convert.ToString(decValueMax - decValueMin);                      //峰谷差
                    this.MaxMinRate = Convert.ToString((decValueMax - decValueMin) / decValueMax);      //峰谷差率

                    decimal decTempValueMax = decValueMax;
                    decimal decTempValueMin = decValueMin;
                    decimal decDiscrepant = (decValueMax - decValueMin) / 10;                           //最大最小值允许调整的范围
                    decimal decDiscrepantMax = decValueMax + decDiscrepant;
                    decimal decDiscrepantMin = decValueMin - decDiscrepant;
                    decimal decAddValue = 1;                                                            //计算每次尝试增加的值，如果有小数则应该从最小位数开始

                    while ((decTempValueMax % 1) != 0) {
                        decTempValueMax *= 10;
                        decAddValue /= 10;
                    }
                    while ((decValueMax + decAddValue) < decDiscrepantMax)                              //如果最大值加上增加值小于最大的允许值
                    {
                        decValueMax = (int)(decValueMax / decAddValue) * decAddValue;                   //将基本值运算过的部分改为0
                        decTempValueMax = decValueMax + decAddValue;
                        decAddValue = decAddValue * 10;
                    }
                    this.MaxLimit = decTempValueMax.ToString();

                    decAddValue = 1;                                                                    //计算每次尝试减少的值，如果有小数则应该从最小位数开始
                    while ((decTempValueMin % 1) != 0) {
                        decTempValueMin *= 10;
                        decAddValue /= 10;
                    }
                    while ((decValueMin - decAddValue) > decDiscrepantMin)                              //如果最大值加上增加值小于最大的允许值
                    {
                        decValueMin = (int)(decValueMin / decAddValue) * decAddValue;                   //将基本值运算过的部分改为0
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
        /// 参与数据分析的数据字符串集合
        /// </summary>
        public string[] Value {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private bool m_DoNull;

        /// <summary>
        /// 如果传入的某个值是 DBNull 或是 "" 或者不是数字，该运算是否还有效。True：运算有效，运算将继续进行，并视此无效数据为“0”，只有当所有参与运算的值都不是数字，或者将此值视为“0”发生了运算错误时，才返回 "" 空字符串；False：运算无效，只要一遇到不是数字的值，便立即返回 "" 字符串。
        /// </summary>
        public bool DoNull {
            get { return m_DoNull; }
            set { m_DoNull = value; }
        }

        private bool m_Availability;

        /// <summary>
        /// 返回的结果是否有效。True：数据有效；False：数据无效。
        /// </summary>
        public bool Availability {
            get { return m_Availability; }
            set { m_Availability = value; }
        }

        private string m_MaxValue;

        /// <summary>
        /// 返回一组数值中的最大值
        /// </summary>
        public string MaxValue {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        private string m_MinValue;

        /// <summary>
        /// 返回一组数值中的最小值
        /// </summary>
        public string MinValue {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        private string m_MaxLimit;

        /// <summary>
        /// 返回一组数值中的最大值的边界值。（比最大值还要大的接近整数的值）
        /// </summary>
        public string MaxLimit {
            get { return m_MaxLimit; }
            set { m_MaxLimit = value; }
        }

        private string m_MinLimit;

        /// <summary>
        /// 返回一组数值中的最小值的边界值。比最小值还要小的接近整数的值。
        /// </summary>
        public string MinLimit {
            get { return m_MinLimit; }
            set { m_MinLimit = value; }
        }

        private int m_MaxIndex;

        /// <summary>
        /// 最大值在数组中的位置（从0起始）。当 Availability 为 False 时，该值无效为“-1”。
        /// </summary>
        public int MaxIndex {
            get { return m_MaxIndex; }
            set { m_MaxIndex = value; }
        }

        private int m_MinIndex;

        /// <summary>
        /// 最小值在数组中的位置（从0起始）。当 Availability 为 False 时，该值无效为“-1”。
        /// </summary>
        public int MinIndex {
            get { return m_MinIndex; }
            set { m_MinIndex = value; }
        }

        private string m_SumValue;

        /// <summary>
        /// 返回一组数值中的合计值。
        /// </summary>
        public string SumValue {
            get { return m_SumValue; }
            set { m_SumValue = value; }
        }

        private string m_AvgValue;

        /// <summary>
        /// 返回一组数值中的平均值。
        /// </summary>
        public string AvgValue {
            get { return m_AvgValue; }
            set { m_AvgValue = value; }
        }

        private string m_MaxMinDiff;

        /// <summary>
        /// 最大值与最小值的差值。（又名峰谷差）
        /// </summary>
        public string MaxMinDiff {
            get { return m_MaxMinDiff; }
            set { m_MaxMinDiff = value; }
        }

        private string m_MaxMinRate;

        /// <summary>
        /// 最大值与最小值的差值比率。（又名峰谷差率，计算方法：峰谷差 / 峰值）
        /// </summary>
        public string MaxMinRate {
            get { return m_MaxMinRate; }
            set { m_MaxMinRate = value; }
        }

        private string m_Loadfact;

        /// <summary>
        /// 负荷率（计算方法：平均值 / 最大值）
        /// </summary>
        public string Loadfact {
            get { return m_Loadfact; }
            set { m_Loadfact = value; }
        }

        private int m_Count;

        /// <summary>
        /// 返回传入的一组数字、参数或字符串数字的个数。当 Availability 为 False 时，该值无效。
        /// </summary>
        public int Count {
            get { return m_Count; }
            set { m_Count = value; }
        }

        private int m_CountValid;

        /// <summary>
        /// 返回一组数值中数字的个数（文本、"" 空、Null等不能转为数字的忽略不计）。当 Availability 为 False 时，该值无效。
        /// </summary>
        public int CountValid {
            get { return m_CountValid; }
            set { m_CountValid = value; }
        }
    }

    #endregion 数学运算函数
    
    #region 统计查询条件

    public static string AreaIdWhere(string strAreaId) {
        try {
            int _intAreaid = SysFun.ToInt(strAreaId), _intAreaIdMax = 0;
            string _strWhere = "";
            if (_intAreaid % 10000 == 0) //省级
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

    #endregion 统计查询条件

    #region 字符串函数

    /// <summary>
    /// 判断是否可以执行
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
    /// 确保将传入的对象转为字符串，并除去两头空格。
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
    /// 字符串比较。如果2个字符串一样则返回“true”（包括一样的内容或者2个字符串都是0长度字符串或者都是 null 值。比较时将忽略字符串两头的空格），否则返回“false”
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
    /// 计算传入的字符串的字节长度
    /// </summary>
    /// <param name="value">欲计算字节长度的字符串</param>
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
    /// 将输入的汉字转为汉字拼音首字母连接的字符串
    /// </summary>
    /// <param name="value">欲转成汉字拼音首字母的汉字字符串</param>
    /// <param name="isCapital">是否大写。true：大写；false：小写</param>
    /// <param name="notChineseDisplay">非中文汉字是否显示。true：显示；false：不显示</param>
    /// <param name="replaceNotChineseDisplay"></param>
    /// <returns></returns>
    public static string GetChineseSpell(object value, bool isCapital, bool notChineseDisplay) {
        string strSpell = "";
        try {
            char[] charValue = Convert.ToString(value).ToCharArray();

            for (int intIndex = 0; intIndex < charValue.Length; intIndex++) {
                byte[] bytChar = System.Text.Encoding.Default.GetBytes(charValue, intIndex, 1);

                if (bytChar.Length > 1) {
                    //有时间把下面的代码再好好看看
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
    /// 字符串截取与填充
    /// </summary>
    /// <param name="value">欲处理的字符串</param>
    /// <param name="interceptLength">截取长度。返回的字符串字节长度一定不超过该值。如果该值为“0”则不进行截取操作</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int interceptLength) {
        return StringInterceptFill(value, interceptLength, 0, "", false);
    }

    /// <summary>
    /// 字符串截取与填充
    /// </summary>
    /// <param name="value">欲处理的字符串</param>
    /// <param name="fillLength">填充长度。当字符串的字节长度没有达到该值则使用 fillValue 进行填充，使整个字符串的字节长度小于等于指定的填充长度。如果该值为“0”则不进行填充操作</param>
    /// <param name="fillValue">填充值。当填充值为空时将使用空格代替</param>
    /// <param name="fillRight">填充位置。true：填充在字符串的右侧； false：填充在字符串的左侧</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int fillLength, string fillValue, bool fillRight) {
        return StringInterceptFill(value, 0, fillLength, fillValue, fillRight);
    }

    /// <summary>
    /// 字符串截取与填充。该函数将先执行截取操作，然后再执行填充操作。
    /// </summary>
    /// <param name="value">欲处理的字符串</param>
    /// <param name="interceptLength">截取长度。返回的字符串字节长度一定不超过该值。如果该值为“0”则不进行截取操作</param>
    /// <param name="fillLength">填充长度。当字符串的字节长度没有达到该值则使用 fillValue 进行填充，使整个字符串的字节长度小于等于指定的填充长度。如果该值为“0”则不进行填充操作</param>
    /// <param name="fillValue">填充值。当填充值为空时将使用空格代替</param>
    /// <param name="fillRight">填充位置。true：填充在字符串的右侧； false：填充在字符串的左侧</param>
    /// <returns></returns>
    public static string StringInterceptFill(object value, int interceptLength, int fillLength, string fillValue, bool fillRight) {
        string strVaue = "";
        try {
            if (value == null) {
                strVaue = "";
            } else {
                strVaue = Convert.ToString(value);
            }

            //截取操作
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

            //填充操作
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
    /// 数字转为汉字后的样式
    /// </summary>
    public enum CharacterStyle {

        /// <summary>
        /// 汉字的“一二三”
        /// </summary>
        Character = 0,

        /// <summary>
        /// 大写的“壹贰叁”
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
                    strChar = new string[] { "○", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
                    break;

                case CharacterStyle.Capitalization:
                    strChar = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
                    break;
            }

            foreach (System.Char chrValue in (string)value) {
                if (chrValue == 45) {
                    strValue += "负";
                } else if (chrValue == 46) {
                    strValue += "点";
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
    /// **********该函数未做好**********将输入的数字转为26进制由英文字母表示的字符串。从1开始
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string IntToAlphabet(int value) {
        //**********该函数未做好**********
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
    /// 输出字符到页面或控件上
    /// </summary>
    /// <param name="value">输入的字符串</param>
    /// <returns>处理后的字符串</returns>
    public static string OutStr(object value) {
        return OutStr(value, "", "", "");
    }

    /// <summary>
    /// 输出字符到页面或控件上
    /// </summary>
    /// <param name="value">输入的字符串</param>
    /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
    /// <returns>处理后的字符串</returns>
    public static string OutStr(object value, string replaceNull) {
        return OutStr(value, replaceNull, "", "");
    }

    /// <summary>
    /// 输出字符到页面或控件上
    /// </summary>
    /// <param name="value">输入的字符串</param>
    /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
    /// <param name="postfix">后缀</param>
    /// <returns>处理后的字符串</returns>
    public static string OutStr(object value, string replaceNull, string postfix) {
        return OutStr(value, replaceNull, "", postfix);
    }

    /// <summary>
    /// 输出字符到页面或控件上
    /// </summary>
    /// <param name="value">输入的字符串</param>
    /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>处理后的字符串</returns>
    public static string OutStr(object value, string replaceNull, string prefix, string postfix) {
        try {
            if (value == null) {
                return replaceNull;
            } else {
                if (Convert.ToString(value).TrimEnd(new char[] { ' ', '　', '\t' }).Length > 0) {
                    return prefix + Convert.ToString(value).TrimEnd(new Char[] { ' ', '　', '\t' }) + postfix;
                } else {
                    return replaceNull;
                }
            }
        } catch {
            return replaceNull;
        }
    }

    /// <summary>
    /// 将输入的字符串转为适合 HTML 页面显示的内容
    /// </summary>
    /// <param name="value">HTML 内容</param>
    /// <returns></returns>
    public static string OutHtml(object value) {
        return OutHtml(value, "", "", "");
    }

    /// <summary>
    /// 将输入的字符串转为适合 HTML 页面显示的内容
    /// </summary>
    /// <param name="value">HTML 内容</param>
    /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
    /// <returns></returns>
    public static string OutHtml(object value, string replaceNull) {
        return OutHtml(value, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的字符串加上前缀、后缀后转为适合 HTML 页面显示的内容
    /// </summary>
    /// <param name="value">HTML 内容</param>
    /// <param name="replaceNull">如果输入的字符串为 DBNull 或“空”时返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
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
    /// 移除所有 HTML 标记。例如“&lt;p align=&quot;center&quot;&gt;居中&lt;/p&gt;”将返回“居中”
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
    /// 对 URL 字符串进行编码。
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
    /// 对 URL 字符串进行解码
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
    /// 获取 HTML 对象的属性值。例如“&lt;input name='key' value='123'&gt;”指定属性名称为“value”可以获得返回值“123”
    /// </summary>
    /// <param name="htmlTag">一个 HTML 对象的完整标签</param>
    /// <param name="attributeName">欲获取的属性名称</param>
    /// <returns></returns>
    public static string GetHtmlTagAttributeValue(object htmlTag, string attributeName) {
        try {
            string value = Convert.ToString(htmlTag);

            System.Text.RegularExpressions.Match matSign = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*\\S", System.Text.RegularExpressions.RegexOptions.IgnoreCase);      //查找值是用单引号、双引号或没有引号括起来的

            string strSign = matSign.Value.Substring(matSign.Value.Length - 1, 1);                              //查找“value = ”后面的第一个字符
            if (strSign == "\"" || strSign == "\'") {
                System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*" + strSign + "(.|\n)*?" + strSign, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                int intStartIndex = matValue.Value.IndexOf(strSign) + 1;
                return matValue.Value.Substring(intStartIndex, matValue.Value.LastIndexOf(strSign) - intStartIndex);
            } else {
                System.Text.RegularExpressions.Match matState = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*", System.Text.RegularExpressions.RegexOptions.IgnoreCase);       //取前导字符，以便替换掉内容开头的部分
                System.Text.RegularExpressions.Match matValue = System.Text.RegularExpressions.Regex.Match(value, attributeName + "\\s*=\\s*\\S*\\s*?[^>]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return matValue.Value.Substring(matState.Length).TrimEnd();
            }
        } catch {
            return "";
        }
    }

    #endregion 字符串函数

    #region SQL 函数

    /// <summary>
    /// 处理字符串，为数据库的 Insert 或 UpDate 做准备
    /// </summary>
    /// <param name="value">准备存入字符型字段的字符串</param>
    /// <returns></returns>
    public static string SqlStr(object value) {
        return SqlStr(value, 0, null);
    }

    /// <summary>
    /// 处理字符串并按指定长度进行截取，为数据库的 Insert 或 UpDate 做准备
    /// </summary>
    /// <param name="value">准备存入字符型字段的字符串</param>
    /// <param name="length">该字符串的限定长度，如果设置为“0”则表示不限定长度，否则如果字符串的字节长度超出该值则进行截取。</param>
    /// <returns></returns>
    public static string SqlStr(object value, int length) {
        return SqlStr(value, length, null);
    }

    /// <summary>
    /// 处理字符串（如果 value 参数为空则使用 replaceNull 替代）。为数据库的 Insert 或 UpDate 做准备
    /// </summary>
    /// <param name="value">准备存入字符型字段的字符串</param>
    /// <param name="replaceNull">如果准备存入数据库的字符串是空，则使用此值替代</param>
    /// <returns></returns>
    public static string SqlStr(object value, string replaceNull) {
        return SqlStr(value, 0, replaceNull);
    }

    /// <summary>
    /// 处理字符串并按指定长度进行截取（如果 value 参数为空则使用 replaceNull 替代）。为数据库的 Insert 或 UpDate 做准备
    /// </summary>
    /// <param name="value">准备存入字符型字段的字符串</param>
    /// <param name="length">该字符串的限定长度，如果设置为“0”则表示不限定长度，否则如果字符串的字节长度超出该值则进行截取。</param>
    /// <param name="replaceNull">如果准备存入数据库的字符串是空，则使用此值替代</param>
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
    /// 格式化数字为 Int 型，不乘以倍率，如果传入数字无效以“Null”替代，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <returns></returns>
    public static string SqlNum(object value) {
        return SqlNum(value, 0, SqlNumericType.Int, 0, 0, null);
    }

    /// <summary>
    /// 格式化数字为 Int 型并将此数字乘以一个倍率，如果传入数字无效以“Null”替代，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple) {
        return SqlNum(value, multiple, SqlNumericType.Int, 0, 0, null);
    }

    /// <summary>
    /// 格式化数字并将此数字乘以一个倍率，如果传入数字无效以“Null”替代，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple, SqlNumericType numericType) {
        return SqlNum(value, multiple, numericType, 0, 0, null);
    }

    /// <summary>
    /// 格式化数字为指定的数据类型并将此数字乘以一个倍率，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
    /// <returns></returns>
    public static string SqlNum(object value, double multiple, SqlNumericType numericType, string replaceNull) {
        return SqlNum(value, multiple, numericType, 0, 0, replaceNull);
    }

    /// <summary>
    /// 格式化数字为指定的数据类型，如果传入数字无效以“Null”替代，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType) {
        return SqlNum(value, 0, numericType, 0, 0, null);
    }

    /// <summary>
    /// 格式化数字为指定的数据类型，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType, string replaceNull) {
        return SqlNum(value, 0, numericType, 0, 0, replaceNull);
    }

    /// <summary>
    /// 格式化数字为指定的数据类型，如果传入数字无效以“Null”替代，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <param name="numericLength">当 numericType = Numeric 时，表示需要将传入的数据转为 Decimal 型，此属性指定 Value * multiple 后的最大长度（含小数部分）</param>
    /// <param name="decimalLength">当 numericType = Numeric 时，此属性指定小数部分的长度，其整数部分长度为 numericLength - decimalLength</param>
    /// <returns></returns>
    public static string SqlNum(object value, SqlNumericType numericType, int numericLength, int decimalLength) {
        return SqlNum(value, 0, numericType, numericLength, decimalLength, null);
    }

    /// <summary>
    /// 格式化数字并将此数字乘以一个倍率，为Insert或UpDate做准备。
    /// </summary>
    /// <param name="value">准备存入数据库的</param>
    /// <param name="multiple">倍率，如果输入的数字有效时将 value 乘以此数字。如果此参数输入“0”则表示不进行倍率运算。</param>
    /// <param name="numericType">数据类型。表示该字段对应数据库内的数据类型。在存入数据库前该函数将做类型转换，遇到无法转换的数字将使用 replaceNull 替换</param>
    /// <param name="numericLength">当 numericType = Numeric 时，表示需要将传入的数据转为 Decimal 型，此属性指定 Value * multiple 后的最大长度（含小数部分）</param>
    /// <param name="decimalLength">当 numericType = Numeric 时，此属性指定小数部分的长度，其整数部分长度为 numericLength - decimalLength</param>
    /// <param name="replaceNull">如果输入的数据无效或者类型转换时发生错误，将使用该值替代。默认情况下该值应为“Null”，以表示在数据库中存入一个空值。</param>
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
                            //不做处理，运行到函数最后的“return”函数
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
        /// 不判断数据类型
        /// </summary>
        None = 0,

        /// <summary>
        /// 从 0 到 255 的整数数据。（Byte）
        /// </summary>
        TinyInt = 1,

        /// <summary>
        /// 从 -2^15 (-32,768) 到 2^15 - 1 (32,767) 的整型数据。（Short、Int16）
        /// </summary>
        SmallInt = 2,

        /// <summary>
        /// 从 -2^31 (-2,147,483,648) 到 2^31 - 1 (2,147,483,647) 的整型数据。（Int、Integer、Int32）
        /// </summary>
        Int = 3,

        /// <summary>
        /// 从 -2^63 (-9,223,372,036,854,775,808) 到 2^63-1 (9,223,372,036,854,775,807) 的整型数据。（Long、Int64）
        /// </summary>
        BigInt = 4,

        /// <summary>
        /// 从 -10^38 +1 到 10^38 –1 的固定精度和小数位的数字数据。（Decimal）
        /// </summary>
        Numeric = 5
    }

    #endregion SQL 函数

    #region 日期时间函数

    /// <summary>
    /// 系统内的日期格式
    /// </summary>
    public enum DateStyle {

        /// <summary>
        /// 自动识别该日期格式
        /// </summary>
        Automatism = 0,

        /// <summary>
        /// 4位年，2位月，2位日。例如：20010310（2001年3月10日）
        /// </summary>
        YYYYMMDD = 1,

        /// <summary>
        /// 4位年，2位月，2位零。例如：20010300（2001年3月）
        /// </summary>
        YYYYMM00 = 2,

        /// <summary>
        /// 4位年，2位周，2位零。例如：20010300（2001年第三周）
        /// </summary>
        YYYYWW00 = 3,

        /// <summary>
        /// 4位年，2位季度，2位零。例如：20010300（2001年第三季度）
        /// </summary>
        YYYYQQ00 = 4,

        /// <summary>
        /// 4位年，4位零。例如：20010000（2001年）
        /// </summary>
        YYYY0000 = 5
    }

    /// <summary>
    /// 输出的日期样式
    /// </summary>
    public enum OutDateStyle {

        /// <summary>
        /// 输出样式：YYYY-MM-DD
        /// </summary>
        /// <remarks></remarks>
        YMD_Sign = 1,

        /// <summary>
        /// 输出样式：YYYY年MM月DD日
        /// </summary>
        /// <remarks></remarks>
        YMD_Num = 2,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年ＭＭ月ＤＤ日 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YMD_Char = 3,

        /// <summary>
        /// 输出样式：YYYY年MM月DD日 星期Ｗ （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YMDW_Num = 4,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年ＭＭ月ＤＤ日 星期Ｗ （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YMDW_Char = 5,

        /// <summary>
        /// 输出样式：YYYY年 第N周
        /// </summary>
        /// <remarks></remarks>
        YW_Num = 6,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年 第Ｎ周 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YW_Char = 7,

        /// <summary>
        /// 输出样式：YYYY年 第N周(MM月DD日 - MM月DD日)
        /// </summary>
        /// <remarks></remarks>
        YWmd_Num = 8,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年 第Ｎ周(MM月DD日 - MM月DD日) （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YWmd_Char = 9,

        /// <summary>
        /// 输出样式：YYYY-MM
        /// </summary>
        /// <remarks></remarks>
        YM_Sign = 10,

        /// <summary>
        /// 输出样式：YYYY年MM月
        /// </summary>
        /// <remarks></remarks>
        YM_Num = 11,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年ＭＭ月 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YM_Char = 12,

        /// <summary>
        /// 输出样式：YYYY年 第Q季度
        /// </summary>
        /// <remarks></remarks>
        YQ_Num = 13,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年 第Ｑ季度 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        YQ_Char = 14,

        /// <summary>
        /// 输出样式：YYYY年
        /// </summary>
        /// <remarks></remarks>
        Y_Num = 15,

        /// <summary>
        /// 输出样式：ＹＹＹＹ年 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        Y_Char = 16,

        /// <summary>
        /// 输出样式：MM月DD日
        /// </summary>
        /// <remarks></remarks>
        MD_Num = 17,

        /// <summary>
        /// 输出样式：ＭＭ月ＤＤ日 （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        MD_Char = 18,

        /// <summary>
        /// 输出样式：星期Ｗ （其中全角的字母表示汉字文字的数字）
        /// </summary>
        /// <remarks></remarks>
        W_Char = 19,
    }

    /// <summary>
    /// 输出的时间样式
    /// </summary>
    public enum OutTimeStyle {

        /// <summary>
        /// 输出样式：HH:MM:SS
        /// </summary>
        HMS_Sign = 1,

        /// <summary>
        /// 输出样式：HH时MM分SS秒
        /// </summary>
        HMS_Num = 2,

        /// <summary>
        /// 输出样式： ＨＨ时ＭＭ分ＳＳ秒
        /// </summary>
        HMS_Char = 3,

        /// <summary>
        /// 输出样式：AM/PM HH:MM:SS
        /// </summary>
        THMS_Sign = 4,

        /// <summary>
        /// 输出样式：AM/PM HH时MM分SS秒
        /// </summary>
        THMS_Num = 5,

        /// <summary>
        /// 输出样式：上午/下午 ＨＨ时ＭＭ分ＳＳ秒
        /// </summary>
        THMS_Char = 6,

        /// <summary>
        /// 输出样式：HH:MM
        /// </summary>
        HM_Sign = 7,

        /// <summary>
        /// 输出样式：HH时MM分
        /// </summary>
        HM_Num = 8,

        /// <summary>
        /// 输出样式：ＨＨ时ＭＭ分
        /// </summary>
        HM_Char = 9,

        /// <summary>
        /// 输出样式：AM/PM HH:MM
        /// </summary>
        THM_Sign = 10,

        /// <summary>
        /// 输出样式：AM/PM HH时MM分
        /// </summary>
        THM_Num = 11,

        /// <summary>
        /// 输出样式：上午/下午 ＨＨ时ＭＭ分
        /// </summary>
        THM_Char = 12,

        /// <summary>
        /// 输出样式：HH时
        /// </summary>
        H_Num = 13,

        /// <summary>
        /// 输出样式：ＨＨ时
        /// </summary>
        H_Char = 14,

        /// <summary>
        /// 输出样式：AM/PM HH时
        /// </summary>
        TH_Num = 15,

        /// <summary>
        /// 输出样式：上午/下午 ＨＨ时
        /// </summary>
        TH_Char = 16,
    }

    /// <summary>
    /// 指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式。
    /// </summary>
    public enum DateInterval {

        /// <summary>
        /// 秒
        /// </summary>
        Second,

        /// <summary>
        /// 分钟
        /// </summary>
        Minute,

        /// <summary>
        /// 小时
        /// </summary>
        Hour,

        /// <summary>
        /// 天
        /// </summary>
        Day,

        /// <summary>
        /// 星期
        /// </summary>
        Weekday,

        /// <summary>
        /// 月
        /// </summary>
        Month,

        /// <summary>
        /// 季度
        /// </summary>
        Quarter,

        /// <summary>
        /// 年
        /// </summary>
        Year
    }

    /// <summary>
    /// 表示 8 位整型日期和 6 位整型时间的结构
    /// </summary>
    public struct IntDateTime {
        private int m_DateNum;

        /// <summary>
        /// 8位的整型日期
        /// </summary>
        public int DateNum {
            get { return m_DateNum; }
            set { m_DateNum = value; }
        }

        private int m_TimeNum;

        /// <summary>
        /// 6位的整型时间
        /// </summary>
        public int TimeNum {
            get { return m_TimeNum; }
            set { m_TimeNum = value; }
        }
    }

    /// <summary>
    /// 获取系统整型日期值
    /// </summary>
    /// <returns></returns>
    public static int GetIntDate() {
        DateTime dtmValue = DateTime.Today;
        return dtmValue.Year * 10000 + dtmValue.Month * 100 + dtmValue.Day;
    }

    /// <summary>
    /// 获取系统整型时间值
    /// </summary>
    /// <returns></returns>
    public static int GetIntTime() {
        DateTime dtmValue = DateTime.Now;
        return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
    }

    /// <summary>
    /// 将传入的日期转为 YYYYMMDD 格式的整型日期。函数将自动判断传入的参数是日期型还是 8 位整型日期。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToIntDate(object value) {
        try {
            if (IsNumeric(value))                                           //尝试转为数字型
            {
                return ToIntDate(Convert.ToInt32(value));
            } else {                                                               //尝试转为日期型
                DateTime dtmValue = Convert.ToDateTime(value);
                return ToIntDate(dtmValue, DateStyle.YYYYMMDD);
            }
        } catch {
            return 0;
        }
    }

    /// <summary>
    /// 将传入的日期转为 YYYYMMDD 格式的整型日期
    /// </summary>
    /// <param name="dateTime">DateTime 型的日期</param>
    /// <returns></returns>
    public static int ToIntDate(DateTime dateTime) {
        return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, DateStyle.YYYYMMDD);
    }

    /// <summary>
    /// 将传入的日期转为指定格式的整型日期
    /// </summary>
    /// <param name="dateTime">DateTime 型日期时间</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
    /// <returns></returns>
    public static int ToIntDate(DateTime dateTime, DateStyle outDateStyle) {
        return ToIntDate(dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day, DateStyle.YYYYMMDD, outDateStyle);
    }

    /// <summary>
    /// 自动判断传入的整型日期格式并转换为 YYYYMMDD 格式的整型日期
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
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
    /// 指定传入的整型日期格式并转为 YYYYMMDD 格式的日期
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的整型日期格式</param>
    /// <returns></returns>
    public static int ToIntDate(int dateNum, DateStyle inDateStyle) {
        return ToIntDate(dateNum, inDateStyle, DateStyle.YYYYMMDD);
    }

    /// <summary>
    /// 将传入的指定格式的整型日期转为指定格式的整型日期
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的整型日期格式</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
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
    /// 将传入的日期转为 HHMMSS 格式的整型时间。函数将自动判断传入的参数是日期型还是 6 位整型时间，如果是 6 位整型时间此函数还将格式化返回时间的正确性。
    /// </summary>
    /// <param name="value">8 位整型日期或者 DateTime 型的日期</param>
    /// <returns></returns>
    public static int ToIntTime(object value) {
        try {
            if (IsNumeric(value))                                           //尝试转为数字型
            {
                int intValue = Convert.ToInt32(value);
                DateTime dtmValue = new DateTime();
                dtmValue = dtmValue.AddSeconds(intValue % 100);
                dtmValue = dtmValue.AddMinutes((intValue / 100) % 100);
                dtmValue = dtmValue.AddHours(intValue / 10000);
                return dtmValue.Hour * 10000 + dtmValue.Minute * 100 + dtmValue.Second;
            } else {                                                               //尝试转为日期型
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
    /// 按指定日期样式将输入的整型日期转为日期型日期
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum) {
        return ToDateTime(dateNum, 0, DateStyle.Automatism);
    }

    /// <summary>
    /// 自动判断日期样式，并将输入的整型日期、时间转为日期型日期时间
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="timeNum">6位整型时间</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum, int timeNum) {
        return ToDateTime(dateNum, timeNum, DateStyle.Automatism);
    }

    /// <summary>
    /// 按指定日期样式将输入的整型日期转为日期型日期
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的整型日期格式</param>
    /// <returns></returns>
    public static DateTime ToDateTime(int dateNum, DateStyle inDateStyle) {
        return ToDateTime(dateNum, 0, inDateStyle);
    }

    /// <summary>
    /// 按指定日期样式将输入的整型日期、时间转为日期型日期时间
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="inDateStyle">输入的整型日期格式</param>
    /// <returns>日期型日期</returns>
    public static DateTime ToDateTime(int dateNum, int timeNum, DateStyle inDateStyle) {
        DateTime dtm = new DateTime();

        if (dateNum > 10000 && dateNum <= 99991231) {
            //自动判断日期类型
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
                    dtm = new DateTime((dateNum / 10000), 1, 1);                                         //该年度1月1日的日期
                    int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1月1日距周的起始日相差几天
                    dtm = dtm.AddDays(-intFirstDayOfWeek + (((dateNum / 100) % 100) - 1) * 7);           //将日期改变成1月1日所在周的起始日的日期，并增加指定的周数
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
                    dtm = new DateTime((dateNum / 10000), 1, 1);                                         //该年度1月1日的日期
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
    /// 自动判断传入的整型日期格式，并计算增减指定值后的整型日期，结果以 YYYYMMDD 格式 IntDateTime 结构返回。
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="dateNum">8 位整型日期</param>
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
    /// 计算整型日期增减指定值后的整型日期，结果以 IntDateTime 结构返回。
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="dateNum">8 位整型日期</param>
    /// <param name="inDateStyle">输入的整型日期格式</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int dateNum, DateStyle inDateStyle, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, dateNum, inDateStyle, outDateStyle, 0);
    }

    /// <summary>
    /// 计算整型时间增减指定值后的整型时间，结果以 IntDateTime 结构返回。此函数不考虑 dateNum 参数，可以传入 null
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="dateNum">传入 null</param>
    /// <param name="timeNum">6 位整型时间</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, int? dateNum, int timeNum) {
        return DoDateAdd(interval, addNumber, 0, DateStyle.YYYYMMDD, null, timeNum);
    }

    /// <summary>
    /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="dateTime">DateTime 格式的日期时间</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, DateTime dateTime, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, ToIntDate(dateTime, DateStyle.YYYYMMDD), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime));
    }

    /// <summary>
    /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="intDateTime">IntDateTime 格式的日期时间</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
    /// <returns></returns>
    public static IntDateTime DoDateAdd(DateInterval interval, int addNumber, IntDateTime intDateTime, DateStyle outDateStyle) {
        return DoDateAdd(interval, addNumber, intDateTime.DateNum, DateStyle.YYYYMMDD, outDateStyle, intDateTime.TimeNum);
    }

    /// <summary>
    /// 计算整型日期时间增减指定值后的整型日期时间，结果以 IntDateTime 结构返回。
    /// </summary>
    /// <param name="interval">设置日期或时间间隔的格式</param>
    /// <param name="addNumber">要增减的日期或时间的间隔数据</param>
    /// <param name="dateNum">8 位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">希望输出的日期格式</param>
    /// <param name="timeNum">6 位整型时间</param>
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
    /// 判断2个日期之间相差的时间值。公式将以 dateTime2 - dateTime1 进行计算
    /// </summary>
    /// <param name="interval">指示在调用与日期相关的函数时如何确定日期间隔和设置日期间隔的格式。</param>
    /// <param name="dateTime1">日期1。通常为之前的日期</param>
    /// <param name="dateTime2">日期2。通常为之后的日期</param>
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
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, "", "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, null, null, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, "", "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(null, null, null, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, "", "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateTime">需要输出的日期型日期和时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
    public static string OutDateTime(DateTime dateTime, OutDateStyle outDateStyle, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(ToIntDate(dateTime), DateStyle.YYYYMMDD, outDateStyle, ToIntTime(dateTime), outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, "", "", "");
    }

    /// <summary>
    ///
    /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的日期按指定格式转换为适合页面显示的日期字符串
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的日期字符串</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, null, null, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, "", "", "");
    }

    /// <summary>
    /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的时间按指定格式转换为适合页面显示的时间字符串
    /// </summary>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的时间字符串</returns>
    public static string OutDateTime(object timeNum, OutTimeStyle outTimeStyle, string replaceNull, string prefix, string postfix) {
        return OutDateTime(null, null, null, timeNum, outTimeStyle, replaceNull, prefix, postfix);
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
    public static string OutDateTime(object dateNum, DateStyle inDateStyle, OutDateStyle outDateStyle, object timeNum, OutTimeStyle outTimeStyle) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, "", "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
    public static string OutDateTime(object dateNum, DateStyle? inDateStyle, OutDateStyle? outDateStyle, object timeNum, OutTimeStyle? outTimeStyle, string replaceNull) {
        return OutDateTime(dateNum, inDateStyle, outDateStyle, timeNum, outTimeStyle, replaceNull, "", "");
    }

    /// <summary>
    /// 将输入的日期、时间按指定格式转换为适合页面显示的日期、时间字符串
    /// </summary>
    /// <param name="dateNum">8位整型日期</param>
    /// <param name="inDateStyle">输入的日期格式</param>
    /// <param name="outDateStyle">输出的日期样式</param>
    /// <param name="timeNum">6位整型时间</param>
    /// <param name="outTimeStyle">输出的时间样式</param>
    /// <param name="replaceNull">如果传入的参数为空是返回的值</param>
    /// <param name="prefix">前缀</param>
    /// <param name="postfix">后缀</param>
    /// <returns>适合页面显示的日期时间字符串</returns>
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
                        strValue += dtm.Year + "年" + dtm.Month + "月" + dtm.Day + "日";
                        break;

                    case OutDateStyle.YMD_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日";
                        break;

                    case OutDateStyle.YMDW_Num:
                        strValue += dtm.Year + "年" + dtm.Month + "月" + dtm.Day + "日 " + GetWeekdayName(dtm.DayOfWeek);
                        break;

                    case OutDateStyle.YMDW_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日 " + GetWeekdayName(dtm.DayOfWeek);
                        break;

                    case OutDateStyle.YW_Num:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "周";
                        break;

                    case OutDateStyle.YW_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtm), CharacterStyle.Character) + "周";
                        break;

                    case OutDateStyle.YWmd_Num:
                        DateTime dtmYWmd_Num = dtm.AddDays(-(int)dtm.DayOfWeek);
                        strValue += dtmYWmd_Num.Year + "年 第" + GetWeekOfYear(dtmYWmd_Num) + "周 (" + dtmYWmd_Num.Month + "月" + dtmYWmd_Num.Day + "日－" + dtmYWmd_Num.AddDays(6).Month + "月" + dtmYWmd_Num.AddDays(6).Day + "日)";
                        break;

                    case OutDateStyle.YWmd_Char:
                        DateTime dtmYWmd_Char = dtm.AddDays(-(int)dtm.DayOfWeek);
                        strValue += ToCharacter(dtmYWmd_Char.Year, CharacterStyle.Character) + "年 第" + ToCharacter(GetWeekOfYear(dtmYWmd_Char), CharacterStyle.Character) + "周 (" + dtmYWmd_Char.Month + "月" + dtmYWmd_Char.Day + "日－" + dtmYWmd_Char.AddDays(6).Month + "月" + dtmYWmd_Char.AddDays(6).Day + "日)";
                        break;

                    case OutDateStyle.YM_Sign:
                        strValue += dtm.Year + "-" + OutDateTimeNum(dtm.Month);
                        break;

                    case OutDateStyle.YM_Num:
                        strValue += dtm.Year + "年" + dtm.Month + "月";
                        break;

                    case OutDateStyle.YM_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年" + ToCharacter(dtm.Month, CharacterStyle.Character) + "月";
                        break;

                    case OutDateStyle.YQ_Num:
                        strValue += dtm.Year + "年 第" + ((dtm.Month + 2) / 3) + "季度";
                        break;

                    case OutDateStyle.YQ_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年 第" + ToCharacter(((dtm.Month + 2) / 3), CharacterStyle.Character) + "季度";
                        break;

                    case OutDateStyle.Y_Num:
                        strValue += dtm.Year + "年";
                        break;

                    case OutDateStyle.Y_Char:
                        strValue += ToCharacter(dtm.Year, CharacterStyle.Character) + "年";
                        break;

                    case OutDateStyle.MD_Num:
                        strValue += dtm.Month + "月" + dtm.Day + "日";
                        break;

                    case OutDateStyle.MD_Char:
                        strValue += ToCharacter(dtm.Month, CharacterStyle.Character) + "月" + ToCharacter(dtm.Day, CharacterStyle.Character) + "日";
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
                        strValue += dtm.Hour + "时" + OutDateTimeNum(dtm.Minute) + "分" + OutDateTimeNum(dtm.Second) + "秒";
                        break;

                    case OutTimeStyle.HMS_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分" + ToCharacter(dtm.Second, CharacterStyle.Character) + "秒";
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
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += (dtm.Hour % 12) + "时" + OutDateTimeNum(dtm.Minute) + "分" + OutDateTimeNum(dtm.Second) + "秒";
                        break;

                    case OutTimeStyle.THMS_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分" + ToCharacter(dtm.Second, CharacterStyle.Character) + "秒";
                        break;

                    case OutTimeStyle.HM_Sign:
                        strValue += dtm.Hour + ":" + OutDateTimeNum(dtm.Minute);
                        break;

                    case OutTimeStyle.HM_Num:
                        strValue += dtm.Hour + "时" + OutDateTimeNum(dtm.Minute) + "分";
                        break;

                    case OutTimeStyle.HM_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分";
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
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += (dtm.Hour % 12) + "时" + OutDateTimeNum(dtm.Minute) + "分";
                        break;

                    case OutTimeStyle.THM_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时" + ToCharacter(dtm.Minute, CharacterStyle.Character) + "分";
                        break;

                    case OutTimeStyle.H_Num:
                        strValue += dtm.Hour + "时";
                        break;

                    case OutTimeStyle.H_Char:
                        strValue += ToCharacter(dtm.Hour, CharacterStyle.Character) + "时";
                        break;

                    case OutTimeStyle.TH_Num:
                        if ((dtm.Hour) < 12) {
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += (dtm.Hour % 12) + "时";
                        break;

                    case OutTimeStyle.TH_Char:
                        if ((dtm.Hour) < 12) {
                            strValue += "上午 ";
                        } else {
                            strValue += "下午 ";
                        }
                        strValue += ToCharacter(dtm.Hour % 12, CharacterStyle.Character) + "时";
                        break;
                }
            }

            return prefix + strValue + postfix;
        } catch {
            return replaceNull;
        }
    }

    /// <summary>
    /// 将小于10的数字加上前缀“0”，用于“OutDateTime”函数输出时间时使用
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns></returns>
    private static string OutDateTimeNum(int num) {
        if (num < 10) {
            return "0" + num;
        } else {
            return num.ToString();
        }
    }

    /// <summary>
    /// 获得传入的整型日期是该年中的第几周
    /// </summary>
    /// <param name="dateNum">需要计算周数的整型日期</param>
    /// <returns>该日期是该年度中的第几周</returns>
    public static int GetWeekOfYear(int dateNum) {
        DateTime dtm = new DateTime();
        dtm = dtm.AddYears((dateNum / 10000) - 1);
        dtm = dtm.AddMonths(((dateNum / 100) % 100) - 1);
        dtm = dtm.AddDays((dateNum % 100) - 1);
        return GetWeekOfYear(dtm);
    }

    /// <summary>
    /// 获得传入的日期是该年中的第几周
    /// </summary>
    /// <param name="dateTime">需要计算周数的日期</param>
    /// <returns>该日期是该年度中的第几周</returns>
    public static int GetWeekOfYear(DateTime dateTime) {
        DateTime dtm = new DateTime(dateTime.Year, 1, 1);                                                //该年度1月1日的日期
        int intFirstDayOfWeek = (int)dtm.DayOfWeek;                                             //1月1日距周的起始日相差几天
        dtm = dtm.AddDays(-intFirstDayOfWeek);                                                  //将日期改变成1月1日所在周的起始日的日期
        return ((dateTime.Subtract(dtm).Days) / 7) + 1;
    }

    /// <summary>
    /// 将传入的数字星期转为汉字星期描述。“0”代表星期日，“6”代表星期六
    /// </summary>
    /// <param name="weekday">星期的数字描述</param>
    /// <returns>汉字的星期描述</returns>
    public static string GetWeekdayName(int weekday) {
        return GetWeekdayName((System.DayOfWeek)weekday);
    }

    /// <summary>
    /// 将传入的 DayOfWeek 行星期转为汉字星期描述
    /// </summary>
    /// <param name="weekday">DayOfWeek 的星期枚举</param>
    /// <returns>汉字的星期描述</returns>
    public static string GetWeekdayName(System.DayOfWeek weekday) {
        switch (weekday) {
            case DayOfWeek.Monday:
                return "星期一";

            case DayOfWeek.Tuesday:
                return "星期二";

            case DayOfWeek.Wednesday:
                return "星期三";

            case DayOfWeek.Thursday:
                return "星期四";

            case DayOfWeek.Friday:
                return "星期五";

            case DayOfWeek.Saturday:
                return "星期六";

            default:
                return "星期日";
        }
    }

    #endregion 日期时间函数

    #region 其它函数

    /// <summary>
    /// 将传入的对象保存为 XML 格式的文件
    /// </summary>
    /// <param name="obj">需要保存查看的任意对象</param>
    /// <param name="fileName">XML 文件名</param>
    public static void ObjectToXml(object obj, string fileName) {
        if (obj == null) {
            throw new Exception("当前传入的对象为空");
        } else {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            xs.Serialize(fs, obj);
            fs.Dispose();
            fs.Close();
        }
    }

    /// <summary>
    /// 将枚举值转为键值集合
    /// 例如：SortedList sl = SysFun.EnumToSortedList(System.Type.GetType("命名空间.枚举"));（注意：“命名空间.枚举”需要用引号引起来作为字符串传入；如果该类型在另一个 dll 文件内，则写成“命名空间.枚举,命名空间”）
    /// </summary>
    /// <param name="enumType">需要转换的枚举类型，例如：SortedList sl = SysFun.EnumToSortedList(System.Type.GetType("命名空间.枚举"));（注意：“命名空间.枚举”需要用引号引起来作为字符串传入；如果该类型在另一个 dll 文件内，则写成“命名空间.枚举,命名空间”）</param>
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
    /// 在传入的字符串中搜索“***.***.***.***”格式的 IP 地址，并将搜索到的 IP 地址计算为整型数字
    /// </summary>
    /// <param name="ip">“***.***.***.***”格式的 IP 地址</param>
    /// <returns>整型数字表示的 IP 地址</returns>
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
    /// 将传入的 Int32 整型数字转为“***.***.***.***”格式的 IP 地址
    /// </summary>
    /// <param name="intIp">整型数字表示的 IP 地址</param>
    /// <returns>“***.***.***.***”格式的 IP 地址</returns>
    public static string IntToIp(int intIp) {
        long lonIp = intIp + 2147483648;

        byte bytIp1 = (byte)((lonIp / 16777216) % 256);
        byte bytIp2 = (byte)((lonIp / 65536) % 256);
        byte bytIp3 = (byte)((lonIp / 256) % 256);
        byte bytIp4 = (byte)(lonIp % 256);

        return bytIp1.ToString() + "." + bytIp2.ToString() + "." + bytIp3.ToString() + "." + bytIp4.ToString();
    }

    /// <summary>
    /// 判断是否有SQL注入，如果有返回""字符串，否则将原字符串返回。
    /// </summary>
    /// <param name="value">要检查的字串</param>
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

    #endregion 其它函数

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