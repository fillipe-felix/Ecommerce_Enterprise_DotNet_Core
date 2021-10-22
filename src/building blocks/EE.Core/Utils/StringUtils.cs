using System.Linq;

namespace EE.Core.Utils
{
    public static class StringUtils
    {
        
        /// <summary>
        /// Util para verificar se uma string contem apenas numeros
        /// </summary>
        /// <param name="str"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ApenasNumeros(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}