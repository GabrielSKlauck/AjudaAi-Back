namespace Rest.Infrastructure
{
    public class Compactador
    {
        public string Compactar(string base64)
        {
            string base64Compactada;
            for (int i = 0; i < base64.Length; i++)
            {
                int contador = 1;
                try
                {
                    while (base64[i] == base64[i + contador])
                    {
                        contador++;
                    }
                    if (contador != 1)
                    {
                        base64 = base64.Remove(i+1, contador-1);

                    }
                    Console.WriteLine(base64);
                    
                }
                catch (Exception e)
                {

                }
                Console.WriteLine($"Para {base64[i]} " + contador);
                contador = 1;
            }
            return "";
        }
    }
}
