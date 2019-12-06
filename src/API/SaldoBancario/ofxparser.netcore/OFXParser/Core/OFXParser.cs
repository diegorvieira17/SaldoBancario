using OFXParser.Core;
using OFXParser.Entities;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace OFXParser
{
    public enum PartDateTime
    {
        DAY,
        MONTH,
        YEAR,
        HOUR,
        MINUTE,
        SECOND
    }

    public class OFXParser : IOFXParser
    {
        public static Extract GetExtract(string ofxSourceFile)
        {
            return new OFXParser().GenerateExtract(ofxSourceFile);
        }

        public Extract GenerateExtract(string ofxSourceFile)
        {
            // Translating to XML file
            var xmlFilePath = ofxSourceFile + ".xml";
            ExportToXml(ofxSourceFile, xmlFilePath);

            var elementoSendoLido = "";
            Transaction transacaoAtual = null;

            var header = new HeaderExtract();
            var bankAccount = new BankAccount();
            var extract = new Extract(header, bankAccount, "");

            // Lendo o XML efetivamente
            var xmlReader = new XmlTextReader(xmlFilePath);
            try
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name.Equals("STMTTRN") && transacaoAtual != null)
                    {
                        extract.AddTransaction(transacaoAtual);
                        transacaoAtual = null;
                    }
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        elementoSendoLido = xmlReader.Name;
                        if (xmlReader.Name.Equals("STMTTRN"))
                        {
                            transacaoAtual = new Transaction();
                        }
                    }
                    if (xmlReader.NodeType == XmlNodeType.Text)
                    {
                        switch (elementoSendoLido)
                        {
                            case "DTSERVER":
                                header.ServerDate = ConvertOfxDateToDateTime(xmlReader.Value, extract);
                                break;
                            case "LANGUAGE":
                                header.Language = xmlReader.Value;
                                break;
                            case "ORG":
                                header.BankName = xmlReader.Value;
                                break;
                            case "DTSTART":
                                extract.InitialDate = ConvertOfxDateToDateTime(xmlReader.Value, extract);
                                break;
                            case "DTEND":
                                extract.FinalDate = ConvertOfxDateToDateTime(xmlReader.Value, extract);
                                break;
                            case "BANKID":
                                bankAccount.Bank = new Bank(GetBankId(xmlReader.Value, extract), "");
                                break;
                            case "BRANCHID":
                                bankAccount.AgencyCode = xmlReader.Value;
                                break;
                            case "ACCTID":
                                bankAccount.AccountCode = xmlReader.Value;
                                break;
                            case "ACCTTYPE":
                                bankAccount.Type = xmlReader.Value;
                                break;
                            case "TRNTYPE":
                                transacaoAtual.Type = xmlReader.Value;
                                break;
                            case "DTPOSTED":
                                transacaoAtual.Date = ConvertOfxDateToDateTime(xmlReader.Value, extract);
                                break;
                            case "TRNAMT":
                                transacaoAtual.TransactionValue = GetTransactionValue(xmlReader.Value, extract);
                                break;
                            case "FITID":
                                transacaoAtual.Id = xmlReader.Value;
                                break;
                            case "CHECKNUM":
                                transacaoAtual.Checksum = Convert.ToInt64(xmlReader.Value);
                                break;
                            case "MEMO":
                                transacaoAtual.Description = string.IsNullOrEmpty(xmlReader.Value) ? string.Empty : Regex.Replace(xmlReader.Value.Trim(), "[^\\S\\n]+", " ");
                                break;
                        }
                    }
                }
            }
            catch (XmlException xe)
            {
                throw new OFXParserException($"Invalid OFX file", xe);
            }
            finally
            {
                xmlReader.Close();
            }
            return extract;
        }

        /// <summary>
        /// This method translate an OFX file to XML tags, independent of the content.
        /// </summary>
        /// <param name="ofxSourceFile">OFX source file</param>
        /// <returns>XML tags in StringBuilder object.</returns>
        private StringBuilder TranslateToXml(string ofxSourceFile)
        {
            var result = new StringBuilder();
            int level = 0;
            string line;

            if (!File.Exists(ofxSourceFile))
            {
                throw new FileNotFoundException("OFX source file not found: " + ofxSourceFile);
            }

            using (var sr = new StreamReader(ofxSourceFile, Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.StartsWith("</") && line.EndsWith(">"))
                    {
                        AddTabs(result, level, true);
                        level--;
                        result.Append(line);
                    }
                    else if (line.StartsWith("<") && line.EndsWith(">"))
                    {
                        level++;
                        AddTabs(result, level, true);
                        result.Append(line);
                    }
                    else if (line.StartsWith("<") && !line.EndsWith(">"))
                    {
                        AddTabs(result, level + 1, true);
                        result.Append(line);
                        result.Append(ReturnFinalTag(line));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method translate an OFX file to XML file, independent of the content.
        /// </summary>
        /// <param name="ofxSourceFile">Path of OFX source file</param>
        /// <param name="xmlNewFile">Path of the XML file, internally generated.</param>
        private void ExportToXml(string ofxSourceFile, string xmlNewFile)
        {
            if (File.Exists(ofxSourceFile))
            {
                if (xmlNewFile.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Translating the OFX file to XML format
                    var ofxTranslated = TranslateToXml(ofxSourceFile);

                    // Verifying if target file exists
                    if (File.Exists(xmlNewFile))
                    {
                        File.Delete(xmlNewFile);
                    }

                    // Writing data into target file
                    StreamWriter sw = File.CreateText(xmlNewFile);
                    sw.WriteLine(@"<?xml version=""1.0""?>");
                    sw.WriteLine(ofxTranslated.ToString());
                    sw.Close();
                }
                else
                {
                    throw new ArgumentException("Name of new XML file is not valid: " + xmlNewFile);
                }
            }
            else
            {
                throw new FileNotFoundException("OFX source file not found: " + ofxSourceFile);
            }
        }

        /// <summary>
        /// This method return the correct closing tag string 
        /// </summary>
        /// <param name="content">Content of analysis</param>
        /// <returns>string with ending tag.</returns>
        private string ReturnFinalTag(string content)
        {
            var returnFinal = string.Empty;

            if ((content.IndexOf("<") != -1) && (content.IndexOf(">") != -1))
            {
                int position1 = content.IndexOf("<");
                int position2 = content.IndexOf(">");
                if ((position2 - position1) > 2)
                {
                    returnFinal = content.Substring(position1, (position2 - position1) + 1);
                    returnFinal = returnFinal.Replace("<", "</");
                }
            }

            return returnFinal;
        }

        /// <summary>
        /// This method add tabs into lines of xml file, to best identation.
        /// </summary>
        /// <param name="stringObject">Line of content</param>
        /// <param name="lengthTabs">Length os tabs to add into content</param>
        /// <param name="newLine">Is it new line?</param>
        private void AddTabs(StringBuilder stringObject, int lengthTabs, bool newLine)
        {
            if (newLine)
            {
                stringObject.AppendLine();
            }
            for (int j = 1; j < lengthTabs; j++)
            {
                stringObject.Append("\t");
            }
        }

        /// <summary>
        /// Method that return a part of date. Is is used internally when the dates are reading.
        /// </summary>
        /// <param name="ofxDate">Date</param>
        /// <param name="partDateTime">Part of date</param>
        /// <returns></returns>
        private int GetPartOfOfxDate(string ofxDate, PartDateTime partDateTime)
        {
            int result = 0;

            switch (partDateTime)
            {
                case PartDateTime.DAY:
                    result = int.Parse(ofxDate.Substring(6, 2));
                    break;
                case PartDateTime.MONTH:
                    result = int.Parse(ofxDate.Substring(4, 2));
                    break;
                case PartDateTime.YEAR:
                    result = int.Parse(ofxDate.Substring(0, 4));
                    break;
                case PartDateTime.HOUR:
                    if (ofxDate.Length >= 10)
                        result = int.Parse(ofxDate.Substring(8, 2));
                    else
                        result = 0;
                    break;
                case PartDateTime.MINUTE:
                    if (ofxDate.Length >= 12)
                        result = int.Parse(ofxDate.Substring(10, 2));
                    else
                        result = 0;
                    break;
                case PartDateTime.SECOND:
                    if (ofxDate.Length >= 14)
                        result = int.Parse(ofxDate.Substring(12, 2));
                    else
                        result = 0;
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Method that convert a OFX date string to DateTime object.
        /// </summary>
        /// <param name="ofxDate"></param>
        /// <param name="extract"></param>
        /// <returns></returns>
        private DateTime ConvertOfxDateToDateTime(string ofxDate, Extract extract)
        {
            DateTime dateTimeReturned = DateTime.MinValue;
            try
            {
                int year = GetPartOfOfxDate(ofxDate, PartDateTime.YEAR);
                int month = GetPartOfOfxDate(ofxDate, PartDateTime.MONTH);
                int day = GetPartOfOfxDate(ofxDate, PartDateTime.DAY);
                int hour = GetPartOfOfxDate(ofxDate, PartDateTime.HOUR);
                int minute = GetPartOfOfxDate(ofxDate, PartDateTime.MINUTE);
                int second = GetPartOfOfxDate(ofxDate, PartDateTime.SECOND);

                dateTimeReturned = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception ex)
            {
                extract.ImportingErrors.Add(string.Format("Invalid datetime {0}", ofxDate));
            }
            return dateTimeReturned;
        }

        private int GetBankId(string value, Extract extract)
        {
            int bankId;
            if (!int.TryParse(value, out bankId))
            {
                extract.ImportingErrors.Add(string.Format("Bank id isn't a numeric value: {0}", value));
                bankId = 0;
            }
            return bankId;
        }

        private double GetTransactionValue(string value, Extract extract)
        {
            double returnValue = 0;
            try
            {
                //var culture = new CultureInfo("pt-br");
                //var provider = new NumberFormatInfo();
                //provider.NumberDecimalSeparator = ",";
                //returnValue = Convert.ToDouble(value, culture.NumberFormat);
                returnValue = Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                extract.ImportingErrors.Add(string.Format("Invalid transaction value: {0}", value));
            }
            return returnValue;
        }
    }
}
