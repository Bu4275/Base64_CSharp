using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace base64_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Base64Decode("ms4otszPhcr7tMmzGMkHyFn="));
            Console.Read();
        }
        
        public static string Chr(int Num)
        {
            char C = Convert.ToChar(Num);
            return C.ToString();
        }

        public static string Base64Encode(string Message)
        {
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                                             'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
                                             'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                             '8','9','+','/','='};

 
            byte empty = (byte)0;

            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int padding = messageLen % 3;

            if (padding > 0)
            {
                for (int i = 0; i < padding; i++)
                    byteMessage.Add(empty);
                page++;
            }

            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = (instr[0] >> 2) & 0x3f;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;

                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }

        public static string Base64Decode(string Message)
        {
            
            if ((Message.Length % 4) != 0)
            {
                throw new ArgumentException("不是正確的BASE64編碼，請檢查。", "Message");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正確的BASE64編碼，請檢查。", "Message");
            }
            
            // setYourSelf
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            for (int j = 0; j < byteMessage.Count; j++)
            {
                byteMessage[j] = (byte)Base64Code.IndexOf(Message[j]);
            }
            int page = Message.Length / 4;
            System.Collections.Generic.List<byte> outMessage = new System.Collections.Generic.List<byte>(page * 3);
            int i = 0;

            for (i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                int[] outstr = new int[3];
                instr[0] = (byte)byteMessage[i * 4];
                instr[1] = (byte)byteMessage[i * 4 + 1];
                instr[2] = (byte)byteMessage[i * 4 + 2];
                instr[3] = (byte)byteMessage[i * 4 + 3];

                //second
                if (instr[2] != 64)
                    outstr[1] = (instr[1] << 4 | instr[2] >> 2) & 0xff;
                else
                    outstr[1] = 0;

                //third
                if (instr[3] != 64)
                    outstr[2] = (instr[2] << 6 | instr[3]) & 0xff;
                else
                    outstr[2] = 0;

                //first
                outstr[0] = (instr[0] << 2 | instr[1] >> 4) & 0xff;

                outMessage.Add((byte)outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add((byte)outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add((byte)outstr[2]);
            }

            if (Message.Length % 4 > 0)
                outMessage.Add((byte)((byte)byteMessage[i * 3] << 2 | (byte)byteMessage[i * 3 + 1] >> 4));
            if (Message.Length % 4 > 1)
                outMessage.Add((byte)((byte)byteMessage[i * 3 + 1] << 2 | (byte)byteMessage[i * 3 + 2] >> 4));
            if (Message.Length % 4 > 2)
                outMessage.Add((byte)((byte)byteMessage[i * 3 + 2] << 6 | (byte)byteMessage[i * 3 + 3] >> 4));

            byte[] outbyte = (byte[])outMessage.ToArray();
            return System.Text.Encoding.Default.GetString(outbyte);

        }

        public static string MyBase64Encode(string Message)
        {

            /*
            byte[] Base64Code = new byte[]{ 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 62, 64, 64, 64, 63,
                                    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 64, 64, 64, 64, 64, 64,
                                    64,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14,
                                    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 64, 64, 64, 64, 64,
                                    64, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                                    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                    64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64};
                                   */


            byte[] Base64Code = new byte[]{
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x3E, 0x40, 0x40, 0x40, 0x3F,
                                0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE,
                                0xF, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x27, 0x26, 0x29,
                                0x28, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,0x40 };


            byte empty = (byte)0;
            int i = 0;

            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int padding = messageLen % 3;

            if (padding > 0)
            {
                for (i = 0; i < 3 - padding; i++)
                    byteMessage.Add(empty);
                page++;
            }

            outmessage = new System.Text.StringBuilder(page * 4);
            
            for (i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];

                outstr[0] = (instr[0] >> 2) & 0x3f;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                outstr[3] = (instr[2] & 0x3f);

                if(instr[1] == 0) outmessage.Append("=");
                if (instr[2] == 0) outmessage.Append("=");

                for (int j = 0; j < Base64Code.Length; j++)
                {
                    if (Base64Code[j] == outstr[0])
                    {
                        outmessage.Append(Chr(j));
                        break;
                    }
                }

                for (int j = 0; j < Base64Code.Length; j++)
                {
                    if (Base64Code[j] == outstr[1])
                    {
                        outmessage.Append(Chr(j));
                        break;
                    }
                }

                for (int j = 0; j < Base64Code.Length; j++)
                {
                    if (Base64Code[j] == outstr[2])
                    {
                        outmessage.Append(Chr(j));
                        break;
                    }
                }

                for (int j = 0; j < Base64Code.Length; j++)
                {
                    if (Base64Code[j] == outstr[3])
                    {
                        outmessage.Append(Chr(j));
                        break;
                    }
                }
                
                /*
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
                */
            }

            
            return outmessage.ToString();
        }

        public static string MyBase64Decode(string Message)
        {
            if ((Message.Length % 4) != 0)
            {
                throw new ArgumentException("不是正確的BASE64編碼，請檢查。", "Message");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正確的BASE64編碼，請檢查。", "Message");
            }
            /*
            byte[] Base64Code = new byte[]{ 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 62, 64, 64, 64, 63,
                                            52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 64, 64, 64, 64, 64, 64,
                                            64,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14,
                                            15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 64, 64, 64, 64, 64,
                                            64, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                                            41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
                                            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64};
*/

            byte[] Base64Code = new byte[]{
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x3E, 0x40, 0x40, 0x40, 0x3F,
                                0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE,
                                0xF, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x27, 0x26, 0x29,
                                0x28, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,
                                0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40,0x40 };
            //string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";


            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            for (int j = 0; j < byteMessage.Count; j++)
            {
                byteMessage[j] = (byte)Base64Code[Message[j]];
            }
            int page = Message.Length / 4;
            System.Collections.Generic.List<byte> outMessage = new System.Collections.Generic.List<byte>(page * 3);
            int i = 0;

            for (i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                int[] outstr = new int[3];
                instr[0] = (byte)byteMessage[i * 4];
                instr[1] = (byte)byteMessage[i * 4 + 1];
                instr[2] = (byte)byteMessage[i * 4 + 2];
                instr[3] = (byte)byteMessage[i * 4 + 3];


                if (instr[2] != 64)
                    outstr[1] = (instr[1] * 16 | instr[2] >> 2) & 0xff;
                else
                    outstr[1] = 0;

                if (instr[3] != 64)
                    outstr[2] = (instr[2] << 6 | instr[3]) & 0xff;
                else
                    outstr[2] = 0;

                outstr[0] = (instr[0] * 4 | instr[1] >> 4) & 0xff;

                outMessage.Add((byte)outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add((byte)outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add((byte)outstr[2]);
            }

            if (Message.Length % 4 > 0)
                outMessage.Add((byte)((byte)byteMessage[i * 3] * 4 | (byte)byteMessage[i * 3 + 1] >> 4));
            if (Message.Length % 4 > 1)
                outMessage.Add((byte)((byte)byteMessage[i * 3 + 1] * 16 | (byte)byteMessage[i * 3 + 2] >> 4));
            if (Message.Length % 4 > 2)
                outMessage.Add((byte)((byte)byteMessage[i * 3 + 2] << 6 | (byte)byteMessage[i * 3 + 3] >> 4));

            byte[] outbyte = (byte[])outMessage.ToArray();
            return System.Text.Encoding.Default.GetString(outbyte);

        }
    }
}
