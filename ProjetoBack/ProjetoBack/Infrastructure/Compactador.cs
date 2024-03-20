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
                string encurtador;
                try
                {
                    while (base64[i] == base64[i + contador])
                    {
                        contador++;
                    }
                    if (contador != 1)
                    {
                        base64 = base64.Remove(i+1, contador-1);
                        encurtador = base64[i] + "-" + (contador - 1);
                        base64 = base64.Remove(i,i);
                        base64 = base64.Insert(i, encurtador); 
                    }
                    
                    
                }
                catch (Exception e)
                {

                }
                
                contador = 1;
            }
            Console.WriteLine(base64);
            return "";
        }
    }
}
