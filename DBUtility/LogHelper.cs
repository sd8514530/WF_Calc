using NLog;
using System;

namespace WF_Calc.DBUtility {

    public enum LogLevel {
        Info = 1,//正常的記錄 對應日誌中的
        Error = 2//異常的記錄
    }

    public enum LogType {
        Trace = 1,//記錄執行信息
        SqlForDB = 2,//記錄SQL語句
        MxForPLC = 3,//記錄寫入寫出PLC的原始數據
        Mat = 4,//記錄物料的信息
        Secs = 5//記錄Sece原始日誌
    }

    public class LogHelper {

        public static void SaveLog(string message, LogLevel logLevel, LogType logType) {
            try {
                switch (logType) {
                    case LogType.Trace:
                        if (LogLevel.Info == logLevel) {
                            Logger loggerTrace = LogManager.GetLogger("Trace");
                            loggerTrace.Trace(message);
                        } else {
                            Logger loggerTraceErr = LogManager.GetLogger("TraceErr");
                            loggerTraceErr.Error(message);
                        }
                        break;

                    case LogType.MxForPLC:
                        if (LogLevel.Info == logLevel) {
                            Logger loggerMxForPLC = LogManager.GetLogger("MxForPLC");
                            loggerMxForPLC.Trace(message);
                        } else {
                            Logger loggerMxForPLCErr = LogManager.GetLogger("MxForPLCErr");
                            loggerMxForPLCErr.Error(message);
                        }
                        break;

                    case LogType.SqlForDB:
                        if (LogLevel.Info == logLevel) {
                            Logger loggerSqlForDB = LogManager.GetLogger("SqlForDB");
                            loggerSqlForDB.Trace(message);
                        } else {
                            Logger loggerSqlForDBErr = LogManager.GetLogger("SqlForDBErr");
                            loggerSqlForDBErr.Error(message);
                        }
                        break;

                    case LogType.Mat:
                        if (LogLevel.Info == logLevel) {
                            Logger loggerMat = LogManager.GetLogger("Mat");
                            loggerMat.Trace(message);
                        } else {
                            Logger loggerMatErr = LogManager.GetLogger("MatErr");
                            loggerMatErr.Error(message);
                        }
                        break;

                    case LogType.Secs:
                        if (LogLevel.Info == logLevel) {
                            Logger loggerSecs = LogManager.GetLogger("Secs");
                            loggerSecs.Trace(message);
                        } else {
                            Logger loggerSecsErr = LogManager.GetLogger("SecsErr");
                            loggerSecsErr.Error(message);
                        }
                        break;

                    default:
                        if (LogLevel.Info == logLevel) {
                            Logger logger = LogManager.GetLogger("Trace");
                            logger.Trace(message);
                        } else {
                            Logger logger = LogManager.GetLogger("TraceErr");
                            logger.Error(message);
                        }
                        break;
                }
            } catch (Exception) {
                throw new Exception("Save Log Error Exception");
            }
        }
    }
}