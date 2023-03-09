using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace ComunBACK.Utils;


[Serializable()]
public class BridgeCommand : ISerializable
{
    private Hashtable commandFields = new Hashtable();
    private String cryptKey = String.Empty;

    public Object this[String key]
    {
        get { return commandFields[key]; }
        set
        {
            if (commandFields.ContainsKey(key)) commandFields[key] = value;
            else commandFields.Add(key, value);
        }
    }
    public String CryptKey { set { cryptKey = value; } get { return cryptKey; } }

    public BridgeCommand(String CryptKey)
    {
        this.CryptKey = CryptKey;
    }
    public BridgeCommand(String CryptKey, String EncodedCommand)
    {
        this.CryptKey = CryptKey;
        DecodeCommand(EncodedCommand);
    }
    public BridgeCommand(SerializationInfo info, StreamingContext context)
    {
        CryptKey = info.GetString("CryptKey");
        DecodeCommand(info.GetString("EncodedCommand"));
    }

    public String GetEncoded()
    {
        return HttpUtility.UrlEncode(Crypt.Encrypt(ToString(), CryptKey));
    }
    public bool DecodeCommand(String EncodedCommand)
    {
        try
        {
            commandFields.Clear();
            String Decrypted = Crypt.Decrypt(EncodedCommand, CryptKey);
            foreach (String field in Decrypted.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                String[] fieldDef = field.Split(new String[] { ":=" }, StringSplitOptions.None);
                this[fieldDef[0]] = fieldDef[1];
            }
        }
        catch { return false; }
        return true;
    }
    public override string ToString()
    {
        StringBuilder strBldr = new StringBuilder();
        foreach (Object key in commandFields.Keys)
            strBldr.AppendFormat("{0}:={1}&", key, commandFields[key]);
        String str = strBldr.ToString();

        if (str.Length > 0)
        {
            return str.Substring(0, str.Length - 1);
        }
        else
        {
            return str;
        }
    }

    #region Miembros de ISerializable

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("EncodedCommand", HttpUtility.UrlDecode(GetEncoded()));
        info.AddValue("CryptKey", CryptKey);
    }

    #endregion
}
