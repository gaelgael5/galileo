using System;
using System.IO;
using System.Text;

namespace Bb.Galileo
{


    public static class ContentHelper
    {


        /// <summary>
        /// Blocks until the file is not locked any more.
        /// </summary>
        /// <param name="file"></param>
        public static bool WaitForFile(this FileInfo file, TimeSpan maxtime)
        {
            var nextStop = DateTime.Now.Add(maxtime);
            while (true)
            {
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        fs.ReadByte();
                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (System.IO.IOException)
                {
                    if (DateTime.Now < nextStop)    // Wait for the lock to be released
                        System.Threading.Thread.Sleep(100);
                    else
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Loads the content of the file.
        /// </summary>
        /// <param name="rootSource">The root source.</param>
        /// <returns></returns>
        public static string LoadContentFromFile(params string[] rootSource)
        {
            string _path = System.IO.Path.Combine(rootSource);
            return LoadContentFromFile(_path);
        }

        public static string LoadContentFromFile(this FileInfo self)
        {
            return LoadContentFromFile(self.FullName);
        }

        public static string LoadContentFromFile(this string _path)
        {

            string fileContents = string.Empty;
            System.Text.Encoding encoding = null;
            FileInfo _file = new FileInfo(_path);

            using (FileStream fs = _file.OpenRead())
            {

                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                if (cdet.Charset != null)
                    encoding = System.Text.Encoding.GetEncoding(cdet.Charset);
                else
                    encoding = System.Text.Encoding.UTF8;

                fs.Position = 0;

                byte[] ar = new byte[_file.Length];
                fs.Read(ar, 0, ar.Length);
                fileContents = encoding.GetString(ar);
            }

            if (fileContents.StartsWith("ï»¿"))
                fileContents = fileContents.Substring(3);

            if (encoding != System.Text.Encoding.UTF8)
            {
                var datas = System.Text.Encoding.UTF8.GetBytes(fileContents);
                fileContents = System.Text.Encoding.UTF8.GetString(datas);
            }

            return fileContents;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string LoadContentFromText(this byte[] text)
        {

            string textContents = string.Empty;
            System.Text.Encoding encoding = null;

            using (MemoryStream fs = new MemoryStream(text))
            {

                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                if (cdet.Charset != null)
                    encoding = System.Text.Encoding.GetEncoding(cdet.Charset);
                else
                    encoding = System.Text.Encoding.UTF8;

                fs.Position = 0;

                byte[] ar = new byte[text.Length];
                fs.Read(ar, 0, ar.Length);
                textContents = encoding.GetString(ar);
            }

            if (textContents.StartsWith("ï»¿"))
                textContents = textContents.Substring(3);

            if (encoding != System.Text.Encoding.UTF8)
            {
                var datas = System.Text.Encoding.UTF8.GetBytes(textContents);
                textContents = System.Text.Encoding.UTF8.GetString(datas);
            }

            return textContents;

        }

        public static void Save(this object self, string filename, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.Indented, params Newtonsoft.Json.JsonConverter[] converters)
        {
            Save(filename, Newtonsoft.Json.JsonConvert.SerializeObject(self, formatting, converters));
        }

        /// <summary>
        /// Save the content in the specified file.
        /// If the directory don't exist. it is created.
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="content"></param>
        public static void Save(this string _path, string content)
        {

            var file = new FileInfo(_path);
            if (!file.Directory.Exists)
                file.Directory.Create();

            File.WriteAllText(file.FullName, content);

        }

        /// <summary>
        /// Save the content in the specified file.
        /// If the directory don't exist. it is created.
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="content"></param>
        public static void Save(string _path, StringBuilder content)
        {

            var file = new FileInfo(_path);
            if (!file.Directory.Exists)
                file.Directory.Create();

            File.WriteAllText(file.FullName, content.ToString());

        }

        /// <summary>
        /// encode the specified <see cref="string"/> in base 64 encoded value
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ConvertToBase64(this string self)
        {

            if (string.IsNullOrEmpty(self))
                return string.Empty;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(self);
            var result = Convert.ToBase64String(bytes);
            return result;
        }

        /// <summary>
        /// decode the specified base 64 <see cref="string"/> encoded value.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ConvertFromBase64(this string self)
        {

            if (string.IsNullOrEmpty(self))
                return string.Empty;

            byte[] bytes = Convert.FromBase64String(self); ;
            string result = System.Text.Encoding.UTF8.GetString(bytes);

            return result;

        }

        /// <summary>
        /// convert the <see cref="StringBuilder"/> in <see cref="JToken" />
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JToken ConvertToJson(this StringBuilder self)
        {
            return Newtonsoft.Json.Linq.JToken.Parse(self.ToString());
        }

        /// <summary>
        /// convert the <see cref="string"/> in <see cref="JToken" />
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JToken ConvertToJson(this string self)
        {
            return Newtonsoft.Json.Linq.JToken.Parse(self);
        }

        /// <summary>
        /// convert the <see cref="string"/> in <see cref="JToken" />
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JToken ConvertToJson(this object self)
        {
            return Newtonsoft.Json.Linq.JToken.FromObject(self);
        }

        public static T Deserialize<T>(this string self)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(self);
        }

        /// <summary>
        /// Read the <see cref="MemoryStream" /> and return the result in <see cref="string"/>
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ConvertToString(this MemoryStream self)
        {
            return self.ToArray().LoadContentFromText();
        }

        public static int DamerauLevenshteinDistance(this string self, string secondText)
        {
            var n = self.Length + 1;
            var m = secondText.Length + 1;
            var arrayD = new int[n, m];

            for (var i = 0; i < n; i++)
            {
                arrayD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                arrayD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var cost = self[i - 1] == secondText[j - 1] ? 0 : 1;

                    arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1, // delete
                                                            arrayD[i, j - 1] + 1, // insert
                                                            arrayD[i - 1, j - 1] + cost); // replacement

                    if (i > 1 && j > 1
                       && self[i - 1] == secondText[j - 2]
                       && self[i - 2] == secondText[j - 1])
                    {
                        arrayD[i, j] = Minimum(arrayD[i, j],
                        arrayD[i - 2, j - 2] + cost); // permutation
                    }
                }
            }

            return arrayD[n - 1, m - 1];
        }

        static int Minimum(int a, int b) => a < b ? a : b;

        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

    }



}
