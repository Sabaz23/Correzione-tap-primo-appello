namespace ExtensionMethod{
    public static class ExtensionMethod{
        public static IEnumerable<bool?> InteresectOn<T1,T2>(this IEnumerable<Func<T1,T2>> source, IEnumerable<Func<T1,T2>> other, T1 p)
        {
            using var e1=source.GetEnumerator();
            using var e2=other.GetEnumerator();

            var sourceHasElements = e1.MoveNext();
            var otherHasElements = e2.MoveNext();

            while(sourceHasElements && otherHasElements)
            {
                try{
                    bool? result = Equals(e1.Current(p),e2.Current(p));
                }
                catch(Exception)
                {

                }
                yield return result;
                sourceHasElements = e1.MoveNext();
                otherHasElements = e2.MoveNext();
            }

            //Se sono entrambi true non uscirei dal while
            //Se sono entrambi false non entro nell'if
            //Se una è true e l'altra è false entro qui
            //E lancio l'eccezione
            if((sourceHasElements||otherHasElement))
                throw new ArgumentException();
        }
    }
}