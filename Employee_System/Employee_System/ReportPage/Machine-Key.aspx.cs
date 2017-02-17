using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Machine_Key : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        MachineKeyVersion version = MachineKeyVersion.Net2;
        string validationKey = GenerateKey(64);
        string decryptionKey;
        if (version == MachineKeyVersion.Net1)
            decryptionKey = GenerateKey(24);
        else
            decryptionKey = GenerateKey(32);

        // Construct <machineKey> tag
        StringBuilder builder = new StringBuilder();
        builder.Append("<machineKey");
        builder.AppendFormat(" validationKey=\"{0}\"", validationKey);
        builder.AppendFormat(" decryptionKey=\"{0}\"", decryptionKey);
        builder.Append(" validation=\"SHA1\"");
        if (version == MachineKeyVersion.Net2)
            builder.Append(" decryption=\"AES\"");
        builder.Append(" />");
        txtMachineKey.Text = builder.ToString();
    }

    public enum MachineKeyVersion
    {
        /// <summary>
        /// .NET version 1.1.
        /// </summary>
        Net1,

        /// <summary>
        /// .NET version 2.0 and up.
        /// </summary>
        Net2,
    }

    protected static string GenerateKey(int length)
    {
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        byte[] buff = new byte[length];
        rngCsp.GetBytes(buff);
        StringBuilder sb = new StringBuilder(buff.Length * 2);
        for (int i = 0; i < buff.Length; i++)
            sb.Append(string.Format("{0:X2}", buff[i]));
        return sb.ToString();
    }


}